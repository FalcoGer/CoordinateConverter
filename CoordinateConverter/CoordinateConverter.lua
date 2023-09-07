-- REF: https://wiki.hoggitworld.com/view/DCS_export
-- REF: https://wiki.hoggitworld.com/view/DCS_Export_Script

local LOG_MODNAME = "COORDINATECONVERTER"
local DEBUGGING = false

log.write(LOG_MODNAME, log.INFO, "Initializing...")
--Version 3
local tcpServer                        = nil
package.path                           = package.path .. ";" .. lfs.currentdir() .. "/LuaSocket/?.lua"
package.cpath                          = package.cpath .. ";" .. lfs.currentdir() .. "/LuaSocket/?.dll"
package.path                           = package.path .. ";" .. lfs.currentdir() .. "/Scripts/?.lua"
local socket                           = require("socket")
local JSON                             = loadfile("Scripts\\JSON.lua")()

local upstreamLuaExportStart           = LuaExportStart
local upstreamLuaExportStop            = LuaExportStop
local upstreamLuaExportAfterNextFrame  = LuaExportAfterNextFrame
local upstreamLuaExportBeforeNextFrame = LuaExportBeforeNextFrame

function LuaExportStop()
    if upstreamLuaExportStop ~= nil then
        successful, err = pcall(upstreamLuaExportStop)
        if not successful then
            log.write(LOG_MODNAME, log.ERROR, "Error in upstream LuaExportStop function" .. tostring(err))
        end
    end
    -- runs once when the mission is stopped
end

function LuaExportStart()
    if upstreamLuaExportStart ~= nil then
        successful, err = pcall(upstreamLuaExportStart)
        if not successful then
            log.write(LOG_MODNAME, log.ERROR, "Error in upstream LuaExportStart function" .. tostring(err))
        end
    end
    local BIND_IP = "127.0.0.1"
    local BIND_PORT = 42020

    tcpServer = socket.tcp()
    tcpServer:bind(BIND_IP, BIND_PORT)
    tcpServer:listen(1)
    tcpServer:settimeout(0)
end

local commands = {}
local busy = false
local isPressed = false
local currCommandIndex = 1
local lastDevice = ""
local lastCode = ""
local lastNeedDepress = true
local whenToDepress = nil
local stringtoboolean = { ["True"] = true,["False"] = false }

function LuaExportBeforeNextFrame()
    if upstreamLuaExportBeforeNextFrame ~= nil then
        successful, err = pcall(upstreamLuaExportBeforeNextFrame)
        if not successful then
            log.write(LOG_MODNAME, log.ERROR, "Error in upstream LuaExportBeforeNextFrame function" .. tostring(err))
        end
    end
    -- executed before the frame is rendered.
    -- put stuff into the frame here
    
    if busy then
        local f = function()
            if isPressed then
                -- check if the time has come to depress
                local currTime = socket.gettime()
                if currTime >= whenToDepress then
                    -- check if it even needs a depress
                    if lastNeedDepress then
                        GetDevice(lastDevice):performClickableAction(lastCode, 0)
                    end
                    isPressed = false
                    currCommandIndex = currCommandIndex + 1
                end
            else
                --check if there are buttons left to press
                if currCommandIndex <= #commands then
                    lastDevice = commands[currCommandIndex]["device"]
                    lastCode = commands[currCommandIndex]["code"]
                    lastNeedDepress = stringtoboolean[commands[currCommandIndex]["addDepress"]]
                    local delay = tonumber(commands[currCommandIndex]["delay"])
                    local activate = tonumber(commands[currCommandIndex]["activate"])
                    -- Push the button
                    GetDevice(lastDevice):performClickableAction(lastCode, activate)
                    --Store the time when we will need to depress
                    whenToDepress = socket.gettime() + (delay / 1000)
                    isPressed = true
                else
                    --if there's nothing else to press, we are done
                    busy = false
                    currCommandIndex = 1
                end
            end
        end
        success, errMsg = pcall(f)
        if not success then
            log.write(LOG_MODNAME, log.ERROR, "Error at entering command at index " .. tostring(currCommandIndex) .. ": " .. err)
        end
    end
end

