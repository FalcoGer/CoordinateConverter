using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents JF17 aircraft
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSAircraft" />
    public class JF17 : DCSAircraft
    {
        public int StartingWaypoint { get; private set; }

        public enum EPointType
        {
            // 0 is INS Alignment point
            Waypoint,           //  1 - 29 (WP1 .. 29)
            MissileRP,          // 30 - 35 (RP1 .. 6)
            PrePlanned,         // 36 - 39 (PP1 .. 4)
            // 40 is SPI
            MarkPoint,          // 41 - 49 (MK1 .. 9)
            // 50 - 58 are non editable airfields nearest to last valid stpt
            Airfield            // 59
        }

        private readonly Dictionary<EPointType, Tuple<int, int>> destStartAndCount = new Dictionary<EPointType, Tuple<int, int>>()
        {
            {EPointType.Waypoint, new Tuple<int, int>(1, 29)},
            {EPointType.MissileRP, new Tuple<int, int>(30, 6)},
            {EPointType.PrePlanned, new Tuple<int, int>(36, 4)},
            {EPointType.MarkPoint, new Tuple<int, int>(41, 9)},
            {EPointType.Airfield, new Tuple<int, int>(59, 1)}
        };
        private Dictionary<EPointType, int> nextPointForType = null;
        private bool hasEnteredWaypoint = false;

        public JF17(int startingWaypoint)
        {
            StartingWaypoint = startingWaypoint;
        }

        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            var commands = new List<DCSCommand>();
            // var commands = new DebugCommandList();

            // Get point type data
            EPointType pointType = EPointType.Waypoint;
            if (coordinate.AircraftSpecificData.ContainsKey(typeof(JF17)))
            {
                pointType = (coordinate.AircraftSpecificData[typeof(JF17)] as JF17SpecificData).PointType;
            }

            // Check if we are over the limit
            if (nextPointForType[pointType] > destStartAndCount[pointType].Item1 + destStartAndCount[pointType].Item2)
            {
                // the next point would be over the limit, we ignore it.
                return commands;
            }

            if (pointType == EPointType.Waypoint)
            {
                hasEnteredWaypoint = true;
            }
            int pointId = nextPointForType[pointType]++;

            CoordinateSharp.CoordinateFormatOptions formatOptions = new CoordinateSharp.CoordinateFormatOptions
            {
                Display_Symbols = false,
                Display_Hyphens = false,
                Position_First = true,
                Display_Trailing_Zeros = true,
                Display_Leading_Zeros = true,
                Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds,
                Round = 1
            };
            string latStr = coordinate.Coordinate.Latitude.ToString(formatOptions);
            string lonStr = coordinate.Coordinate.Longitude.ToString(formatOptions);

            // Select DST (R1)
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R1));
            // Enter point ID
            commands.AddRange(EnterUFC(pointId.ToString().PadLeft(2, '0')));
            // Confirm point ID
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R1));

            // Select LAT
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_L2));
            // Enter LAT
            commands.AddRange(EnterUFC(latStr));
            // Confirm LAT
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_L2));
            // TODO: Check if N/S is automatically correct

            // Select LON
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_L3));
            // Enter LON
            commands.AddRange(EnterUFC(lonStr));
            // Confirm LON
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_L3));
            // TODO: Check if E/W is automatically correct

            int altitude = (int)Math.Round(Math.Min(coordinate.GetAltitudeValue(true), 99999));
            // Select ALT
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R4));
            // Enter ALT
            commands.AddRange(EnterUFC(Math.Abs(altitude).ToString().PadLeft(5, '0')));
            // Confirm ALT
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R4));
            // Enter Negative ALT if required
            if (altitude < 0)
            {
                commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_L4));
            }

            return commands;
        }

        private List<DCSCommand> EnterUFC(string data)
        {
            List <DCSCommand> commands = new List<DCSCommand>();
            foreach (char ch in data)
            {
                switch (ch)
                {
                    case '0':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_0));
                        break;
                    case '1':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_1_PFL));
                        break;
                    case '2':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_2_VRC));
                        break;
                    case '3':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_3));
                        break;
                    case '4':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_4_DST));
                        break;
                    case '5':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_5_TOT));
                        break;
                    case '6':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_6_TOD));
                        break;
                    case '7':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_7_FUL));
                        break;
                    case '8':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_8_IFF));
                        break;
                    case '9':
                        commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_9));
                        break;
                    default:
                        // ignore anything else
                        break;
                }
            }
            return commands;
        }

        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            return new List<string> { "Point" };
        }

        public override List<string> GetPointTypes()
        {
            return Enum.GetNames(typeof(EPointType)).ToList();
        }

        public override List<DCSCommand> GetPostPointActions()
        {
            nextPointForType = null;
            List<DCSCommand> commands = new List<DCSCommand>();
            if (hasEnteredWaypoint)
            {
                // Select DST (R1)
                commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R1));
                // Enter point ID
                commands.AddRange(EnterUFC(StartingWaypoint.ToString().PadLeft(2, '0')));
                // Confirm point ID
                commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_R1));
            }
            // Return to main screen
            commands.Add(new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_RTN));
            return commands;
        }

        public override List<DCSCommand> GetPrePointActions()
        {
            hasEnteredWaypoint = false; // we have not entered any waypoints yet
            // Setup the point IDs
            nextPointForType = new Dictionary<EPointType, int>()
            {
                { EPointType.Waypoint, StartingWaypoint }
            };
            foreach (EPointType pt in Enum.GetValues(typeof(EPointType)))
            {
                if (pt == EPointType.Waypoint)
                {
                    continue;
                }
                nextPointForType.Add(pt, destStartAndCount[pt].Item1);
            }
            return new List<DCSCommand>()
            {
                new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_RTN),      // UFC Main Screen
                new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_RTN),      // Needs 2 presses because first press might exit out of data entry mode
                new DCSCommand(UFC_DEVICE_ID, (int)EKeyCodes.KEY_4_DST)     // DST Screen
            };
        }

        private const int UFC_DEVICE_ID = 46;
        enum EKeyCodes
        {
            // command_defs.lua L662 for the offset, L394 for the start and definition
            // PNT_500 is 3002
            KEY_1_PFL = 3002 + 700 - 500,
            KEY_2_VRC,
            KEY_3,
            KEY_L1,
            KEY_R1,
            KEY_OAP,
            KEY_MRK,
            KEY_4_DST,
            KEY_5_TOT,
            KEY_6_TOD,
            KEY_L2,
            KEY_R2,
            KEY_P_U,
            KEY_HNS,
            KEY_7_FUL,
            KEY_8_IFF,
            KEY_9,
            KEY_L3,
            KEY_R3,
            KEY_AP,
            KEY_FPM,
            KEY_RTN,
            KEY_0,
            KEY_NA_1,            
            KEY_L4,
            KEY_R4,
            KEY_NA_2
        }
    }
}
