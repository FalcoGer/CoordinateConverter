using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft.F18C
{
    /// <summary>
    /// Class containing weapon data for the F18C
    /// </summary>
    public class F18WeaponInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="F18WeaponInfo"/> class.
        /// </summary>
        [JsonConstructor]
        public F18WeaponInfo()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F18WeaponInfo"/> class.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <param name="allStations">All stations.</param>
        public F18WeaponInfo(WeaponStation station, List<WeaponStation> allStations)
        {
            Station = station;
            UpdateKey(allStations);
        }

        /// <summary>
        /// Updates the key to press for this weapon.
        /// </summary>
        /// <param name="allStations">List of all weapon stations.</param>
        public void UpdateKey(List<WeaponStation> allStations)
        {
            List<EWeaponType> knownTypes = new List<EWeaponType>();
            // The keys are assigned on top of the MFD in order of the weapons as they appear on the station.
            foreach (WeaponStation station in
                allStations
                    .OrderBy(x => x.StationNumber)
                    .Where(x => weaponNamesForGroups.ContainsKey(x.WeaponName))
                    .Where(x => weaponNamesForGroups[x.WeaponName] <= EWeaponType._LAST_AG_LISTED)
            )
            {
                EWeaponType wt = weaponNamesForGroups[station.WeaponName];
                if (!knownTypes.Contains(wt))
                {
                    knownTypes.Add(wt);
                }
            }
            int idx = knownTypes.IndexOf(WeaponType);
            if(idx == -1)
            {
                WeaponStationButtonIndex = null;
            }
            else
            {
                WeaponStationButtonIndex = idx;
            }
        }

        /// <summary>
        /// Gets the index of the weapon station button.
        /// </summary>
        /// <value>
        /// The index of the weapon station button.
        /// </value>
        public int? WeaponStationButtonIndex { get; private set; }

        /// <summary>
        /// The stores page button for weapon type.
        /// If null, the weapon is not loaded or can not be selected (eg. fuel tank).
        /// </summary>
        [JsonIgnore]
        public F18C.EKeyCodes? StoresPageButtonForWeaponType
        {
            get
            {
                return WeaponStationButtonIndex.HasValue ? (F18C.EKeyCodes?)((int)F18C.EKeyCodes.MDI_PB06 + WeaponStationButtonIndex.Value) : null;
            }
        }

        [JsonProperty("Station")]
        private WeaponStation station;
        /// <summary>
        /// Gets the station.
        /// </summary>
        /// <value>
        /// The station.
        /// </value>
        [JsonIgnore]
        public WeaponStation Station
        {
            get { return station; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Station));
                }
                station = value;
                if (weaponNamesForGroups.ContainsKey(station.WeaponName))
                {
                    WeaponType = weaponNamesForGroups[station.WeaponName];
                }
                else
                {
                    WeaponType = EWeaponType.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the type of the weapon.
        /// </summary>
        /// <value>
        /// The type of the weapon.
        /// </value>
        public EWeaponType WeaponType { get; private set; }

        /// <summary>
        /// Dictionary to turn weapon names into <see cref="EWeaponType"/>, which are grouped in the jet itself.
        /// </summary>
        protected static readonly Dictionary<string, EWeaponType> weaponNamesForGroups = new Dictionary<string, EWeaponType>()
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
            { "{AN_ASQ_228}", EWeaponType.TPOD }, // AT-FLIR
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
        /// Weapon types that get grouped together in the hornet
        /// This is what the buttons display on the SMS page.
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
            /// The AIM7 family missiles
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
        public static List<EWeaponType> JdamTypes { get; private set; } = new List<EWeaponType>()
        {
            EWeaponType.J82,
            EWeaponType.J83,
            EWeaponType.J84,
            EWeaponType.J109
        };

        public enum EElectronicFuzeSetting
        {
            Off,
            Instant,
            Delayed_1,
            Delayed_2,
            VariableTiming_1,
            VariableTiming_2,
        }

        public enum EMechanicalFuzeSetting
        {
            Off,
            Nose,
            Tail,
            NoseAndTail,
            Primary,
            Optional
        }

        public enum EReleaseMode
        {
            // Basically everything
            Manual,
            // All bombs
            Automatic,
            // Dumb bombs/LGB
            CCIP,
            // Paveway III
            CLAR_CS,
            CLAR_LS,
            // JDAM/JSOW
            LOFT_15,
            LOFT_30,
            LOFT_45,
            Flight_Director
        }

        public List<EElectronicFuzeSetting> ElectricalFuzeSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