function LuaExportAfterNextFrame()
    if upstreamLuaExportAfterNextFrame ~= nil then
        successful, err = pcall(upstreamLuaExportAfterNextFrame)
        if not successful then
            log.write(LOG_MODNAME, log.ERROR, "Error in upstream LuaExportAfterNextFrame function" .. tostring(err))
        end
    end
    -- runs after a frame was rendered
    -- fetch data from the game here

    local client, err = tcpServer:accept()
    if client ~= nil then
        client:settimeout(10)
        local data = nil
        local err = nil
        
        data, err = client:receive()
        if err then
            log.write(LOG_MODNAME, log.ERROR, "Error at receiving: " .. err)
        end

        if data then
            if DEBUGGING then
                log.write(LOG_MODNAME, log.INFO, "Got data: \n" .. data .. "\n")
            end

            data = JSON:decode(data)

            -- Answer section
            local response = {}
            response["time"] = os.date("%Y-%m-%dT%H:%M:%S")

            -- handle aircraft type request
            if data["FetchAircraftType"] then
                if stringtoboolean[data["FetchAircraftType"]] then
                    local f = function()
                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "Getting AircraftType")
                        end

                        local selfData = LoGetSelfData()

                        if selfData and selfData["Name"] then
                            response["Model"] = selfData["Name"]
                        else
                            response["Model"] = "null"
                        end

                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "Got type " .. response["Model"])
                        end
                    end

                    success, errMsg = pcall(f)
                    if not success then
                        log.write(LOG_MODNAME, log.ERROR, "Failure to fetch aircraft type: " .. tostring(errMsg))
                    end

                end
            end

            -- handle camera position request
            if data["FetchCameraPosition"] then
                if stringtoboolean[data["FetchCameraPosition"]] then
                    local f = function()
                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "Getting camera position")
                        end
                    
                        local camPos = LoGetCameraPosition()

                        if camPos then
                            local loX = camPos['p']['x']
                            local loZ = camPos['p']['z']
                            local loY = camPos['p']['y']

                            local elevation = LoGetAltitude(loX, loZ)
                            local coords = LoLoCoordinatesToGeoCoordinates(loX, loZ)

                            response["CameraPosition"] = {}
                            response["CameraPosition"]["Lat"] = tostring(coords.latitude)
                            response["CameraPosition"]["Lon"] = tostring(coords.longitude)
                            response["CameraPosition"]["Alt"] = tostring(loZ) -- might be wrong
                            response["CameraPosition"]["Elev"] = tostring(elevation)
                        end

                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "Got " .. response["CameraPosition"]["Lat"] .. " / " .. response["CameraPosition"]["Lon"] .. " / " .. response["CameraPosition"]["Alt"] .. " / " .. response["CameraPosition"]["Elev"] )
                        end
                    end

                    success, errMsg = pcall(f)
                    if not success then
                        log.write(LOG_MODNAME, log.ERROR, "Failure to fetch camera position: " .. tostring(errMsg))
                    end
                end
            end

            -- handle ground elevation requests
            if data["Altitudes"] then
                local f = function()
                    local pointsToProcess = data["Altitudes"]
                    -- lua arrays start with 1
                    for idx=1,#pointsToProcess,1 do
                        local point = pointsToProcess[idx]
                        log.write(LOG_MODNAME, log.ERROR, "Fetching point altitudes for: " .. JSON:encode(point))
                        local loLo = LoGeoCoordinatesToLoCoordinates(point["Lat"], point["Lon"])
                        local loX = loLo['x']
                        local loZ = loLo['z']
                        local elevation = LoGetAltitude(loX, loZ)
                        point["Elev"] = elevation
                        pointsToProcess[idx] = point
                    end
                    response["Altitudes"] = pointsToProcess
                end
                success, errMsg = pcall(f)
                if not success then
                    log.write(LOG_MODNAME, log.ERROR, "Failure to fetch point altitudes: " .. tostring(errMsg))
                end
            end

            -- set up command input logic
            if data["Commands"] and not busy then
                commands = data["Commands"]
                busy = true
            elseif data["Commands"] then
                response["Error"] = "EBusy"
            end

            local responseData = JSON:encode(response)
            
            if DEBUGGING then
                log.write(LOG_MODNAME, log.INFO, "Returning response: \n" .. responseData .. "\n")
            end

            client:send(responseData)
        else
            log.write(LOG_MODNAME, log.INFO, "Connection without data")
        end
    end
end

log.write(LOG_MODNAME, log.INFO, "Done")
