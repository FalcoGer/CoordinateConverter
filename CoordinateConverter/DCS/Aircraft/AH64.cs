using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft
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
        private enum EDeviceCode
        {
            PLT_KU = 29,
            CPG_KU = 30,
            PLT_RMFD = 43,
            CPG_RMFD = 45
        }

        /// <summary>
        /// Key codes
        /// </summary>
        private enum EKeyCode : int
        {
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
            RMFD_TSD = 3029,
            RMFD_B6 = 3013, // POINT page
            RMFD_L1 = 3024, // IDENT
            RMFD_L2 = 3023, // point ADD
            RMFD_L3 = 3022, // WP (TODO: TEST)
            RMFD_L4 = 3021, // HZ (TODO: TEST)
            RMFD_L5 = 3020, // CM (TODO: TEST)
            RMFD_L6 = 3019, // TG (TODO: TEST)

        }


        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="coordinate">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.ArgumentException">Bad Point Type</exception>
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            AH64SpecificData extraData = null;
            EKeyCode keyMFDPointType = EKeyCode.RMFD_L3; // assume waypoint
            string ident = "\n"; // assume default ident
            if (coordinate.AircraftSpecificData.ContainsKey(typeof(AH64)) && coordinate.AircraftSpecificData[typeof(AH64)] != null)
            {
                extraData = coordinate.AircraftSpecificData[typeof(AH64)] as AH64SpecificData;
                ident = extraData.Ident.ToString().Substring(3) + '\n';

                switch (extraData.PointType)
                {
                    case EPointType.Waypoint:
                        keyMFDPointType = EKeyCode.RMFD_L3;
                        break;
                    case EPointType.Hazard:
                        keyMFDPointType = EKeyCode.RMFD_L4;
                        break;
                    case EPointType.ControlMeasure:
                        keyMFDPointType = EKeyCode.RMFD_L5;
                        break;
                    case EPointType.Target:
                        keyMFDPointType = EKeyCode.RMFD_L6;
                        break;
                    default:
                        throw new ArgumentException("Bad Point Type");
                }
            }

            List<DCSCommand> commands = new List<DCSCommand>
            {
                // press ADD
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.RMFD_L2),
                // press the waypoint type button
                extraData == null ? null : new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)keyMFDPointType),
                // press ident
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.RMFD_L1)
            };
            // enter ident
            commands.AddRange(GetCommandsForKUText(ident, false));
            // enter free text (max 3 chars)
            commands.AddRange(GetCommandsForKUText((coordinate.Name.Length <= 3 ? coordinate.Name : coordinate.Name.Substring(0, 3)) + '\n', false));
            // enter MGRS coordinates
            // remove spaces and append enter
            string mgrsString = string.Join(string.Empty ,coordinate.GetCoordinateStrMGRS(4).Where(ch => ch != ' ')) + '\n';
            commands.AddRange(GetCommandsForKUText(mgrsString, true));

            // enter altitude
            if (coordinate.AltitudeInFt == 0 && coordinate.AltitudeIsAGL)
            {
                commands.AddRange(GetCommandsForKUText("\n", false));
            }
            else
            {
                commands.AddRange(GetCommandsForKUText(((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString() + "\n", true));
            }
            return commands;
        }

        /// <summary>
        /// Gets the commands for ku text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="clearFirst">if set to <c>true</c> the KU input is cleared first by use of the CLR button.</param>
        /// <returns>The list of commands that is required to enter the text into the KU</returns>
        private List<DCSCommand> GetCommandsForKUText(string text, bool clearFirst)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            if (text == null)
            {
                return commands;
            }

            if (clearFirst)
            {
                commands.Add(new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_KU : EDeviceCode.CPG_KU), (int)EKeyCode.KU_CLR));
            }
            // type the text
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
                    commands.Add(new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_KU : EDeviceCode.CPG_KU), (int)keyCode.Value));
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
        public override List<DCSCommand> GetPostPointActions()
        {
            return new List<DCSCommand>()
            {
                // press TSD to reset the screen
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.RMFD_TSD)
            };
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        public override List<DCSCommand> GetPrePointActions()
        {
            return new List<DCSCommand>
            {
                // press TSD
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.RMFD_TSD),
                // go to point page
                new DCSCommand((int)(IsPilot ? EDeviceCode.PLT_RMFD : EDeviceCode.CPG_RMFD), (int)EKeyCode.RMFD_B6),
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

            for (int pointIdx = startIdx - ((pointType == EPointType.ControlMeasure) ? 50 : 0); pointIdx <= endIdx - ((pointType == EPointType.ControlMeasure) ? 50 : 0); pointIdx++)
            {
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_TSD)); // Reset to TSD after every point, to avoid weirdness.
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_B6)); // Point
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_L1)); // Point >
                commands.AddRange(GetCommandsForKUText(pointType.ToString().First() + pointIdx.ToString() + "\n", true)); // Enter point identifier
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_L4)); // Del
                commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_L3)); // Yes
            }
            commands.Add(new DCSCommand(deviceId, (int)EKeyCode.RMFD_TSD)); // reset to TSD
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
            { EPointIdent.WP_WP, "Waypoint" },
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
            { EPointIdent.HZ_TU, "Tower < 1000" },
            { EPointIdent.HZ_WL, "Wires Power" },
            { EPointIdent.HZ_WS, "Wires Tele/Elec" }
        };

        /// <summary>
        /// The descriptions for CM idents
        /// </summary>
        public static readonly Dictionary<EPointIdent, string> ECMOptionDescriptions = new Dictionary<EPointIdent, string>()
        {
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
            { EPointIdent.CM_WF, "FND Fixed Wing" },
            { EPointIdent.CM_FL, "FND Fixed Arty" },
            { EPointIdent.CM_AH, "FND Attack Helicopter" },
            { EPointIdent.CM_FG, "FND General Helicopter" },
            { EPointIdent.CM_HO, "FND Hospital" },
            { EPointIdent.CM_FI, "FND Infantry" },
            { EPointIdent.CM_MI, "FND Mechanized" },
            { EPointIdent.CM_MD, "FND Medical" },
            { EPointIdent.CM_TF, "FND TOC" },
            { EPointIdent.CM_FU, "FND Generic Unit" },
            { EPointIdent.CM_ES, "EMY Air Assault" },
            { EPointIdent.CM_EV, "EMY Airborne Cav" },
            { EPointIdent.CM_ED, "EMY ADU" },
            { EPointIdent.CM_EB, "EMY Airborne" },
            { EPointIdent.CM_EC, "EMY CAV Armor" },
            { EPointIdent.CM_AE, "EMY Armor" },
            { EPointIdent.CM_ME, "EMY AV Maintenance" },
            { EPointIdent.CM_CE, "EMY Chemical" },
            { EPointIdent.CM_DE, "EMY Decontamination" },
            { EPointIdent.CM_EE, "EMY Engineers" },
            { EPointIdent.CM_WR, "EMY EW" },
            { EPointIdent.CM_EF, "EMY Fixed Wing" },
            { EPointIdent.CM_WE, "EMY Fixed Arty" },
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
            { EPointIdent.TG_TG, "Target Reference Point" }
        };
    }
}
