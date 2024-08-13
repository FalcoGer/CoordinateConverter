using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents the A10C
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSAircraft" />
    public class A10C : DCSAircraft
    {
        /// <summary>
        /// Gets a value indicating whether the input into the jet is in MGRS format.
        /// </summary>
        /// <value>
        ///   <c>true</c> if MGRS format is used; otherwise, <c>false</c> for L/L.
        /// </value>
        private bool UsingMGRS { get; set; }

        private void readIsMGRSFromAircraft()
        {
            // set CDU mode
            var commands = new List<DCSCommand>
            {
                // Clear any previous scratchpad input)
                new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_Key_Clear),
                // Switch CDU to Page to Other
                new DCSCommand((int)EDevices.AAP, (int)EKeyCodes.AAP_Knob_PageSelect, 100, ((int)ECDUPageSelectPositions.Other) / 10.0, false),
                // Switch the CDU SteerPt to Mission
                new DCSCommand((int)EDevices.AAP, (int)EKeyCodes.AAP_Knob_SteerPt, 100, ((int)ECDUSteerPtSelectPositions.MissionPoints) / 10.0, false),
                // Press the WP button
                new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_WP),
                // Press the Waypoint option on R1
                new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_LSK_Line3_R1)
            };

            DCSCommand.RunAndSleep(commands);

            int cdu = (int)EDisplays.CDU;

            var message = new DCSMessage()
            {
                GetCockpitDisplayData = new List<int>() { cdu }
            };

            message = DCSConnection.SendRequest(message);

            var displayData = message.CockpitDisplayData[cdu];

            UsingMGRS = displayData.ContainsKey("WAYPTCoordFormat1"); // "WAYPTCoordFormat" when L/L
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="A10C"/> class.
        /// </summary>
        public A10C() {}

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            return new List<string>() { "Point" };
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
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="item">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetActions(object item)
        {
            CoordinateDataEntry coordinate = item as CoordinateDataEntry;
            var commands = new List<DCSCommand>();
            // var commands = new DebugCommandList();
            string label = GetLabelForPoint(coordinate.Name, coordinate.Id);
            if (firstPointEnteredLabel == null)
            {
                firstPointEnteredLabel = label;
            }
            int cdu = (int)EDevices.CDU;
            // Enter the label
            commands.AddRange(EnterIntoCDU(label));
            // Create a new point with that label or select the point that exists with that label
            commands.Add(new DCSCommand(cdu, (int)EKeyCodes.CDU_LSK_Line7_R3));

            // Always using MGRS for data entry, since it is faster.

            // Enter UTM Grid
            CoordinateSharp.MilitaryGridReferenceSystem mgrs = coordinate.Coordinate.MGRS;
            string utmGrid = mgrs.LongZone.ToString().PadLeft(2, '0') + mgrs.LatZone;
            commands.AddRange(EnterIntoCDU(utmGrid));
            commands.Add(new DCSCommand(cdu, (int)EKeyCodes.CDU_LSK_Line7_L3));

            // Enter digraph and easting/northing
            string diagraphAndEastingAndNorthing =
                mgrs.Digraph +
                ((int)Math.Round(mgrs.Easting)).ToString().PadLeft(5, '0') +
                ((int)Math.Round(mgrs.Northing)).ToString().PadLeft(5, '0');
            commands.AddRange(EnterIntoCDU(diagraphAndEastingAndNorthing));
            commands.Add(new DCSCommand(cdu, (int)EKeyCodes.CDU_LSK_Line9_L4));
            
            // Enter altitude
            if (!coordinate.AltitudeIsAGL || coordinate.AltitudeInFt != 0)
            {
                string altitudeString = Math.Abs((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString();
                // Can not rely on A10 ground database, enter manually
                commands.AddRange(EnterIntoCDU(altitudeString));
                commands.Add(new DCSCommand(cdu, (int)EKeyCodes.CDU_LSK_Line5_L2));
                // To enter a negative altitude, press the button again
                if (coordinate.GetAltitudeValue(true) < 0)
                {
                    commands.Add(new DCSCommand(cdu, (int)EKeyCodes.CDU_LSK_Line5_L2));
                }
            }
            return commands;
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPreActions()
        {
            firstPointEnteredLabel = null;

            readIsMGRSFromAircraft();

            var commands = new List<DCSCommand>(){};

            if (!UsingMGRS)
            {
                // put it temporarily into MGRS mode
                commands.Add(new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_LSK_Line9_R4));
            }

            return commands;
        }

        /// <summary>
        /// Gets the actions to be used after points have been entered.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPostActions()
        {
            List<DCSCommand> commands = new List<DCSCommand>(){};

            // Set MGRS back to user preference
            if (!UsingMGRS)
            {
                commands.Add(new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_LSK_Line9_R4));
            }

            // Put the page selector into steer point
            commands.Add(new DCSCommand((int)EDevices.AAP, (int)EKeyCodes.AAP_Knob_PageSelect, 100, ((int)ECDUPageSelectPositions.SteerPoint) / 10.0, false));
            
            // enter the first label
            commands.AddRange(EnterIntoCDU(firstPointEnteredLabel));
            // hit R1 to make that point the selected steer point
            commands.Add(new DCSCommand((int)EDevices.CDU, (int)EKeyCodes.CDU_LSK_Line3_R1));
            firstPointEnteredLabel = null;
            return commands;
        }

        private List<DCSCommand> EnterIntoCDU(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            List <DCSCommand> commands = new List<DCSCommand>();
            int dev = (int)EDevices.CDU;
            foreach (char ch in data.ToLower())
            {
                switch (ch)
                {
                    case '.':
                    case ',':
                        commands.Add(new DCSCommand(dev, (int)EKeyCodes.CDU_Key_Point));
                        break;
                    case '/':
                        commands.Add(new DCSCommand(dev, (int)EKeyCodes.CDU_Key_Slash));
                        break;
                    case '0':
                        // Needs to be separate because cdu key 0 is above key 9's value, but ascii '0' is below '1'
                        commands.Add(new DCSCommand(dev, (int)EKeyCodes.CDU_Key_0));
                        break;
                    case ' ':
                        commands.Add(new DCSCommand(dev, (int)EKeyCodes.CDU_Key_Space));
                        break;
                    default:
                        int keyCode;
                        if (ch >= '1' && ch <= '9')
                        {
                            keyCode = ch - '1' + (int)EKeyCodes.CDU_Key_1;
                        }
                        else if (ch >= 'a' && ch <= 'z')
                        {
                            keyCode = ch - 'a' + (int)EKeyCodes.CDU_Key_A;
                        }
                        else
                        {
                            continue;
                        }
                        commands.Add(new DCSCommand(dev, keyCode));
                        break;
                }
            }
            return commands;
        }

        /// <summary>
        /// Gets the label for the A10 given a point label. The A10 can have a maximum of 12 characters per label.<br/>
        /// Additionally the point labels must be unique, otherwise points will be overwritten.
        /// </summary>
        /// <param name="label">The label for the original point.</param>
        /// <param name="id">The identifier of the point is used to deconflict names.</param>
        /// <returns></returns>
        public static string GetLabelForPoint(string label, int id)
        {
            // maximum length is 12, cut it down
            // Needs M in front because if entered as a point label starting with a number, A10 will prepend M anyway and throw away the last character.
            string prefix = "M" + id.ToString().PadLeft(2, '0') + "/";
            label = label.Trim();
            if (string.IsNullOrEmpty(label))
            {
                return prefix.Substring(0, prefix.Length - 1);
            }
            label = label.Length <= (12 - prefix.Length) ? label : label.Substring(0, 12 - prefix.Length);
            label = prefix + label;
            return label;
        }

        private string firstPointEnteredLabel = null;

        private enum EDisplays
        {
            MFCDLeft = 1,
            MFCDRight = 2,
            CDU = 3,
            DigitalClock = 4,
            HUD = 5,
            EW_CMDS = 7,
            EW_UFC = 8,
            HMCD = 17,
            ARC210 = 18
        }

        private enum EDevices
        {
            MFCDLeft = 2,
            MFCDRight = 3,
            UFC = 8,
            CDU = 9,
            /// <summary>
            /// The Avionics Power Panel (CDU power)
            /// </summary>
            AAP = 22
        }

        // according to clickabledata.lua (Button_# is defined in command_defs.lua, in short Button_# is 3000 + button number)
        private enum EKeyCodes
        {
            CDU_LSK_Line3_L1 = 3001,
            CDU_LSK_Line5_L2,
            CDU_LSK_Line7_L3,
            CDU_LSK_Line9_L4,
            CDU_LSK_Line3_R1,
            CDU_LSK_Line5_R2,
            CDU_LSK_Line7_R3,
            CDU_LSK_Line9_R4,
            CDU_SYS,
            CDU_NAV,
            CDU_WP,
            CDU_OSET,
            CDU_FPM,
            CDU_PREV,
            CDU_Key_1,
            CDU_Key_2,
            CDU_Key_3,
            CDU_Key_4,
            CDU_Key_5,
            CDU_Key_6,
            CDU_Key_7,
            CDU_Key_8,
            CDU_Key_9,
            CDU_Key_0,
            CDU_Key_Point,
            CDU_Key_Slash,
            CDU_Key_A,
            CDU_Key_B,
            CDU_Key_C,
            CDU_Key_D,
            CDU_Key_E,
            CDU_Key_F,
            CDU_Key_G,
            CDU_Key_H,
            CDU_Key_I,
            CDU_Key_J,
            CDU_Key_K,
            CDU_Key_L,
            CDU_Key_M,
            CDU_Key_N,
            CDU_Key_O,
            CDU_Key_P,
            CDU_Key_Q,
            CDU_Key_R,
            CDU_Key_S,
            CDU_Key_T,
            CDU_Key_U,
            CDU_Key_V,
            CDU_Key_W,
            CDU_Key_X,
            CDU_Key_Y,
            CDU_Key_Z,
            CDU_Key_NoFunction1,
            CDU_Key_NoFunction2,
            CDU_Key_MK,
            CDU_Key_Backspace,
            CDU_Key_Space,
            CDU_Key_Clear,
            CDU_Key_FaultAcknowledge,
            CDU_Key_DimBrt_L,
            CDU_Key_DimBrt_R,
            CDU_Key_PgUp,
            CDU_Key_PgDown,
            CDU_Key_Blank_L,
            CDU_Key_Blank_R,
            CDU_Key_DataIncrement,
            CDU_Key_DataDecrement,
            AAP_Knob_SteerPt = 3001,
            AAP_2WaySwitch_SteerPtIncrement,
            AAP_2WaySwitch_SteerPtDecrement,
            AAP_Knob_PageSelect,
            AAP_Switch_CDU_Power,
            AAP_Switch_EGI_Power
        }

        private enum ECDUSteerPtSelectPositions
        {
            FlightPlanPoints,
            MarkPoints,
            MissionPoints
        }

        private enum  ECDUPageSelectPositions
        {
            Other,
            Position,
            SteerPoint,
            WayPoint
        }
    }
}
