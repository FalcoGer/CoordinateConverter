using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Data for a point, specific to the AH64
    /// </summary>
    /// <seealso cref="DCS.Aircraft.AircraftSpecificData" />
    public class AH64SpecificData : AircraftSpecificData
    {
        private AH64.EPointType pointType;
        private AH64.EPointIdent ident;

        /// <summary>
        /// Initializes a new instance of the <see cref="AH64SpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        /// <param name="ident">The ident.</param>
        [JsonConstructor]
        public AH64SpecificData(AH64.EPointType pointType = AH64.EPointType.Waypoint, AH64.EPointIdent ident = AH64.EPointIdent.WP_WP)
        {
            this.pointType = pointType;
            this.ident = ident;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AH64SpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        /// <param name="ident">The ident.</param>
        public AH64SpecificData(string pointType, string ident)
        {
            PointType = pointType;
            Ident = ident;
        }

        public AH64SpecificData(DCSUnit unit)
        {
            if (unit.Type.Level2 == DCSUnitTypeInformation.ELevel2Type.Airdrome)
            {
                pointType = AH64.EPointType.Waypoint;
                ident = AH64.EPointIdent.WP_LZ;
                return;
            }

            pointType = AH64.EPointType.Target;
            ident = AH64.EPointIdent.TG_TG;
            if (unit.Type.Level3 == DCSUnitTypeInformation.ELevel3Type.NoWeapon)
            {
                ident = AH64.EPointIdent.TG_TG;
                return;
            }
            if (unit.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Navy)
            {
                ident = AH64.EPointIdent.TG_NV;
                return;
            }

            switch (unit.TypeName)
            {
                case "rapier_fsa_launcher":
                    ident = AH64.EPointIdent.TG_RA;
                    return;
                case "2B11 mortar":
                    ident = AH64.EPointIdent.TG_TG;
                    return;
                case "NASAMS_LN_B":
                case "NASAMS_LN_C":
                    ident = AH64.EPointIdent.TG_G1;
                    return;
                case "Fire Can radar":
                case "SON_9":
                case "RPC_5N62V":
                    ident = AH64.EPointIdent.TG_TR;
                    return;
                case "ZSU-23-4 Shilka":
                    ident = AH64.EPointIdent.TG_ZU;
                    return;
                case "Roland ADS":
                    ident = AH64.EPointIdent.TG_RO;
                    return;
                case "Hawk ln":
                    ident = AH64.EPointIdent.TG_HK;
                    return;
                case "M48 Chaparral":
                    ident = AH64.EPointIdent.TG_CH;
                    return;
                case "HQ-7_LN_SP":
                case "HQ-7_LN_EO":
                    ident = AH64.EPointIdent.TG_CT;
                    return;
                case "Dog Ear radar":
                case "FPS-117 Dome":
                case "FPS-117":
                case "FPS-117 ECS":
                case "Roland Radar":
                case "RD_75": // SA-2 Range Finder Radar (Auxiliary)
                case "SA-11 Buk SR 9S18M1":
                case "RLS_19J6": // SA-5 SR
                    ident = AH64.EPointIdent.TG_SR;
                    return;
                case "NASAMS_Radar_MPQ64F1":
                case "rapier_fsa_blindfire_radar":
                case "rapier_fsa_optical_tracker_unit":
                case "Patriot str":
                case "Hawk cwar":
                case "HQ-7_STR_SP":
                case "SNR_75V": // SA-2 TR
                case "Kub 1S91 str":
                    ident = AH64.EPointIdent.TG_TR;
                    return;
                case "Patriot ln":
                    ident = AH64.EPointIdent.TG_PT;
                    return;
                case "S_75M_Volhov": // SA-2 LN
                case "HQ_2_Guideline_LN":
                case "S_75M_Volhov_V759": // SA-2 (LN)
                    ident = AH64.EPointIdent.TG_2;
                    return;
                case "5p73 s-125 ln":
                case "5p73 V-601P ln":
                    ident = AH64.EPointIdent.TG_3;
                    return;
                case "S-200_Launcher":
                    ident = AH64.EPointIdent.TG_5;
                    return;
                case "Kub 2P25 ln":
                    ident = AH64.EPointIdent.TG_6;
                    return;
                case "Osa 9A33 ln":
                    ident = AH64.EPointIdent.TG_8;
                    return;
                case "Strela-1 9P31":
                    ident = AH64.EPointIdent.TG_9;
                    return;
                case "SA-11 Buk LN 9A310M1":
                    ident = AH64.EPointIdent.TG_11;
                    return;
                case "Strela-10M3":
                    ident = AH64.EPointIdent.TG_13;
                    return;
                case "SA-14 Strela-3 manpad":
                    ident = AH64.EPointIdent.TG_14;
                    return;
                case "HQ17A":
                case "Tor 9A331":
                    ident = AH64.EPointIdent.TG_15;
                    return;
                case "SA-18 Igla-S comm":
                case "SA-18 Igla-S manpad":
                case "SA-18 Igla comm":
                case "SA-24 Igla-S manpad":
                case "Igla manpad INS":
                case "SA-18 Igla manpad":
                    ident = AH64.EPointIdent.TG_16; // igla
                    return;
                case "SA-17 Buk M1-2 LN 9A310M1-2":
                    ident = AH64.EPointIdent.TG_17;
                    return;
                case "2S6 Tunguska":
                case "PantsirS1":
                case "PantsirS2":
                    ident = AH64.EPointIdent.TG_S6;
                    return;
                case "flak18":
                case "bofors40":
                case "Vulcan":
                case "S-60_Type59_Artillery":
                case "Gepard":
                case "KS-19":
                case "M6 Linebacker":
                case "PGL_625":
                case "2S38":
                case "ZSU_57_2":
                    ident = AH64.EPointIdent.TG_AA;
                    return;
                case "M1097 Avenger":
                case "Stinger comm dsr":
                case "Stinger comm":
                case "Soldier stinger":
                    ident = AH64.EPointIdent.TG_ST;
                    return;
            }

            if (unit.TypeName.StartsWith("S-300") && unit.TypeName.EndsWith(" ln"))
            {
                ident = AH64.EPointIdent.TG_10;
                return;
            }

            if (unit.TypeName.Contains("ZU-23") || unit.TypeName.Contains("ZU23"))
            {
                ident = AH64.EPointIdent.TG_ZU;
                return;
            }

            switch (unit.Type.Level3)
            {
                case DCSUnitTypeInformation.ELevel3Type.NoWeapon:
                    ident = AH64.EPointIdent.TG_TG;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Gun:
                    if (unit.Type.Level4 == 90) // generic infantry
                    {
                        ident = AH64.EPointIdent.TG_MK;
                        return;
                    }
                    if (unit.Type.Level4 == 16) // generic tank
                    {
                        ident = AH64.EPointIdent.TG_GU;
                        return;
                    }
                    if (unit.TypeName == "TACAN_beacon")
                    {
                        ident = AH64.EPointIdent.TG_TG;
                        return;
                    }
                    ident = AH64.EPointIdent.TG_GU;
                    return;
                case DCSUnitTypeInformation.ELevel3Type.Miss:
                    ident = AH64.EPointIdent.TG_TG;
                    return;
                case DCSUnitTypeInformation.ELevel3Type.MissGun:
                    List<string> actuallyGun = new List<string>()
                    {
                        "BMD-1",
                        "BMP-1",
                        "BMP-2",
                        "BMP-3",
                        "BTR_D",
                        "ZBD04A",
                        "M-2 Bradley",
                        "VAB_Mephisto",
                        "M1134 Stryker ATGM",
                        "M1045 HMMWV TOW"
                    };
                    if (actuallyGun.Contains(unit.TypeName))
                    {
                        ident = AH64.EPointIdent.TG_GU;
                        return;
                    }
                    ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_Miss:
                    ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_MissGun:
                    ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_Gun:
                    ident = AH64.EPointIdent.TG_AA;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.AS_TRAIN_Missile:
                    if (unit.TypeName.ToUpper().EndsWith(" TR"))
                    {
                        ident = AH64.EPointIdent.TG_TR;
                        return;
                    }
                    if (unit.TypeName.ToUpper().EndsWith(" SR") || unit.TypeName.ToUpper().StartsWith("EWR ") || unit.TypeName.ToUpper().EndsWith(" EWR"))
                    {
                        ident = AH64.EPointIdent.TG_SR;
                        return;
                    }
                    ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Battleplane:
                case DCSUnitTypeInformation.ELevel3Type.Fighter:
                case DCSUnitTypeInformation.ELevel3Type.F_Bomber:
                case DCSUnitTypeInformation.ELevel3Type.Intercepter:
                    ident = AH64.EPointIdent.TG_TG;
                    return;
            }
            return;
        }

        /// <summary>
        /// Gets or sets the type of the point.
        /// </summary>
        /// <value>
        /// The type of the point.
        /// </value>
        public string PointType
        {
            get
            {
                return pointType.ToString();
            }
            set
            {
                pointType = (AH64.EPointType)Enum.Parse(typeof(AH64.EPointType), value, true);
            }
        }

        /// <summary>
        /// Gets or sets the ident of the point.
        /// </summary>
        /// <value>
        /// The ident of the point.
        /// </value>
        public string Ident
        {
            get
            {
                return ident.ToString();
            }
            set
            {
                ident = (AH64.EPointIdent)Enum.Parse(typeof(AH64.EPointIdent), value, true);
            }
        }

        /// <summary>
        /// Clones the specified other.
        /// </summary>
        /// <returns></returns>
        public override AircraftSpecificData Clone()
        {
            AH64SpecificData ret = new AH64SpecificData(PointType, Ident);
            return ret;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <exception cref="System.Exception">Bad point type</exception>
        public override string ToString()
        {
            string pointTypeStr = string.Empty;
            switch (pointType)
            {
                case AH64.EPointType.Waypoint:
                    pointTypeStr = "WP";
                    break;
                case AH64.EPointType.Hazard:
                    pointTypeStr = "HZ";
                    break;
                case AH64.EPointType.ControlMeasure:
                    pointTypeStr = "CM";
                    break;
                case AH64.EPointType.Target:
                    pointTypeStr = "TG";
                    break;
                default:
                    throw new Exception("Bad point type");
            }
            return pointTypeStr + " / " + Ident.Substring(3);
        }
    }
}
