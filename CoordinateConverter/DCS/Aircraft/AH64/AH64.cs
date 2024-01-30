using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// This class represents the AH64 aircraft
    /// </summary>
    /// <seealso cref="DCSAircraft" />
    public class AH64 : DCSAircraft
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is in the pilot or CPG seat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pilot; otherwise, <c>false</c>.
        /// </value>
        public bool IsPilot { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AH64"/> class.
        /// </summary>
        /// <param name="isPilot">if set to <c>true</c> [is pilot].</param>
        public AH64(bool isPilot)
        {
            IsPilot = isPilot;
        }

        /// <summary>
        /// Device codes
        /// </summary>
        public enum EDeviceCode
        {
            /// <summary>
            /// The pilot's hotas controls
            /// </summary>
            PLT_HOTAS = 23,
            /// <summary>
            /// The copilot's hotas controls
            /// </summary>
            CPG_HOTAS = 24,
            /// <summary>
            /// The pilot's Keyboard Unit
            /// </summary>
            PLT_KU = 29,
            /// <summary>
            /// The copilot's Keyboard Unit
            /// </summary>
            CPG_KU = 30,
            /// <summary>
            /// The pilot's left MFD
            /// </summary>
            PLT_LMFD = 42,
            /// <summary>
            /// The pilot's right MFD
            /// </summary>
            PLT_RMFD = 43,
            /// <summary>
            /// The copilot's left MFD
            /// </summary>
            CPG_LMFD = 44,
            /// <summary>
            /// The copilot's right MFD
            /// </summary>
            CPG_RMFD = 45,
            /// <summary>
            /// The pilots up front display
            /// </summary>
            PLT_EUFD = 48,
            /// <summary>
            /// The copilots up front display
            /// </summary>
            CPG_EUFD = 49,
            /// <summary>
            /// The tedac
            /// </summary>
            TEDAC = 51

        }

        /// <summary>
        /// Key codes
        /// </summary>
        public enum EKeyCode : int
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            KU_ENT = 3006,
            KU_CLR = 3001,
            KU_SPC = 3003,
            KU_DOT = 3042,
            KU_COMMA = 3042,
            KU_DIV = 3045,
            KU_SUB = 3047,
            KU_ADD = 3046,
            KU_0 = 3043,
            KU_1 = 3033,
            KU_2 = 3034,
            KU_3 = 3035,
            KU_4 = 3036,
            KU_5 = 3037,
            KU_6 = 3038,
            KU_7 = 3039,
            KU_8 = 3040,
            KU_9 = 3041,
            KU_A = 3007,
            KU_B = 3008,
            KU_C = 3009,
            KU_D = 3010,
            KU_E = 3011,
            KU_F = 3012,
            KU_G = 3013,
            KU_H = 3014,
            KU_I = 3015,
            KU_J = 3016,
            KU_K = 3017,
            KU_L = 3018,
            KU_M = 3019,
            KU_N = 3020,
            KU_O = 3021,
            KU_P = 3022,
            KU_Q = 3023,
            KU_R = 3024,
            KU_S = 3025,
            KU_T = 3026,
            KU_U = 3027,
            KU_V = 3028,
            KU_W = 3029,
            KU_X = 3030,
            KU_Y = 3031,
            KU_Z = 3032,

            // MFD
            MFD_T1 = 3001,
            MFD_T2 = 3002,
            MFD_T3 = 3003,
            MFD_T4 = 3004,
            MFD_T5 = 3005,
            MFD_T6 = 3006,

            MFD_R1 = 3007,
            MFD_R2 = 3008,
            MFD_R3 = 3009,
            MFD_R4 = 3010,
            MFD_R5 = 3011,
            MFD_R6 = 3012,

            MFD_B6 = 3013,
            MFD_B5 = 3014,
            MFD_B4 = 3015,
            MFD_B3 = 3016,
            MFD_B2 = 3017,
            MFD_B1_M = 3018,

            MFD_L6 = 3019,
            MFD_L5 = 3020,
            MFD_L4 = 3021,
            MFD_L3 = 3022,
            MFD_L2 = 3023,
            MFD_L1 = 3024,

            MFD_ASTERISK = 3025,
            MFD_VID = 3026,
            MFD_COM = 3027,
            MFD_AC = 3028,
            MFD_TSD = 3029,
            MFD_WPN = 3030,
            MFD_FCR = 3031,

            MFD_BRT_KNOB = 3032,
            MFD_VID_KNOB = 3033,
            MFD_MODE_KNOB = 3034,

            // Input commands
            BRT_KNOB_ITER = 3035,
            BRT_KNOB_AXIS = 3036,
            VID_KNOB_ITER = 3037,
            VID_KNOB_AXIS = 3038,
            MODE_KNOB_ITER = 3039,

            // EUFD
            EUFD_WCA_UP = 3001,
            EUFD_WCA_DOWN = 3002,
            EUFD_IDM_UP = 3003,
            EUFD_IDM_DOWN = 3004,
            EUFD_RTS_UP = 3005,
            EUFD_RTS_DOWN = 3006,
            EUFD_Preset = 3007,
            EUFD_Enter = 3008,
            EUFD_Stopwatch = 3009,
            EUFD_Swap = 3010,
            EUFD_Brightness_Knob = 3011,

            // Cyclic
            CYCLIC_TRIGGER_GUARD = 3001,
            CYCLIC_TRIGGER_1ST_DETENT,
            CYCLIC_TRIGGER_2ND_DETENT,
            CYCLIC_TRIM_HOLD_SW_UP,
            CYCLIC_TRIM_HOLD_SW_DOWN,
            CYCLIC_TRIM_HOLD_SW_LEFT,
            CYCLIC_TRIM_HOLD_SW_RIGHT,
            CYCLIC_WEAPONS_ACTION_SW_UP,
            CYCLIC_WEAPONS_ACTION_SW_DOWN,
            CYCLIC_WEAPONS_ACTION_SW_LEFT,
            CYCLIC_WEAPONS_ACTION_SW_RIGHT,
            CYCLIC_SYMBOLOGY_SELECT_SW_UP,
            CYCLIC_SYMBOLOGY_SELECT_SW_DOWN,
            CYCLIC_SYMBOLOGY_SELECT_SW_DEPRESS,
            CYCLIC_CMDS_SW_FWD,
            CYCLIC_CMDS_SW_AFT,
            CYCLIC_RTS_SW_LEFT,
            CYCLIC_RTS_SW_RIGHT,
            CYCLIC_RTS_SW_DEPRESS,
            CYCLIC_FMC_RELEASE_SW,
            CYCLIC_CHAFF_DISPENCE_BTN,
            CYCLIC_FLARE_DISPENCE_BTN,
            CYCLIC_ATA_CAGE_UNCAGE_BTN,
            // Collective mission grip
            MISSION_FCR_SCAN_SIZE_SW_UP,
            MISSION_FCR_SCAN_SIZE_SW_DOWN,
            MISSION_FCR_SCAN_SIZE_SW_LEFT,
            MISSION_FCR_SCAN_SIZE_SW_RIGHT,
            MISSION_SIGHT_SELECT_SW_UP,
            MISSION_SIGHT_SELECT_SW_DOWN,
            MISSION_SIGHT_SELECT_SW_LEFT,
            MISSION_SIGHT_SELECT_SW_RIGHT,
            MISSION_FCR_MODE_SW_UP,
            MISSION_FCR_MODE_SW_DOWN,
            MISSION_FCR_MODE_SW_LEFT,
            MISSION_FCR_MODE_SW_RIGHT,
            MISSION_CURSOR_UP,
            MISSION_CURSOR_DOWN,
            MISSION_CURSOR_LEFT,
            MISSION_CURSOR_RIGHT,
            MISSION_CURSOR_ENTER,
            MISSION_ALTERNATE_CURSOR_ENTER,
            MISSION_CURSOR_AXIS_X,
            MISSION_CURSOR_AXIS_Y,

            MISSION_CURSOR_DISPLAY_SELECT_BTN,
            MISSION_FCR_SCAN_SW_SINGLE,
            MISSION_FCR_SCAN_SW_CONTINUOUS,
            MISSION_CUED_SEARCH_SW,
            MISSION_MISSILE_ADVANCE_SW,

            // Collective Flight Grip
            FLIGHT_EMERGENCY_JETTISON_SW_GUARD,
            FLIGHT_EMERGENCY_JETTISON_SW,

            FLIGHT_NVS_SELECT_SW_TADS,
            FLIGHT_NVS_SELECT_SW_PNVS,
            FLIGHT_BORESIGHT_POLARITY_SW_BS,
            FLIGHT_BORESIGHT_POLARITY_SW_PLRT,

            FLIGHT_STABILATOR_CONTROL_SW_NU,	    // NOSE UP
            FLIGHT_STABILATOR_CONTROL_SW_ND,	    // NOSE DOWN
            FLIGHT_STABILATOR_CONTROL_SW_DEPRESS,	// STABILATOR AUTO MODE

            FLIGHT_SEARCHLIGHT_SW_UP,	            // ON/OFF
            FLIGHT_SEARCHLIGHT_SW_DOWN,	        // STOW/OFF

            FLIGHT_SEARCHLIGHT_POSITION_SW_UP,	    // EXT
            FLIGHT_SEARCHLIGHT_POSITION_SW_DOWN,	// RET
            FLIGHT_SEARCHLIGHT_POSITION_SW_LEFT,	// L
            FLIGHT_SEARCHLIGHT_POSITION_SW_RIGHT,	// R

            FLIGHT_CHOP_BTN_GUARD,
            FLIGHT_CHOP_BTN,

            FLIGHT_TAIL_WHEEL_BTN,	            // LOCK/UNLOCK

            FLIGHT_BUCS_TRIGGER_GUARD,	        // only in CPG
            FLIGHT_BUCS_TRIGGER,                    // only in CPG

            // TEDAC / TDU
            // Display Unit
            TDU_MODE_KNOB = 3001,
            TDU_GAIN_KNOB,
            TDU_LEV_KNOB,
            TDU_ASTERISK_BTN,
            // Video Select Buttons
            TDU_VIDEO_SELECT_TAD_BTN,
            TDU_VIDEO_SELECT_FCR_BTN,
            TDU_VIDEO_SELECT_PNV_BTN,
            TDU_VIDEO_SELECT_GS_BTN,
            // Bezel Controls
            TDU_SYM_INC,
            TDU_SYM_DEC,
            TDU_BRT_INC,
            TDU_BRT_DEC,
            TDU_CON_INC,
            TDU_CON_DEC,
            TDU_AZ_LEFT,
            TDU_AZ_RIGHT,
            TDU_EL_UP,
            TDU_EL_DOWN,
            TDU_RF_UP,
            TDU_RF_DOWN,
            // Bottom Buttons
            TDU_B1, // AZ/EL
            TDU_B2, // ACM
            TDU_B3, // FREEZE
            TDU_B4, // FILTER

            // input commands
            TDU_MODE_KNOB_ITER,
            TDU_GAIN_KNOB_ITER,
            TDU_GAIN_KNOB_AXIS,
            TDU_LEV_KNOB_ITER,
            TDU_LEV_KNOB_AXIS,

            // Left Hand Grip ---------------------------------
            LHG_IAT_OFS_SW_IAT, // Image Auto Track / Offset
            LHG_IAT_OFS_SW_OFS, // Image Auto Track / Offset

            LHG_TADS_FOV_SW_Z,  // Zoom
            LHG_TADS_FOV_SW_M,  // Medium
            LHG_TADS_FOV_SW_N,  // Narrow
            LHG_TADS_FOV_SW_W,  // Wide

            LHG_TADS_SENSOR_SELECT_SW_FLIR,
            LHG_TADS_SENSOR_SELECT_SW_TV,
            LHG_TADS_SENSOR_SELECT_SW_DVO,  // ORT only

            LHG_STORE_UPDT_SW_STORE,
            LHG_STORE_UPDT_SW_UPDT,

            LHG_FCR_SCAN_SW_S,  // single (as in collective mission grip)
            LHG_FCR_SCAN_SW_C,  // continuous (as in collective mission grip)

            LHG_CUED_SEARCH_BTN,    // (as in collective mission grip)
            LHG_LMC_BTN,    // Linear Motion Compensation

            LHG_FCR_MODE_SW_UP, // (GTM) Ground Targeting Mode		(as in collective mission grip)
            LHG_FCR_MODE_SW_DOWN,   // (ATM) Air Targeting Mode			(as in collective mission grip)
            LHG_FCR_MODE_SW_LEFT,   // (TPM) Terrain Profile Mode		(as in collective mission grip)
            LHG_FCR_MODE_SW_RIGHT,  // (RMAP) Radar MAP					(as in collective mission grip)

            LHG_WEAPONS_ACTION_SW_UP,   // (G) GUN				(as in collective mission grip)
            LHG_WEAPONS_ACTION_SW_DOWN, // (A) AIR-TO-AIR		(as in collective mission grip)
            LHG_WEAPONS_ACTION_SW_LEFT, // (R) ROCKET			(as in collective mission grip)
            LHG_WEAPONS_ACTION_SW_RIGHT,    // (M) MISSILE			(as in collective mission grip)

            LHG_CURSOR_UP,
            LHG_CURSOR_DOWN,
            LHG_CURSOR_LEFT,
            LHG_CURSOR_RIGHT,
            LHG_CURSOR_ENTER,
            LHG_CURSOR_AXIS_X,
            LHG_CURSOR_AXIS_Y,

            LHG_LR_BTN, // (L/R Switch) Cursor Display Select Btn
            LHG_WEAPON_TRIGGER_1ST_DETENT,
            LHG_WEAPON_TRIGGER_2ND_DETENT,

            // Right Hand Grip --------------------------------
            RHG_SIGHT_SELECT_SW_UP, // (HMD)		(as in collective mission grip)
            RHG_SIGHT_SELECT_SW_DOWN,   // (LINK)		(as in collective mission grip)
            RHG_SIGHT_SELECT_SW_LEFT,   // (FCR)		(as in collective mission grip)
            RHG_SIGHT_SELECT_SW_RIGHT,  // (TADS)		(as in collective mission grip)

            RHG_LT_SW_A,    // Automatic	Laser Tracker Switch
            RHG_LT_SW_O,    // Off
            RHG_LT_SW_M,    // Manual

            RHG_FCR_SCAN_SIZE_SW_UP,    // (Z) ZOOM
            RHG_FCR_SCAN_SIZE_SW_DOWN,  // (M) MEDIUM
            RHG_FCR_SCAN_SIZE_SW_LEFT,  // (N) NARROW
            RHG_FCR_SCAN_SIZE_SW_RIGHT, // (W) WIDE

            RHG_C_SCOPE_SW,
            RHG_FLIR_PLRT_BTN,
            RHG_SIGHT_SLAVE_BTN,
            RHG_DISPLAY_ZOOM_BTN,
            RHG_LRFD_TRIGGER,

            RHG_SPARE_SW_FWD,   // MTADS only, MTT Switch (Multi Target Tracker, 3-pos, rocker)
            RHG_SPARE_SW_AFT,   // MTADS only, MTT Switch (Multi Target Tracker, 3-pos, rocker)

            RHG_HDD_SW, // HDD/HOD
            RHG_ENTER_BTN,

            RHG_MAN_TRK_UP, // Sensor (Sight) Manual Tracker
            RHG_MAN_TRK_DOWN,
            RHG_MAN_TRK_LEFT,
            RHG_MAN_TRK_RIGHT,
            RHG_MAN_TRK_AXIS_X,
            RHG_MAN_TRK_AXIS_Y,

            RHG_IAT_POLARITY_W, // White
            RHG_IAT_POLARITY_A, // Auto
            RHG_IAT_POLARITY_B, // Black

            // additional commands
            LHG_TADS_SENSOR_SELECT_SW,  // for clickability
            RHG_LT_SW,  // for clickability
            RHG_IAT_POLARITY_SW,    // for clickability

            RHG_MAN_TRK_DOWN_LEFT,  // for input
            RHG_MAN_TRK_DOWN_RIGHT, // for input
            RHG_MAN_TRK_UP_LEFT,    // for input
            RHG_MAN_TRK_UP_RIGHT,	// for input
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }


        /// <summary>
        /// Gets the actions to be added for each item.
        /// </summary>
        /// <param name="item">The item for which the commands are generated.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.ArgumentException">Bad Point Type</exception>
        protected override List<DCSCommand> GetActions(object item)
        {
            CoordinateDataEntry coordinate = item as CoordinateDataEntry;
            AH64SpecificData extraData = null;
            EKeyCode keyMFDPointType = EKeyCode.MFD_L3; // assume waypoint
            string ident = "\n"; // assume default ident
            if (coordinate.AircraftSpecificData.ContainsKey(typeof(AH64)) && coordinate.AircraftSpecificData[typeof(AH64)] != null)
            {
                extraData = coordinate.AircraftSpecificData[typeof(AH64)] as AH64SpecificData;
                ident = extraData.Ident.ToString().Substring(3) + '\n';

                switch (extraData.PointType)
                {
                    case EPointType.Waypoint:
                        keyMFDPointType = EKeyCode.MFD_L3;
                        break;
                    case EPointType.Hazard:
                        keyMFDPointType = EKeyCode.MFD_L4;
                        break;
                    case EPointType.ControlMeasure:
                        keyMFDPointType = EKeyCode.MFD_L5;
                        break;
                    case EPointType.Target:
                        keyMFDPointType = EKeyCode.MFD_L6;
                        break;
                    default:
                        throw new ArgumentException("Bad Point Type");
                }
            }

            // DebugCommandList commands = new DebugCommandList
            List<DCSCommand> commands = new List<DCSCommand>
            {
                // press ADD
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.MFD_L2),
                // press the waypoint type button
                extraData == null ? null : new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)keyMFDPointType),
                // press ident
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.MFD_L1)
            };
            // enter ident
            commands.AddRange(GetCommandsForKUText(ident, false, IsPilot));
            // enter free text (max 3 chars)
            commands.AddRange(GetCommandsForKUText((coordinate.Name.Length <= 3 ? coordinate.Name : coordinate.Name.Substring(0, 3)) + '\n', false, IsPilot));
            // enter MGRS coordinates
            // remove spaces and append enter
            string mgrsString = string.Join(string.Empty ,coordinate.GetCoordinateStrMGRS(4).Where(ch => ch != ' ')) + '\n';
            commands.AddRange(GetCommandsForKUText(mgrsString, true, IsPilot));

            // enter altitude
            if (coordinate.AltitudeInFt == 0 && coordinate.AltitudeIsAGL)
            {
                commands.AddRange(GetCommandsForKUText("\n", false, IsPilot));
            }
            else
            {
                commands.AddRange(GetCommandsForKUText(((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString() + "\n", true, IsPilot));
            }
            return commands;
        }

        /// <summary>
        /// Checks if the text is valid for entry into the keyboard unit.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="validChars">The valid chars.</param>
        /// <returns>Whether the text is valid.</returns>
        public static bool GetIsValidTextForKU(string text, uint minLength = 1, uint maxLength = 32, List<char> validChars = null)
        {
            if (validChars is null)
            {
                validChars = new List<char>();
                for (char ch = '0'; ch <= '9'; ch++)
                {
                    validChars.Add(ch);
                }
                for (char ch = 'A'; ch <= 'Z'; ch++)
                {
                    validChars.Add(ch);
                }
                validChars.AddRange(new List<char>() { '\n', '+', '-', '*', '/', ' ', '.', ',' });
            }

            if (string.IsNullOrEmpty(text)) { return false; }
            if (text.Length < minLength) { return false; }
            if (text.Length > maxLength) { return false; }
            foreach (char textCh in text)
            {
                if (!validChars.Contains(textCh))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the commands for ku text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="clearFirst">if set to <c>true</c> the KU input is cleared first by use of the CLR button.</param>
        /// <param name="IsPilot">if set to <c>true</c> will use the pilot's keyboard unit, otherwise the CPGs</param>
        /// <returns>The list of commands that is required to enter the text into the KU</returns>
        public static List<DCSCommand> GetCommandsForKUText(string text, bool clearFirst, bool IsPilot)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            // DebugCommandList commands = new DebugCommandList();
            if (text == null)
            {
                return commands;
            }

            if (clearFirst)
            {
                commands.Add(new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_KU : EDeviceCode.CPG_KU), (int)EKeyCode.KU_CLR));
            }
            // type the text
            EKeyCode? prevKeyCode = null;
            foreach (char ch in text.ToUpper())
            {
                EKeyCode? keyCode = null;
                // handle special cases
                switch (ch)
                {
                    case '\n':
                        keyCode = EKeyCode.KU_ENT;
                        break;
                    case ' ':
                        keyCode = EKeyCode.KU_SPC;
                        break;
                    case '.':
                        keyCode = EKeyCode.KU_DOT;
                        break;
                    case ',':
                        keyCode = EKeyCode.KU_COMMA;
                        break;
                    case '/':
                        keyCode = EKeyCode.KU_DIV;
                        break;
                    case '-':
                        keyCode = EKeyCode.KU_SUB;
                        break;
                    case '+':
                        keyCode = EKeyCode.KU_ADD;
                        break;
                    default:
                        if ((ch >= 'A' && ch <= 'Z') || (ch <= '9' && ch >= '0'))
                        {
                            keyCode = (EKeyCode)Enum.Parse(typeof(EKeyCode), "KU_" + ch, true);
                            break;
                        }
                        else
                        {
                            break;
                        }
                }
                if (keyCode != null)
                {
                    // Double presses of the same button need to have the be slower, otherwise they don't register.
                    if (prevKeyCode == keyCode && prevKeyCode != null && commands.Count > 0)
                    {
                        commands.Last().Delay = 250;
                    }
                    commands.Add(new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_KU : EDeviceCode.CPG_KU), (int)keyCode.Value));
                    prevKeyCode = keyCode;
                }
            }
            return commands;
        }

        /// <summary>
        /// Presses TSD button to reset the screen
        /// </summary>
        /// <returns>
        /// A list of actions that result in TSD being pressed.
        /// </returns>
        protected override List<DCSCommand> GetPostActions()
        {
            return new List<DCSCommand>()
            {
                // press TSD to reset the screen
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.MFD_TSD)
            };
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPreActions()
        {
            return new List<DCSCommand>
            {
                // press TSD
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.MFD_TSD),
                // go to point page
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.MFD_B6),
                // clear KU
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_KU : EDeviceCode.CPG_KU), (int)EKeyCode.KU_CLR),
            };
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>A list of valid point types.</returns>
        public override List<string> GetPointTypes()
        {
            return Enum.GetNames(typeof(EPointType)).ToList();
        }

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes"/>.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>A list of names for point options.</returns>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            EPointType pointType = (EPointType)Enum.Parse(typeof(EPointType),pointTypeStr);

            switch (pointType)
            {
                case EPointType.Waypoint:
                    return EWPOptionDescriptions.Values.ToList();
                case EPointType.Hazard:
                    return EHZOptionDescriptions.Values.ToList();
                case EPointType.ControlMeasure:
                    return ECMOptionDescriptions.Values.ToList();
                case EPointType.Target:
                    return ETGOptionDescriptions.Values.ToList();
                default:
                    throw new Exception("Invalid pointType");
            }
        }

        /// <summary>
        /// Clears the points of the specified type between the starting and end index (inclusive).
        /// </summary>
        /// <param name="pointType">Type of the point to clear.</param>
        /// <param name="startIdx">The first point to delete</param>
        /// <param name="endIdx">The last point to delete</param>
        /// <returns>Number of commands generated.</returns>
        public int ClearPoints(EPointType pointType, int startIdx, int endIdx)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            int deviceId = IsPilot ? (int)EDeviceCode.PLT_RMFD : (int)EDeviceCode.CPG_RMFD;

            for (int pointIdx = startIdx; pointIdx <= endIdx ; pointIdx++)
            {
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_TSD)); // Reset to TSD after every point, to avoid weirdness.
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_B6)); // Point
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_L1)); // Point >
                commands.AddRange(GetCommandsForKUText(pointType.ToString().First() + pointIdx.ToString() + "\n", true, IsPilot)); // Enter point identifier
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_L4)); // Del
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_L3)); // Yes
            }
            commands.Add(new DCSCommand(deviceId, (int)EKeyCode.MFD_TSD)); // reset to TSD
            DCSMessage message = new DCSMessage() { Commands = commands };
            _ = DCSConnection.SendRequest(message);
            return commands.Count;
        }

        /// <summary>
        /// The valid point types for the AH64
        /// </summary>
        public enum EPointType
        {
            #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Waypoint, Hazard, ControlMeasure, Target
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// The valid point Idents, with Point type prepended and separated with an underscore ('_')
        /// </summary>
        public enum EPointIdent
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            WP_WP, WP_LZ, WP_SP, WP_CC, WP_PP, WP_RP,
            HZ_TO,HZ_TU,HZ_WL,HZ_WS,
            CM_AP, CM_AG, CM_AI, CM_AL, CM_F1, CM_F2, CM_AA, CM_BN, CM_BP, CM_BR, CM_BD, CM_CP, CM_CO, CM_CR, CM_DI, CM_FF, CM_FM, CM_FC, CM_FA,
            CM_GL, CM_HA, CM_ID, CM_NB, CM_BE, CM_RH, CM_GP, CM_US,
            CM_AD, CM_AS, CM_AV, CM_AB, CM_AM, CM_CA, CM_MA, CM_CF, CM_DF, CM_EN, CM_FW, CM_WF, CM_FL, CM_AH, CM_FG, CM_HO, CM_FI, CM_MI, CM_MD, CM_TF, CM_FU,
            CM_ES, CM_EV, CM_ED, CM_EB, CM_EC, CM_AE, CM_ME, CM_CE, CM_DE, CM_EE, CM_WR, CM_EF, CM_WE, CM_EK, CM_HG, CM_EH, CM_EI, CM_EM, CM_EX, CM_ET, CM_EU,
            TG_AX, TG_AS, TG_AD, TG_GP, TG_G1, TG_G2, TG_G3, TG_G4, TG_SD, TG_83, TG_U, TG_S6, TG_AA, TG_GU, TG_MK, TG_SB, TG_GS, TG_GT, TG_ZU, TG_NV, TG_SR,
            TG_TR, TG_70, TG_BP, TG_BH, TG_CH, TG_CT, TG_C2, TG_HK, TG_JA, TG_PT, TG_RE, TG_RA, TG_RO, TG_1, TG_2, TG_3, TG_4, TG_5, TG_6, TG_7, TG_8, TG_9,
            TG_10, TG_11, TG_12, TG_13, TG_14, TG_15, TG_16, TG_17, TG_SM, TG_SC, TG_SP, TG_SH, TG_SS, TG_TC, TG_ST, TG_SA, TG_VU, TG_TG
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// The descriptions for WP idents
        /// </summary>
        public static readonly Dictionary<EPointIdent, string> EWPOptionDescriptions = new Dictionary<EPointIdent, string>()
        {
            { EPointIdent.WP_WP, "*Waypoint" },
            { EPointIdent.WP_LZ, "Landing Zone" },
            { EPointIdent.WP_PP, "Passage Point" },
            { EPointIdent.WP_RP, "Release Point" },
            { EPointIdent.WP_SP, "Start Point" },
            { EPointIdent.WP_CC, "Comm Check Point" }
        };

        /// <summary>
        /// The descriptions for HZ idents
        /// </summary>
        public static readonly Dictionary<EPointIdent, string> EHZOptionDescriptions = new Dictionary<EPointIdent, string>()
        {
            { EPointIdent.HZ_TO, "Tower > 1000" },
            { EPointIdent.HZ_TU, "*Tower < 1000" },
            { EPointIdent.HZ_WL, "Wires Power" },
            { EPointIdent.HZ_WS, "Wires Tele/Elec" }
        };

        /// <summary>
        /// The descriptions for CM idents
        /// </summary>
        public static readonly Dictionary<EPointIdent, string> ECMOptionDescriptions = new Dictionary<EPointIdent, string>()
        {
            // General (Green)
            { EPointIdent.CM_AP, "Air Control Point" },
            { EPointIdent.CM_AG, "Airfield (General)" },
            { EPointIdent.CM_AI, "Airfield (Instrument)" },
            { EPointIdent.CM_AL, "Airfield (Lighted)" },
            { EPointIdent.CM_F1, "Artillery Fire Point 1" },
            { EPointIdent.CM_F2, "Artillery Fire Point 2" },
            { EPointIdent.CM_AA, "Assembly Area" },
            { EPointIdent.CM_BN, "Battalion" },
            { EPointIdent.CM_BP, "Battle Position" },
            { EPointIdent.CM_BR, "Bridge or Gap" },
            { EPointIdent.CM_BD, "Brigade" },
            { EPointIdent.CM_CP, "*Check Point" },
            { EPointIdent.CM_CO, "Company" },
            { EPointIdent.CM_CR, "Corps" },
            { EPointIdent.CM_DI, "Division" },
            { EPointIdent.CM_FF, "FARP (Fuel)" },
            { EPointIdent.CM_FM, "FARP (Munitions)" },
            { EPointIdent.CM_FC, "FARP (F & M)" },
            { EPointIdent.CM_FA, "Forward Assembly Area" },
            { EPointIdent.CM_GL, "Ground Lights / Small Town" },
            { EPointIdent.CM_HA, "Holding Area" },
            { EPointIdent.CM_ID, "IDM Subscriber" },
            { EPointIdent.CM_BE, "NDB Symbol" },
            { EPointIdent.CM_NB, "NBC Contaminated Area" },
            { EPointIdent.CM_RH, "Rail Head" },
            { EPointIdent.CM_GP, "Regiment/Group" },
            { EPointIdent.CM_US, "US Army" },
            // Friendly (Blue)
            { EPointIdent.CM_AD, "FND ADU" },
            { EPointIdent.CM_AS, "FND Air Assault" },
            { EPointIdent.CM_AV, "FND Airborne Cav" },
            { EPointIdent.CM_AB, "FND Airborne" },
            { EPointIdent.CM_AM, "FND Armor" },
            { EPointIdent.CM_CA, "FND CAV Armor" },
            { EPointIdent.CM_MA, "FND AV Maintenance" },
            { EPointIdent.CM_CF, "FND Chemical" },
            { EPointIdent.CM_DF, "FND Decontamination" },
            { EPointIdent.CM_EN, "FND Engineers" },
            { EPointIdent.CM_FW, "FND EW" },
            { EPointIdent.CM_FL, "FND Field Artillery" },
            { EPointIdent.CM_WF, "FND Fixed Wing" },
            { EPointIdent.CM_AH, "FND Attack Helicopter" },
            { EPointIdent.CM_FG, "FND General Helicopter" },
            { EPointIdent.CM_HO, "FND Hospital" },
            { EPointIdent.CM_FI, "FND Infantry" },
            { EPointIdent.CM_MI, "FND Mechanized" },
            { EPointIdent.CM_MD, "FND Medical" },
            { EPointIdent.CM_TF, "FND TOC" },
            { EPointIdent.CM_FU, "FND Generic Unit" },
            // Hostile (Red)
            { EPointIdent.CM_ED, "EMY ADU" },
            { EPointIdent.CM_ES, "EMY Air Assault" },
            { EPointIdent.CM_EV, "EMY Airborne Cav" },
            { EPointIdent.CM_EB, "EMY Airborne" },
            { EPointIdent.CM_AE, "EMY Armor" },
            { EPointIdent.CM_EC, "EMY CAV Armor" },
            { EPointIdent.CM_ME, "EMY AV Maintenance" },
            { EPointIdent.CM_CE, "EMY Chemical" },
            { EPointIdent.CM_DE, "EMY Decontamination" },
            { EPointIdent.CM_EE, "EMY Engineers" },
            { EPointIdent.CM_WR, "EMY EW" },
            { EPointIdent.CM_EF, "EMY Field Artilery" },
            { EPointIdent.CM_WE, "EMY Fixed Wing" },
            { EPointIdent.CM_EK, "EMY Attack Helicopter" },
            { EPointIdent.CM_HG, "EMY General Helicopter" },
            { EPointIdent.CM_EH, "EMY Hospital" },
            { EPointIdent.CM_EI, "EMY Infantry" },
            { EPointIdent.CM_EM, "EMY Mechanized" },
            { EPointIdent.CM_EX, "EMY Medical" },
            { EPointIdent.CM_ET, "EMY TOC" },
            { EPointIdent.CM_EU, "EMY Generic Unit" }
        };

        /// <summary>
        /// The descriptions for TG idents
        /// </summary>
        public static readonly Dictionary<EPointIdent, string> ETGOptionDescriptions = new Dictionary<EPointIdent, string>()
        {
            { EPointIdent.TG_AX, "AMX-13" },
            { EPointIdent.TG_AS, "Aspide" },
            { EPointIdent.TG_AD, "ADU Friendly" },
            { EPointIdent.TG_GP, "Gepard" },
            { EPointIdent.TG_G1, "Growth 1" },
            { EPointIdent.TG_G2, "Growth 2" },
            { EPointIdent.TG_G3, "Growth 3" },
            { EPointIdent.TG_G4, "Growth 4" },
            { EPointIdent.TG_SD, "SPADA" },
            { EPointIdent.TG_83, "M1983" },
            { EPointIdent.TG_U, "Unknown" },
            { EPointIdent.TG_S6, "2S6/SA-19 Tunguska" },
            { EPointIdent.TG_AA, "Gun AAA" },
            { EPointIdent.TG_GU, "Gun Generic" },
            { EPointIdent.TG_MK, "Gun Marksman" },
            { EPointIdent.TG_SB, "Gun Sabre" },
            { EPointIdent.TG_GS, "Gun Self-Propelled" },
            { EPointIdent.TG_GT, "Gun Towed" },
            { EPointIdent.TG_ZU, "Gun ZSU-23" },
            { EPointIdent.TG_NV, "Naval" },
            { EPointIdent.TG_SR, "Surveillance Radar" },
            { EPointIdent.TG_TR, "Acquisition Radar" },
            { EPointIdent.TG_70, "RBS-70" },
            { EPointIdent.TG_BP, "SAM Blowpipe" },
            { EPointIdent.TG_BH, "SAM Bloodhound" },
            { EPointIdent.TG_CH, "SAM Chaparral" },
            { EPointIdent.TG_CT, "SAM Crotale" },
            { EPointIdent.TG_C2, "SAM CSA-2/1/X" },
            { EPointIdent.TG_HK, "SAM Hawk" },
            { EPointIdent.TG_JA, "SAM Javelin" },
            { EPointIdent.TG_PT, "SAM Patriot" },
            { EPointIdent.TG_RE, "SAM Redeye" },
            { EPointIdent.TG_RA, "SAM Rapier" },
            { EPointIdent.TG_RO, "SAM Roland" },
            { EPointIdent.TG_1, "SAM SA-1" },
            { EPointIdent.TG_2, "SAM SA-2" },
            { EPointIdent.TG_3, "SAM SA-3" },
            { EPointIdent.TG_4, "SAM SA-4" },
            { EPointIdent.TG_5, "SAM SA-5" },
            { EPointIdent.TG_6, "SAM SA-6" },
            { EPointIdent.TG_7, "SAM SA-7" },
            { EPointIdent.TG_8, "SAM SA-8" },
            { EPointIdent.TG_9, "SAM SA-9" },
            { EPointIdent.TG_10, "SAM SA-10" },
            { EPointIdent.TG_11, "SAM SA-11" },
            { EPointIdent.TG_12, "SAM SA-12" },
            { EPointIdent.TG_13, "SAM SA-13" },
            { EPointIdent.TG_14, "SAM SA-14" },
            { EPointIdent.TG_15, "SAM SA-15" },
            { EPointIdent.TG_16, "SAM SA-16" },
            { EPointIdent.TG_17, "SAM SA-17" },
            { EPointIdent.TG_SM, "SAM SAMP" },
            { EPointIdent.TG_SC, "SAM SATCP" },
            { EPointIdent.TG_SP, "SAM Self-Propelled" },
            { EPointIdent.TG_SH, "SAM Shahin/R440" },
            { EPointIdent.TG_SS, "SAM Starstreak" },
            { EPointIdent.TG_TC, "SAM Tigercat" },
            { EPointIdent.TG_ST, "SAM Stinger" },
            { EPointIdent.TG_SA, "SAM Towed" },
            { EPointIdent.TG_VU, "Gun Vulcan" },
            { EPointIdent.TG_TG, "*Target Reference Point" }
        };
    }
}
