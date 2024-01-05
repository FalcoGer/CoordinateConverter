-- REF: https://wiki.hoggitworld.com/view/DCS_export
-- REF: https://wiki.hoggitworld.com/view/DCS_Export_Script

local LOG_MODNAME = "COORDINATECONVERTER"
local DEBUGGING = false -- if true logs all the things
local TESTING = false -- if true sends all available data, even unrequested

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
                    lastNeedDepress = commands[currCommandIndex]["addDepress"]
                    local delay = tonumber(commands[currCommandIndex]["delay"])
                    local value = tonumber(commands[currCommandIndex]["value"])
                    if (DEBUGGING) then
                        log.write(LOG_MODNAME, log.INFO, "Pressing button\n  Device: " .. tostring(lastDevice) .. "\n  Code: " .. tostring(lastCode) .. "\n  AddDepress: " .. tostring(lastNeedDepress) .. "\n  Delay:" .. tostring(delay) .. "\n  Value: " .. tostring(value))
                    end
                    -- Push the button
                    GetDevice(lastDevice):performClickableAction(lastCode, value)
                    -- Store the time when we will need to depress
                    whenToDepress = socket.gettime() + (delay / 1000)
                    isPressed = true
                else
                    -- if there's nothing else to press, we are done
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
            response["ErrorList"] = {}

            -- process stop command
            if data["Stop"] and busy then
                busy = false
                commands = nil
                currCommandIndex = 1

                -- check if the button needs to be depressed
                if isPressed and lastNeedDepress then
                    GetDevice(lastDevice):performClickableAction(lastCode, 0)
                    isPressed = false
                end
            end

            -- set up command input logic
            if data["Commands"] and not busy then
                commands = data["Commands"]
                busy = true
            elseif data["Commands"] then
                response["ErrorList"][#response["ErrorList"] + 1] = "Busy pressing buttons."
            elseif busy then
                response["CmdIdx"] = currCommandIndex
            end

            -- handle aircraft type request
            if data["FetchAircraftType"] or TESTING then
                if data["FetchAircraftType"] then
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
                        response["ErrorList"][#response["ErrorList"] + 1] = tostring(errMsg)
                    end

                end
            end

            -- handle camera position request
            if data["FetchCameraPosition"] or TESTING then
                local f = function()
                    if DEBUGGING then
                        log.write(LOG_MODNAME, log.INFO, "Getting camera position")
                    end
                    
                    local camPos = LoGetCameraPosition()

                    if camPos then
                        local loX = camPos['p']['x'] -- game coordinate x
                        local loY = camPos['p']['y'] -- altitude in m ASL
                        local loZ = camPos['p']['z'] -- game coordinate y

                        local elevation = LoGetAltitude(loX, loZ)                   -- ground elevation
                        local coords = LoLoCoordinatesToGeoCoordinates(loX, loZ)    -- geo coordinates in L/L decimal degrees

                        response["CameraPosition"] = {}
                        response["CameraPosition"]["Lat"] = tostring(coords.latitude)
                        response["CameraPosition"]["Long"] = tostring(coords.longitude)
                        response["CameraPosition"]["Alt"] = tostring(loY)
                        response["CameraPosition"]["Elev"] = tostring(elevation)

                        -- Find out if we are in F10 map mode
                        local xRot = camPos['x']
                        local yRot = camPos['y']
                        local zRot = camPos['z']

                        local isF10 = (
                            xRot['x'] == 0 and xRot['y'] == -1 and xRot['z'] == 0 and
                            yRot['x'] == 1 and yRot['y'] == 0 and yRot['z'] == 0 and
                            zRot['x'] == 0 and zRot['y'] == 0 and zRot['z'] == 1
                        )

                        response["isF10"] = isF10
                    end

                    if DEBUGGING then
                        log.write(LOG_MODNAME, log.INFO, "Got " .. response["CameraPosition"]["Lat"] .. " / " .. response["CameraPosition"]["Long"] .. " / " .. response["CameraPosition"]["Alt"] .. " / " .. response["CameraPosition"]["Elev"] )
                    end
                end

                success, errMsg = pcall(f)
                if not success then
                    log.write(LOG_MODNAME, log.ERROR, "Failure to fetch camera position: " .. tostring(errMsg))
                    response["ErrorList"][#response["ErrorList"] + 1] = tostring(errMsg)
                end
            end

            -- handle ground elevation requests
            if data["Altitudes"] then -- no testing, can't fetch elevation data without positions to check
                local f = function()
                    local pointsToProcess = data["Altitudes"]
                    -- lua arrays start with 1
                    for idx=1,#pointsToProcess,1 do
                        local point = pointsToProcess[idx]
                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "Fetching point altitudes for: " .. JSON:encode(point))
                        end
                        local loLo = LoGeoCoordinatesToLoCoordinates(point["Long"], point["Lat"]) -- longitude first
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
                    response["ErrorList"][#response["ErrorList"] + 1] = tostring(errMsg)
                end
            end

            -- get weapons information
            if data["FetchWeaponStations"] or TESTING then
                local f = function()
                    local payloadInfo = LoGetPayloadInfo()
                    if payloadInfo then
                        response["Stations"] = payloadInfo["Stations"]
                        for idx=1,#payloadInfo["Stations"],1 do
                            response["Stations"][idx]["number"] = idx
                        end
                    end
                end

                success, errMsg = pcall(f)
                if not success then
                    log.write(LOG_MODNAME, log.ERROR, "Failure to fetch weapon station info: " .. tostring(errMsg))
                    response["ErrorList"][#response["ErrorList"] + 1] = tostring(errMsg)
                end
            end

            -- get units information
            -- needs to be separate because of how JSON:encode cuts off the data after ~16kB
            local unitsData = nil
            if data["FetchUnits"] or TESTING then
                local f = function()
                    unitsData = {}
                    local units = LoGetWorldObjects("units")
                    if units then
                        local idx = 1
                        for id,unit in pairs(units) do
                            if unit then
                                -- add the object identifier
                                unit["ObjectId"] = id
                                -- remove unneded data to save memory and speed up transfer
                                unit["Country"] = nil
                                unit["Coalition"] = nil
                                unit["Heading"] = nil
                                unit["Pitch"] = nil
                                unit["Bank"] = nil
                                unit["Position"] = nil
                                -- add ground elevation
                                local point = unit["LatLongAlt"]
                                if point then
                                    local loLo = LoGeoCoordinatesToLoCoordinates(point["Long"], point["Lat"]) -- longitude first
                                    local elevation = LoGetAltitude(loLo['x'], loLo['z'])
                                    point["Elev"] = tostring(elevation)
                                    unit["LatLongAlt"] = point
                                end

                                -- remove unneeded flags
                                if unit["Flags"] then
                                    unit["Flags"]["RadarActive"] = nil
                                    unit["Flags"]["Jamming"] = nil
                                    unit["Flags"]["IRJamming"] = nil
                                    unit["Flags"]["AI_ON"] = nil
                                end
                                --
                                unitsData[idx] = unit
                                idx = idx + 1
                            end
                        end
                        response["UnitsCount"] = idx
                    end
                end

                success, errMsg = pcall(f)
                if not success then
                    log.write(LOG_MODNAME, log.ERROR, "Failure to fetch units: " .. tostring(errMsg))
                    response["ErrorList"][#response["ErrorList"] + 1] = tostring(errMsg)
                end
            end

            -- Send response
            local responseData = JSON:encode(response)
            
            if unitsData then
                responseData = responseData:sub(1, -2) -- remove trailing }
            end

            if DEBUGGING then
                log.write(LOG_MODNAME, log.INFO, "Returning response: \n" .. responseData .. "\n")
            end

            client:send(responseData)
            
            -- Send units separately because of string size constraints for both JSON and socket
            if unitsData then
                local f = function()
                    for idx=1,#unitsData,1 do
                        local unitDataStr = JSON:encode(unitsData[idx])
                        
                        if idx == 1 then
                            -- add the units array to the output if it's the first element'
                            unitDataStr = ",Units:[" .. unitDataStr .. ","
                        end

                        -- Add a comma if it's not the last, otherwise close the array and object
                        if idx < #unitsData then
                            unitDataStr = unitDataStr .. ","
                        else
                            unitDataStr = unitDataStr .. "]}"
                        end
                        if DEBUGGING then
                            log.write(LOG_MODNAME, log.INFO, "sending unitDataStr: \n" ..unitDataStr .. "\n")
                        end
                        client:send(unitDataStr)
                    end
                end

                success, errMsg = pcall(f)
                if not success then
                    log.write(LOG_MODNAME, log.ERROR, "Failure send units data: " .. tostring(errMsg))
                end
            end
            client:send(string.char(0xFF) .. string.char(0x00))
        else
            log.write(LOG_MODNAME, log.INFO, "Connection without data")
        end
    end
end

log.write(LOG_MODNAME, log.INFO, "Done")
