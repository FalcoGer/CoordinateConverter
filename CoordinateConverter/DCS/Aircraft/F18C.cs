using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents F18C aircraft
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSAircraft" />
    public class F18C : DCSAircraft
    {
        private enum EDevices
        {
            UFC = 25,
            MDI_LEFT = 35,
            AMPCD = 37,

        }

        // from command_defs.lua, numbers are calculated from the counter() function.
        private enum EKeyCodes
        {
            MDI_PB01 = 3011,
            MDI_PB02,
            MDI_PB03,
            MDI_PB04,
            MDI_PB05,
            MDI_PB06, // 3016
            MDI_PB07,
            MDI_PB08,
            MDI_PB09,
            MDI_PB10,
            MDI_PB11, // 3021
            MDI_PB12,
            MDI_PB13,
            MDI_PB14,
            MDI_PB15,
            MDI_PB16, // 3026
            MDI_PB17,
            MDI_PB18,
            MDI_PB19,
            MDI_PB20, // 3030

            UFC_AP = 3001,
            UFC_IFF,
            UFC_TCN,
            UFC_ILS,
            UFC_DL,
            UFC_BCN,
            UFC_ON_OFF,
            UFC_COMM1_FN,
            UFC_COMM2_FN,
            UFC_PB1,
            UFC_PB2,
            UFC_PB3,
            UFC_PB4,
            UFC_PB5,
            UFC_IP,
            UFC_ADF,
            UFC_EMCON,
            UFC_KB0_NEG,
            UFC_KB1,
            UFC_KB2_N,
            UFC_KB3,
            UFC_KB4_W,
            UFC_KB5,
            UFC_KB6_E,
            UFC_KB7,
            UFC_KB8_S,
            UFC_KB9,
            UFC_KB_CLR,
            UFC_KB_ENT,
        }

        private List<WeaponStation> weapons = new List<WeaponStation>();


        /// <summary>
        /// Weapon types that get grouped together in the hornet
        /// </summary>
        public enum EWeaponType
        {
            /// <summary>
            /// Mk82 500lbs based JDAMs (GBU-38 and BRU-55 With GBU-38)
            /// </summary>
            J82,

            /// <summary>
            /// MK83 1000lbs based JDAMs (GBU-32(V)2/B)
            /// </summary>
            J83,

            /// <summary>
            /// Mk84 2000lbs based JDAMs without penetrator (GBU-31(V)1/B and GBU-31(V)2/B)
            /// </summary>
            J84,

            /// <summary>
            /// MK84 2000lbs based JDAMs with BLU-109 penetrator (GBU-31(V)3/B and GBU-31(V)4/B)
            /// </summary>
            J109,

            /// <summary>
            /// AGM-154A JSOW (cluster effect munitions)
            /// </summary>
            JSA,

            /// <summary>
            /// AGM-154C JSOW (unary broach)
            /// </summary>
            JSC,

            /// <summary>
            /// AGM-84E Stand-off land attack missile
            /// </summary>
            SLAM,

            /// <summary>
            /// AGM-84H Stand-off land attack missile, expanded response
            /// </summary>
            SLAMER,

            /// <summary>
            /// The last AG programable weapon is below this value
            /// </summary>
            _LAST_AG_PROGRAMABLE,

            /// <summary>
            /// The AAW-13 Datalink pod
            /// </summary>
            DL13,

            /// <summary>
            /// The Mk82 low drag bomb
            /// </summary>
            B82B,

            /// <summary>
            /// The Mk82 Snake eye retarded bomb
            /// </summary>
            B82XT,

            /// <summary>
            /// The Mk82Y high drag bomb
            /// </summary>
            B82YT,

            /// <summary>
            /// The GBU-10 laser guided bombs
            /// </summary>
            B82LG,

            /// <summary>
            /// The Mk83 1000lbs bomb
            /// </summary>
            B83B,

            /// <summary>
            /// The GBU-16 1000 lbs bomb
            /// </summary>
            B83LG,

            /// <summary>
            /// The Mk84 2000 lbs bomb
            /// </summary>
            B84B,

            /// <summary>
            /// The Mk84 2000 lbs laser guided bomb (GBU-10)
            /// </summary>
            B84LG,

            /// <summary>
            /// The GBU-24 laser guided, glide penetrator bomb
            /// </summary>
            GB24,

            /// <summary>
            /// The BDU-33 practice bomb
            /// </summary>
            B76,

            /// <summary>
            /// The BDU-45 Practice bomb family
            /// </summary>
            B45X,

            /// <summary>
            /// The BDU-45 Practice bomb family
            /// </summary>
            B45,

            /// <summary>
            /// The B45 Laser guided practice bomb
            /// </summary>
            B45LG,

            /// <summary>
            /// The ADM_141A TALD
            /// </summary>
            T82P,

            /// <summary>
            /// The AGM-88C HARM
            /// </summary>
            HARM,

            /// <summary>
            /// The AGM-84D Harpoon
            /// </summary>
            HPD,

            /// <summary>
            /// The AGM-65E Laser maverick
            /// </summary>
            MAV,

            /// <summary>
            /// The AGM-65F IR Imaging maverick
            /// </summary>
            MAVF,

            /// <summary>
            /// The Walleye datalink guided bomb
            /// </summary>
            WEDL,

            /// <summary>
            /// The Mk-20 Rockeye
            /// </summary>
            RE,

            /// <summary>
            /// CBU99 Family cluster bombs
            /// </summary>
            RET,

            /// <summary>
            /// LAU-10 w/ Zuni
            /// </summary>
            R10S,

            /// <summary>
            /// LAU-61 w/ Hydra (x19)
            /// </summary>
            R61S,

            /// <summary>
            /// The LAU-68 w/ Hydra (x7)
            /// </summary>
            R68S,

            /// <summary>
            /// The last weapon that is listed on the A/G stores page is less than this value
            /// </summary>
            _LAST_AG_LISTED,

            /// <summary>
            /// The AIM7 familiy missiles
            /// </summary>
            AIM7,

            /// <summary>
            /// The AIM9 family missiles
            /// </summary>
            AIM9,

            /// <summary>
            /// The AIM120 family missiles
            /// </summary>
            AIM120,

            /// <summary>
            /// The last listed AA weapon
            /// </summary>
            _LAST_AA_LISTED,

            /// <summary>
            /// Fuel tanks
            /// </summary>
            FUEL,

            /// <summary>
            /// Targeting pods (AN-ASQ)
            /// </summary>
            TPOD,

            /// <summary>
            /// The AN/ASQ-T50 TCTS ACMI Pod
            /// </summary>
            AIR_SENSOR,

            /// <summary>
            /// Smoke canister for airshows
            /// </summary>
            SMOKE,

            /// <summary>
            /// Just a pylon or pylon removed
            /// </summary>
            Empty,

            /// <summary>
            /// Unknown weapon
            /// </summary>
            Unknown
        }

        /// <summary>
        /// List of types that are considered JDAMs and thus have their display option select button on PB11
        /// </summary>
        public List<EWeaponType> JdamTypes { get; private set; } = new List<EWeaponType>()
        {
            EWeaponType.J82,
            EWeaponType.J83,
            EWeaponType.J84,
            EWeaponType.J109
        };

        /// <summary>
        /// Gets the F18 programable weapon type for the station number.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public Dictionary<int, EWeaponType> StationGroups { get; private set; } = new Dictionary<int, EWeaponType>();

        /// <summary>
        /// Gets the order of programbe types for the F18 as it appears on the MFD from PB 6 through 10.
        /// </summary>
        /// <value>
        /// The weapon station order.
        /// </value>
        public List<EWeaponType> WeaponStationOrder { get; private set; } = new List<EWeaponType>();

        /// <summary>
        /// Dictionary to turn weapon names into <see cref="EWeaponType"/>, which are grouped in the jet itself.
        /// </summary>
        public readonly Dictionary<string, EWeaponType> weaponNamesForGroups = new Dictionary<string, EWeaponType>()
        {
            // JDAM
            { "{GBU-38}", EWeaponType.J82 },
            { "{BRU55_2*GBU-38}", EWeaponType.J82 },
            { "{GBU_32_V_2B}", EWeaponType.J83 },
            { "{GBU-31}", EWeaponType.J84 },
            { "{GBU_31_V_2B}", EWeaponType.J84 },
            { "{GBU-31V3B}", EWeaponType.J109 },
            { "{GBU_31_V_4B}", EWeaponType.J109 },

            // JSOW
            { "{AGM-154A}", EWeaponType.JSA },
            { "{BRU55_2*AGM-154A}", EWeaponType.JSA },
            { "{9BCC2A2B-5708-4860-B1F1-053A18442067}", EWeaponType.JSC }, // AGM-154C single
            { "{BRU55_2*AGM-154C}", EWeaponType.JSC },

            // LASER
            { "{DB769D48-67D7-42ED-A2BE-108D566C8B1E}", EWeaponType.B82LG }, // GBU-12 single
            { "{BRU33_2X_GBU-12}", EWeaponType.B82LG },
            { "{0D33DDAE-524F-4A4E-B5B8-621754FE3ADE}", EWeaponType.B83LG }, // GBU16 (1000lbs)
            { "{51F9AAE5-964F-4D21-83FB-502E3BFE5F8A}", EWeaponType.B84LG }, // GBU-10 (2000lbs)
            { "{GBU-24}", EWeaponType.GB24 },

            // CLUSTER
            { "{BRU33_2X_CBU-99}", EWeaponType.RET },
            { "{BRU33_2X_ROCKEYE}", EWeaponType.RE },
            { "{ADD3FAE1-EBF6-4EF9-8EFC-B36B5DDF1E6B}", EWeaponType.RE }, // Rockeye single
            { "{CBU_99}", EWeaponType.RET },

            // Special
            { "{C40A1E3A-DD05-40D9-85A4-217729E37FAE}", EWeaponType.WEDL }, // WallEye DataLink

            // IRON
            { "{BRU33_2X_MK-82}", EWeaponType.B82B }, // Mk82 Slick
            { "{BCE4E030-38E9-423E-98ED-24BE3DA87C32}", EWeaponType.B82B }, // 82 slick
            { "{BRU33_2X_MK-82_Snakeye}", EWeaponType.B82XT },
            { "{Mk82SNAKEYE}", EWeaponType.B82XT },
            { "{BRU33_2X_MK-82Y}", EWeaponType.B82YT },
            { "{Mk_82Y}", EWeaponType.B82YT },
            { "{BRU33_2X_MK-83}", EWeaponType.B83B },
            { "{7A44FF09-527C-4B7E-B42B-3F111CFE50FB}", EWeaponType.B83B }, // 83 single
            { "{AB8B8299-F1CC-4359-89B5-2172E0CF4A5A}", EWeaponType.B84B }, // 84

            // ROCKETS
            { "{BRU33_LAU10}", EWeaponType.R10S },
            { "{BRU33_2*LAU10}", EWeaponType.R10S },
            { "{BRU33_LAU61}", EWeaponType.R61S },
            { "{BRU33_2*LAU61}", EWeaponType.R61S },
            { "{BRU33_LAU68_MK5}", EWeaponType.R68S },
            { "{BRU33_LAU68}", EWeaponType.R68S },
            { "{BRU33_2*LAU68}", EWeaponType.R68S },
            { "{BRU33_2*LAU68_MK5}", EWeaponType.R68S },

            // PRACTICE
            { "{BDU_45}", EWeaponType.B45X }, // dumb practice
            { "{BRU33_2X_BDU-45}", EWeaponType.B45X },
            { "{BDU_45B}", EWeaponType.B45 }, // practice ballute
            { "{BRU33_2X_BDU-45B}", EWeaponType.B45 },
            { "{BDU_45LG}", EWeaponType.B45LG }, // Laser Guided
            { "{BRU33_2X_BDU_45LG}", EWeaponType.B45LG },
            { "{BRU41_6X_BDU-33}", EWeaponType.B76 }, // 6x BDU-33

            // MISSILES
            { "{B06DD79A-F21E-4EB9-BD9D-AB3844618C93}", EWeaponType.HARM },
            { "{AGM_84D}", EWeaponType.HPD },
            { "{AF42E6DF-9A60-46D8-A9A0-1708B241AADB}", EWeaponType.SLAM }, // AGM-84E SLAM
            { "{AGM_84H}", EWeaponType.SLAMER },
            { "{F16A4DE0-116C-4A71-97F0-2CF85B0313EC}", EWeaponType.MAV }, // Laser MAV
            { "LAU_117_AGM_65F", EWeaponType.MAVF },


            // TALD
            { "{BRU_42A_x1_ADM_141A}", EWeaponType.T82P },
            { "{BRU_42A_x2_ADM_141A}", EWeaponType.T82P },
            { "{BRU_42A_x3_ADM_141A}", EWeaponType.T82P },

            // AIM7
            { "{AIM-7H}", EWeaponType.AIM7 },
            { "{LAU-115 - AIM-7F}", EWeaponType.AIM7 },
            { "{AIM-7P}", EWeaponType.AIM7 },
            { "{LAU-115 - AIM-7M}", EWeaponType.AIM7 },
            { "{AIM-7F}", EWeaponType.AIM7 },
            { "{LAU-115 - AIM-7P}", EWeaponType.AIM7 },
            { "{LAU-115 - AIM-7H}", EWeaponType.AIM7 },
            { "{8D399DDA-FF81-4F14-904D-099B34FE7918}", EWeaponType.AIM7 },

            // AIM9
            { "CATM-9M", EWeaponType.AIM9 },
            { "{5CE2FF2A-645A-4197-B48D-8720AC69394F}", EWeaponType.AIM9 }, // AIM9X
            { "LAU-115_2*LAU-127_AIM-9L", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9L_R", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_CATM-9M_R", EWeaponType.AIM9 },
            { "LAU-115_2*LAU-127_CATM-9M", EWeaponType.AIM9 },
            { "{6CEB49FC-DED8-4DED-B053-E1F033FF72D3}", EWeaponType.AIM9 }, // AIM9M
            { "LAU-115_2*LAU-127_AIM-9M", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9M_R", EWeaponType.AIM9 },
            { "{AIM-9L}", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9L", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9M", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9X", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_AIM-9X_R", EWeaponType.AIM9 },
            { "LAU-115_2*LAU-127_AIM-9X", EWeaponType.AIM9 },
            { "LAU-115_LAU-127_CATM-9M", EWeaponType.AIM9 },

            // AIM120

            { "{LAU-115 - AIM-120B}", EWeaponType.AIM120 },
            { "LAU-115_2*LAU-127_AIM-120C", EWeaponType.AIM120 },
            { "{C8E06185-7CD6-4C90-959F-044679E90751}", EWeaponType.AIM120 }, // AIM120B
            { "{40EF17B7-F508-45de-8566-6FFECC0C1AB8}", EWeaponType.AIM120 }, // AIM120C-5
            { "{LAU-115 - AIM-120B_R}", EWeaponType.AIM120 },
            { "{LAU-115 - AIM-120C}", EWeaponType.AIM120 },
            { "{LAU-115 - AIM-120C_R}", EWeaponType.AIM120 },

            // DATA
            { "{AWW-13}", EWeaponType.DL13 },
            { "{AN_ASQ_228}", EWeaponType.TPOD }, // ATFlir
            { "{A111396E-D3E8-4b9c-8AC9-2432489304D5}", EWeaponType.TPOD }, // AAQ-28 Litening (Center pylon)
            { "{AAQ-28_LEFT}", EWeaponType.TPOD },
            { "{AIS_ASQ_T50}", EWeaponType.AIR_SENSOR },

            // SMOKE
            { "{INV-SMOKE-BLUE}" , EWeaponType.SMOKE },
            { "{INV-SMOKE-GREEN}" , EWeaponType.SMOKE },
            { "{INV-SMOKE-ORANGE}" , EWeaponType.SMOKE },
            { "{INV-SMOKE-RED}" , EWeaponType.SMOKE },
            { "{INV-SMOKE-WHITE}" , EWeaponType.SMOKE },
            { "{INV-SMOKE-YELLOW}" , EWeaponType.SMOKE },

            // FUEL
            { "{FPU_8A_FUEL_TANK}", EWeaponType.FUEL },
            
            // EMPTY
            { "", EWeaponType.Empty }, // Just a pylon
            { "<CLEAN>", EWeaponType.Empty }, // Nothing at all
        };


        /// <summary>
        /// Updates the weapon stations.
        /// </summary>
        /// <param name="newWeapons">The new weapons.</param>
        /// <returns>If the stations were updated, false if they were the same as the new stations</returns>
        public bool UpdateWeaponStations(List<WeaponStation> newWeapons)
        {
            if (weapons == null && newWeapons == null)
            {
                return false;
            }
           
            if (weapons != null && newWeapons != null)
            {
                // they're both not null
                if (weapons.Count == newWeapons.Count)
                {
                    // same amount, need to check each station for equality
                    bool isSame = true;
                    for (int idx = 0; idx < weapons.Count; idx++)
                    {
                        WeaponStation oldWeapon = weapons[idx];
                        WeaponStation newWeapon = newWeapons[idx];
                        if (!oldWeapon.Equals(newWeapon))
                        {
                            isSame = false;
                            break;
                        }
                    }
                    if (isSame)
                    {
                        return false;
                    }
                }
            }
            weapons = newWeapons;
            // update station groups and the order in the jet
            StationGroups = new Dictionary<int, EWeaponType>();
            WeaponStationOrder = new List<EWeaponType>();
            foreach (WeaponStation weapon in weapons.Where(x => x != null).OrderBy(x => x.StationNumber))
            {
                if (weaponNamesForGroups.ContainsKey(weapon.WeaponName))
                {
                    EWeaponType weaponType = weaponNamesForGroups[weapon.WeaponName];
                    StationGroups.Add(weapon.StationNumber, weaponType);
                    if (!WeaponStationOrder.Contains(weaponType) && weaponType < EWeaponType._LAST_AG_LISTED)
                    {
                        WeaponStationOrder.Add(weaponType);
                    }
                }
                else
                {
                    string weaponInfo = Newtonsoft.Json.JsonConvert.SerializeObject(weapon, Newtonsoft.Json.Formatting.Indented);
                    string errMsg = string.Format(
                        "Unknown weapon: \"{0}\" on station {1}.\n" +
                        "====================\n" +
                        "{2}\n" +
                        "====================\n\n" +
                        "Weapon data entry may be unreliable.\n" +
                        "This information was copied to clipboard.\n" +
                        "Please provide it on https://github.com/FalcoGer/CoordinateConverter/issues\n" +
                        "Along with the actual weapon on the pylon.",
                        weapon.WeaponName,
                        weapon.StationNumber,
                        weaponInfo
                    );
                    Clipboard.SetText("```json\n" + weaponInfo + "\n```");
                    MessageBox.Show(errMsg, "Unknown weapon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    weaponNamesForGroups.Add(weapon.WeaponName, EWeaponType.Unknown);
                }
            }
            return true;
        }

        /// <summary>
        /// The next slamer STP number to be entered. needed to press the correct option select button on the UFC
        /// </summary>
        private int currentSlamErStp = 0;
        /// <summary>
        /// The left ddi was set up, needs to be done only once during entry and only when weapon points are part of the plan
        /// </summary>
        private bool leftDDIWasSetUp = false;
        /// <summary>
        /// The number of waypoints entered into the jet. Used for backtracking the waypoint number to where it was before entering.
        /// </summary>
        private int numberOfWaypointsEntered = 0;

        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="coordinate">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            if (!coordinate.AircraftSpecificData.ContainsKey(typeof(F18C)))
            {
                coordinate.AircraftSpecificData.Add(typeof(F18C), new F18CSpecificData());
            }
            F18CSpecificData extraData = coordinate.AircraftSpecificData[typeof(F18C)] as F18CSpecificData;

            List<DCSCommand> commands = new List<DCSCommand>();
            // var commands = new DebugCommandList();

            if (!extraData.WeaponType.HasValue)
            {
                // Is standard waypoint
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB12, 300)); // Increment WP#
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB05, 800)); // Open UFC
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 300)); // POSN
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 300, 0));

                // Enter latitude
                double latMinutes = coordinate.Coordinate.Latitude.DecimalMinute;
                string latMDecimal = string.Format("{0}\n", (int)Math.Round((latMinutes - (int)latMinutes) * 10000));
                string latDM = string.Format("{0}{1}{2}\n",
                    coordinate.Coordinate.Latitude.Position.ToString(),
                    coordinate.Coordinate.Latitude.Degrees.ToString().PadLeft(2, '0'),
                    coordinate.Coordinate.Latitude.Minutes.ToString().PadLeft(2, '0')
                    );
                commands.AddRange(UFCEnterString(latDM));
                commands.AddRange(UFCEnterString(latMDecimal));

                // Enter longitude
                double lonMinutes = coordinate.Coordinate.Longitude.DecimalMinute;
                string lonMDecimal = string.Format("{0}\n", (int)Math.Round((lonMinutes - (int)lonMinutes) * 10000));
                string lonDMS = string.Format("{0}{1}{2}\n",
                    coordinate.Coordinate.Longitude.Position.ToString(),
                    coordinate.Coordinate.Longitude.Degrees.ToString().PadLeft(2, '0'),
                    coordinate.Coordinate.Longitude.Minutes.ToString().PadLeft(2, '0')
                    );
                commands.AddRange(UFCEnterString(lonDMS));
                commands.AddRange(UFCEnterString(lonMDecimal));

                // Enter altitude
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 300)); // ELEV
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 300)); // FEET
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 100, 0)); // FEET
                commands.AddRange(UFCEnterString(((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString() + "\n"));
                numberOfWaypointsEntered++;
                return commands;
            }
            // Non waypoint here

            // The waypoint is for this weapon type
            EWeaponType pwt = extraData.WeaponType.Value;
            int stationBtnIdx = WeaponStationOrder.IndexOf(pwt);
            if (stationBtnIdx == -1)
            {
                // This weapon is not on board.
                return commands;
            }

            // set up left ddi if it wasn't already
            if (!leftDDIWasSetUp)
            {
                // Setup left DDI for weapon entry.
                // Make sure the stores page is up
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 200)); // go to TAC or SUPT page
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 100, 0));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB20, 200)); // go to TGT Data or Fuel page
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB20, 100, 0));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 200)); // go to TAC page
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 100, 0));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB05, 200)); // go to STORES page
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB05, 100, 0));

                if (WeaponStationOrder.Count > 1)
                {
                    // Does not reliably work. Switching JSOW -> LGB -> LGB off selects JSOW again.
                    // Works only reliably with 2 GPS weapons
                    /*
                    // we can reliable deselect any weapon by pression PB 7, then PB 6 twice
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB07, 200)); // Select or deselect the second available weapon type
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB07, 100, 0));
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB06, 200)); // Select the first available weapon type
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB06, 100, 0));
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB06, 200)); // Deselect the first available weapon type
                    commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)KeyCodes.MDI_PB06, 100, 0));
                    */
                    // now no weapon is selected
                }
                leftDDIWasSetUp = true;
            }

            List<int> pylonsWithThatWeapon = StationGroups.Where(x => x.Value == pwt).Select(x => x.Key).ToList();
            if (weapons.Sum(x => x.Count) == 0)
            {
                // no weapons left on the aircraft to step to
                return commands;
            }

            if (extraData.StationSetting == F18CSpecificData.EStationSetting.All)
            {
                foreach (int pylonId in pylonsWithThatWeapon.Where(x => weapons[x].Count > 0))
                {

                    if (extraData.PreplanPointIdx.HasValue)
                    {
                        commands.AddRange(EnterPP(pwt, coordinate, extraData, stationBtnIdx, true));
                    }
                    else
                    {
                        // is a SLAMER STP
                        throw new NotImplementedException("The waypoint type is not yet supported for transfer");
                    }
                }
            }
            else
            {
                bool step = (extraData.StationSetting == F18CSpecificData.EStationSetting.Step);
                if (extraData.PreplanPointIdx.HasValue)
                {
                    commands.AddRange(EnterPP(pwt, coordinate, extraData, stationBtnIdx, step));
                }
                else
                {
                    throw new NotImplementedException("The waypoint type is not yet supported for transfer");
                }
            }
            return commands;
        }

        private List<DCSCommand> EnterPP(EWeaponType pwt, CoordinateDataEntry coordinate, F18CSpecificData extraData, int stationBtnIdx, bool step)
        {
            bool isBomb = JdamTypes.Contains(pwt);
            bool isAGM84Variant = pwt == EWeaponType.SLAM || pwt == EWeaponType.SLAMER;
            bool isPenetrator = pwt == EWeaponType.J109;
            int keyCodeStationSelect = (int)EKeyCodes.MDI_PB06 + stationBtnIdx;
            // JDAMs are PB11, everything else (JSOW, SLAM, SLAMER) on PB12 for DSPLY page
            int keyCodeWpnDsplyPage = (int)EKeyCodes.MDI_PB11 + (isBomb ? 0 : 1);

            List<DCSCommand> commands = new List<DCSCommand>();
            // var commands = new DebugCommandList();
            int keyCodePPIdx = (int)EKeyCodes.MDI_PB06 + extraData.PreplanPointIdx.Value - 1;

            // activate the relevant weapon station
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodeStationSelect, 500));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodeStationSelect, 200, 0)); // sleep a while
            // set EFUZ
            if (!isAGM84Variant) // slammers are toggled, can't determine the setting
            {
                int offset = isPenetrator ? -1 : 0; // penetrators set delay 1, others set instant
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB03, 300));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB03, 100, 0));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB03 + offset, 300));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB03 + offset, 100, 0));
            }

            // Enter DSPLY page
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodeWpnDsplyPage, 400));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodeWpnDsplyPage, 200, 0));

            if (isAGM84Variant)
            {
                // is SLAM/SLAM-ER
                // Set Distance
                // UFC
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 100, 0));
                // DIST
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 200));
                commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 100, 0));
                // Enter 15 nmi
                commands.AddRange(UFCEnterString("15\n"));
            }

            // Enter MSN page
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB04, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB04, 100, 0));

            // Pressing PP when it is selected switches to TOO. We force it into TOO mode by hitting the button twice.
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodePPIdx, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodePPIdx, 200));
            // then we switch back to PP mode, the last selected PP mission will be marked
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB05, 200));

            // HDG UNDF
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB02, 200));
            // TGT UFC
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200, 0));
            // TERM
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1));
            // ANG
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 200));
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 200, 0));
            // 60° for bombs, otherwise 30°
            commands.AddRange(UFCEnterString(isBomb ? "60\n" : "30\n"));
            // VEL
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB5, 200));
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB5, 200, 0));
            commands.AddRange(UFCEnterString("700\n"));
            // TGT UFC (x2)
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200, 0));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200, 0));
            // POSN
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 200));
            // LAT
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 200));
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 500, 0));
            CoordinateSharp.CoordinatePart lat = coordinate.Coordinate.Latitude;
            string latDMS = string.Format("{0}{1}{2}{3}",
                lat.Position.ToString(),
                lat.Degrees.ToString().PadLeft(2, '0'),
                lat.Minutes.ToString().PadLeft(2, '0'),
                ((int)lat.Seconds).ToString().PadLeft(2, '0')
            );
            string latSecDecimal = ((int)Math.Round((lat.Seconds - (int)lat.Seconds) * 100)).ToString().PadLeft(2, '0');
            commands.AddRange(UFCEnterString(latDMS + '\n'));
            commands.AddRange(UFCEnterString(latSecDecimal + '\n'));

            // LON
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 200));
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 500, 0));
            CoordinateSharp.CoordinatePart lon = coordinate.Coordinate.Longitude;
            string lonDMS = string.Format("{0}{1}{2}{3}",
                lon.Position.ToString(),
                lon.Degrees.ToString().PadLeft(2, '0'),
                lon.Minutes.ToString().PadLeft(2, '0'),
                ((int)lon.Seconds).ToString().PadLeft(2, '0')
            );
            string lonSecDecimal = ((int)Math.Round((lon.Seconds - (int)lon.Seconds) * 100)).ToString().PadLeft(2, '0');
            commands.AddRange(UFCEnterString(lonDMS + '\n'));
            commands.AddRange(UFCEnterString(lonSecDecimal + '\n'));

            // TGT UFC (x2)
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200, 0));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB14, 200, 0));

            // ELEV
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB4, 200));
            // FEET
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 300));
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 100, 0));
            // Altitude
            string altitudeStr = ((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString();
            commands.AddRange(UFCEnterString((altitudeStr == "0" ? "1" : altitudeStr) + '\n'));

            if (step)
            {
                // Step. Option does not exist if only one weapon of the type
                commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB13));
            }

            // Cleanup...
            // Return command is inconsistent. Go to TAC -> Stores instead
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB18, 100, 0));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB05, 200));
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, (int)EKeyCodes.MDI_PB05, 100, 0));

            // and deselect the weapon entirely
            commands.Add(new DCSCommand((int)EDevices.MDI_LEFT, keyCodeStationSelect));
            // ready for the next weapon

            return commands;
        }

        /// <summary>
        /// Enters string into UFC
        /// </summary>
        /// <param name="str">The string to enter. Must only contain digits, '-' and '\n'</param>
        /// <returns>The commands required to enter the string.</returns>
        private List<DCSCommand> UFCEnterString(string str)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            const int delay = 300;
            const int offDelay = 100;
            if (str == "0\n")
            {
                str = "00\n";
            }
            foreach (char ch in str.ToUpper())
            {
                switch (ch)
                {
                    case '0':
                    case '-':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB0_NEG, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB0_NEG, offDelay, 0));
                        break;
                    case '1':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB1, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB1, offDelay, 0));
                        break;
                    case 'N':
                    case '2':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB2_N, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB2_N, offDelay, 0));
                        break;
                    case '3':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB3, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB3, offDelay, 0));
                        break;
                    case '4':
                    case 'W':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB4_W, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB4_W, offDelay, 0));
                        break;
                    case '5':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB5, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB5, offDelay, 0));
                        break;
                    case '6':
                    case 'E':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB6_E, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB6_E, offDelay, 0));
                        break;
                    case '7':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB7, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB7, offDelay, 0));
                        break;
                    case '8':
                    case 'S':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB8_S, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB8_S, offDelay, 0));
                        break;
                    case '9':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB9, delay));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB9, offDelay, 0));
                        break;
                    case '\n':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_ENT, 500));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_ENT, 500, 0));
                        break;
                    default:
                        throw new ArgumentException(nameof(str) + " must only contain 0 .. 9, -, N, E, S, W, and \\n. But has '" + ch + "'");
                }
            }
            return commands;
        }

        /// <summary>
        /// String that describes a waypoint point type
        /// </summary>
        public const string WAYPOINT_STR = "Waypoint";
        /// <summary>
        /// String that describes a preplanned target point type
        /// </summary>
        public const string PP_TARGET_STR = "PP TARGET";
        /// <summary>
        /// String that describes a slammer steer point type
        /// </summary>
        public const string SLAMER_STP_STR = "SLAM-ER STP";

        /// <summary>
        /// Gets the string for a PP weapon target point as placed into the point type combo box for a specific weapon type
        /// </summary>
        /// <param name="pwt">The PWT.</param>
        /// <returns>The point option string for the weapon type provided</returns>
        public static string GetPointTypePPStrForWeaponType(EWeaponType pwt)
        {
            return Enum.GetName(typeof(EWeaponType), pwt) + " " + PP_TARGET_STR;
        }

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            List<string> options = new List<string>();
            if (pointTypeStr == WAYPOINT_STR)
            {
                options.Add(WAYPOINT_STR);
            }
            else if (pointTypeStr.EndsWith(PP_TARGET_STR))
            {
                for (int i = 1; i <= 6; i++)
                {
                    foreach (string stationSettingsStr in Enum.GetNames(typeof(F18CSpecificData.EStationSetting)))
                    {
                        options.Add(string.Format("PP {0} - {1}", i, stationSettingsStr));
                    }
                }
            }
            else if (pointTypeStr == SLAMER_STP_STR)
            {
                foreach (string stationSettingsStr in Enum.GetNames(typeof(F18CSpecificData.EStationSetting)))
                {
                    options.Add(string.Format("Auto Increment - {0}", stationSettingsStr));
                }
                
            }
            return options;
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>
        /// A list of valid point types.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<string> GetPointTypes()
        {
            List<string> types = new List<string>()
            {
                WAYPOINT_STR
            };
            foreach (EWeaponType weaponCategory in Enum.GetValues(typeof(EWeaponType)))
            {
                if (weaponCategory < EWeaponType._LAST_AG_PROGRAMABLE)
                {
                    types.Add(GetPointTypePPStrForWeaponType(weaponCategory));
                }
            }
            types.Add(SLAMER_STP_STR);
            return types;
        }

        /// <summary>
        /// Gets the actions to be used after points have been entered.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        public override List<DCSCommand> GetPostPointActions()
        {
            List<DCSCommand> commands = new List<DCSCommand>
            {
                new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_CLR)  // clear ufc
            };
            for (int ctr = 0; ctr < numberOfWaypointsEntered; ctr++)
            {
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB13)); // back one waypoint
            }
            commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18)); // go to TAC page
            commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB13)); // go to SA

            // if ddi was set up, need to clean it up again
            
            if (leftDDIWasSetUp)
            {
                // cleanup left ddi
            }
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
            // reset variables that need to be kept track of during entry
            leftDDIWasSetUp = false;
            currentSlamErStp = 1;
            numberOfWaypointsEntered = 0;

            List<DCSCommand> commands = new List<DCSCommand>
            {
                // Setup AMPCD for waypoints
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 150), // go to TAC or SUPT page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB20, 150), // go to TGT Data or Fuel page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 150), // go to TAC page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 150), // go to SUPT page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB02, 150), // go to HSI page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB10, 150), // go to DATA page
            };
            return commands;
        }
    }
}
