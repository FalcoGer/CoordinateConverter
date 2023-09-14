using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// The F16C aircraft from DCS
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSAircraft" />
    public class F16C : DCSAircraft
    {
        /// <summary>
        /// Gets the starting waypoint.
        /// </summary>
        /// <value>
        /// The starting waypoint.
        /// </value>
        public int StartingWaypoint { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="F16C"/> class.
        /// </summary>
        /// <param name="startingWaypoint">The starting waypoint.</param>
        public F16C(int startingWaypoint)
        {
            StartingWaypoint = startingWaypoint;
        }

        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="coordinate">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            // We are at LAT on the correct STPT here in the STPT page
            List<DCSCommand> commands = new List<DCSCommand>();

            CoordinateSharp.CoordinateFormatOptions formattingOptions = new CoordinateSharp.CoordinateFormatOptions()
            {
                Display_Symbols = false,
                Display_Hyphens = false,
                Display_Leading_Zeros = true,
                Display_Trailing_Zeros = true,
                Position_First = true,
                Round = 3,
                Format = CoordinateSharp.CoordinateFormatType.Degree_Decimal_Minutes
            };
            string latStr = coordinate.Coordinate.Latitude.ToString(formattingOptions);
            string lonStr = coordinate.Coordinate.Longitude.ToString(formattingOptions);
            // enter LAT
            commands.AddRange(EnterUFCData(latStr + "\n"));
            // Down to LON
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_DOWN, 300, -1));
            // enter LNG
            commands.AddRange(EnterUFCData(lonStr + "\n"));
            // Down to ELEV
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_DOWN, 300, -1));
            // enter ELEV
            commands.AddRange(EnterUFCData(((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString() + "\n"));
            // Up to LAT again
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_UP, 300));
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_UP, 300));
            // Increment STPT
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DED_INC));
            return commands;
        }

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            return new List<string>() { "Waypoint" };
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>
        /// A list of valid point types.
        /// </returns>
        public override List<string> GetPointTypes()
        {
            return new List<string>() { "Waypoint" };
        }

        /// <summary>
        /// Gets the actions to be used after points have been entered.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        public override List<DCSCommand> GetPostPointActions()
        {
            List<DCSCommand> commands = new List<DCSCommand>()
            {
                new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_UP), // go back to STPT #
                new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_UP),
                
            };
            commands.AddRange(EnterUFCData(StartingWaypoint.ToString() + '\n')); // Enter the starting STPT
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_RTN, 500, -1)); // Go back to main page
            return commands;
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        public override List<DCSCommand> GetPrePointActions()
        {
            List<DCSCommand> commands = new List<DCSCommand>()
            {
                new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_RTN, 500, -1),  // return to main page
                new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_RTN, 200, 0),  // return to main page
                new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG4_STPT) // steerpoints page
            };
            // Enter start point
            commands.AddRange(EnterUFCData(StartingWaypoint.ToString() + "\n"));
            // select lat
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_DOWN, 100, -1));
            commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DCS_DOWN, 100, -1));
            return commands;
        }

        private List<DCSCommand> EnterUFCData(string data)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            foreach (char ch in data.ToUpper())
            {
                switch (ch)
                {
                    case '-':
                    case '0':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG0_M_SEL));
                        break;
                    case '1':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG1_T_ILS));
                        break;
                    case 'N':
                    case '2':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG2_ALOW));
                        break;
                    case '3':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG3));
                        break;
                    case 'W':
                    case '4':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG4_STPT));
                        break;
                    case '5':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG5_CRUS));
                        break;
                    case 'E':
                    case '6':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG6_TIME));
                        break;
                    case '7':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG7_MARK));
                        break;
                    case 'S':
                    case '8':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG8_FIX));
                        break;
                    case '9':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.DIG9_A_CAL));
                        break;
                    case '\n':
                        commands.Add(new DCSCommand(DEVICE_UFC, (int)EKeyCodes.ENTR));
                        break;
                    default:
                        // Ignore anything else like spaces or commas
                        break;
                }
            }
            return commands;
        }

        // As per devices.lua
        private const int DEVICE_UFC = 17;

        // As per command_defs.lua
        private enum EKeyCodes
        {
            UFC_SW = 3001,
            DIG0_M_SEL,
            DIG1_T_ILS,
            DIG2_ALOW,
            DIG3,
            DIG4_STPT,
            DIG5_CRUS,
            DIG6_TIME,
            DIG7_MARK,
            DIG8_FIX,
            DIG9_A_CAL,
            COM1,
            COM2,
            IFF,
            LIST,

            ENTR,
            RCL,
            AA,
            AG,

            RET_DEPR_Knob,
            CONT_Knob,
            SYM_Knob,
            BRT_Knob,

            Wx,
            FLIR_INC,
            FLIR_DEC,
            FLIR_GAIN_Sw,

            DRIFT_CUTOUT,
            WARN_RESET,

            DED_INC,
            DED_DEC,
            DCS_RTN,
            DCS_SEQ,
            DCS_UP,
            DCS_DOWN,

            F_ACK,
            IFF_IDENT,
            RF_Sw,

            // Input commands
            UFC_Sw_ITER,
            SYM_Knob_ITER,
            SYM_Knob_AXIS,
            RET_DEPR_Knob_ITER,
            RET_DEPR_Knob_AXIS,
            BRT_Knob_ITER,
            BRT_Knob_AXIS,
            CONT_Knob_ITER,
            CONT_Knob_AXIS,
            FLIR_GAIN_Sw_ITER,
            DriftCO_WarnReset_ITER,
            RF_Sw_ITER,
        }
    }
}
