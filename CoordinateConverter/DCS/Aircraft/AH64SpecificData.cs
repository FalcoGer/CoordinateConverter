using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Data for a point, specific to the AH64
    /// </summary>
    /// <seealso cref="AircraftSpecificData" />
    public class AH64SpecificData : AircraftSpecificData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64SpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        /// <param name="ident">The ident.</param>
        [JsonConstructor]
        public AH64SpecificData(AH64.EPointType pointType = AH64.EPointType.Waypoint, AH64.EPointIdent ident = AH64.EPointIdent.WP_WP)
        {
            PointType = pointType;
            Ident = ident;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AH64SpecificData"/> class.
        /// </summary>
        /// <param name="unit">The unit which will be the base for the point type and ident.</param>
        public AH64SpecificData(DCSUnit unit)
        {
            if (unit.Type.Level2 == DCSUnitTypeInformation.ELevel2Type.Airdrome)
            {
                PointType = AH64.EPointType.Waypoint;
                Ident = AH64.EPointIdent.WP_LZ;
                return;
            }

            PointType = AH64.EPointType.Target;
            Ident = AH64.EPointIdent.TG_TG;
            if (unit.Type.Level3 == DCSUnitTypeInformation.ELevel3Type.NoWeapon)
            {
                Ident = AH64.EPointIdent.TG_TG;
                return;
            }
            if (unit.Type.Level1 == DCSUnitTypeInformation.ELevel1Type.Navy)
            {
                Ident = AH64.EPointIdent.TG_NV;
                return;
            }

            switch (unit.TypeName)
            {
                case "Grad_FDDM": // BMP-2, Fire Director for artillery
                case "MLRS FDDM": // HUMVEE w/ MG, Fire Director for artillery
                    Ident = AH64.EPointIdent.TG_GU;
                    return;
                case "M-109":
                case "SAU 2-C9":        // 2S9
                case "SAU Akatsia":     // 2S3
                case "SAU Gvozdika":    // 2S1
                case "SAU Msta":        // 2S19
                case "SpGH_Dana":
                case "PLZ05":
                case "T155_Firtina":
                    // Self propelled gun
                    Ident = AH64.EPointIdent.TG_GS;
                    return;
                case "Grad-URAL":       // BM-21
                case "HL_B8M1":         // Rocket pod strapped to car
                case "MLRS":            // M270
                case "Smerch":
                case "Smerch_HE":
                case "tt_B8M1":         // Rocket pod strapped to car
                case "Uragan_BM-27":    // BM-27 (Like Smerch, diffrent vehicle)
                    // MLRS
                    // has no separate ident
                    Ident = AH64.EPointIdent.TG_GS;
                    return;
                case "2B11 mortar":
                    // Towed Gun
                    Ident = AH64.EPointIdent.TG_GT;
                    return;
                case "rapier_fsa_launcher":
                    Ident = AH64.EPointIdent.TG_RA;
                    return;
                case "NASAMS_LN_B":
                case "NASAMS_LN_C":
                    Ident = AH64.EPointIdent.TG_G1;  // There isn't anything better...
                    return;
                case "Fire Can radar":
                case "SON_9":
                case "RPC_5N62V":
                    Ident = AH64.EPointIdent.TG_TR;
                    return;
                case "ZSU-23-4 Shilka":
                    Ident = AH64.EPointIdent.TG_ZU;
                    return;
                case "Roland ADS":
                    Ident = AH64.EPointIdent.TG_RO;
                    return;
                case "Hawk ln":
                    Ident = AH64.EPointIdent.TG_HK;
                    return;
                case "M48 Chaparral":
                    Ident = AH64.EPointIdent.TG_CH;
                    return;
                case "HQ-7_LN_SP":
                case "HQ-7_LN_EO":
                    Ident = AH64.EPointIdent.TG_CT;
                    return;
                case "Dog Ear radar":
                case "FPS-117 Dome":
                case "FPS-117":
                case "FPS-117 ECS":
                case "Roland Radar":
                case "RD_75": // SA-2 Range Finder Radar (Auxiliary)
                case "SA-11 Buk SR 9S18M1":
                case "RLS_19J6": // SA-5 SR
                    Ident = AH64.EPointIdent.TG_SR;
                    return;
                case "NASAMS_Radar_MPQ64F1":
                case "rapier_fsa_blindfire_radar":
                case "rapier_fsa_optical_tracker_unit":
                case "Patriot str":
                case "Hawk cwar":
                case "HQ-7_STR_SP":
                case "SNR_75V": // SA-2 TR
                case "Kub 1S91 str":
                    Ident = AH64.EPointIdent.TG_TR;
                    return;
                case "Patriot ln":
                    Ident = AH64.EPointIdent.TG_PT;
                    return;
                case "S_75M_Volhov": // SA-2 LN
                case "HQ_2_Guideline_LN":
                case "S_75M_Volhov_V759": // SA-2 (LN)
                    Ident = AH64.EPointIdent.TG_2;
                    return;
                case "5p73 s-125 ln":
                case "5p73 V-601P ln":
                    Ident = AH64.EPointIdent.TG_3;
                    return;
                case "S-200_Launcher":
                    Ident = AH64.EPointIdent.TG_5;
                    return;
                case "Kub 2P25 ln":
                    Ident = AH64.EPointIdent.TG_6;
                    return;
                case "Osa 9A33 ln":
                    Ident = AH64.EPointIdent.TG_8;
                    return;
                case "Strela-1 9P31":
                    Ident = AH64.EPointIdent.TG_9;
                    return;
                case "SA-11 Buk LN 9A310M1":
                    Ident = AH64.EPointIdent.TG_11;
                    return;
                case "Strela-10M3":
                    Ident = AH64.EPointIdent.TG_13;
                    return;
                case "SA-14 Strela-3 manpad":
                    Ident = AH64.EPointIdent.TG_14;
                    return;
                case "HQ17A":
                case "Tor 9A331":
                    Ident = AH64.EPointIdent.TG_15;
                    return;
                case "SA-18 Igla-S comm":
                case "SA-18 Igla-S manpad":
                case "SA-18 Igla comm":
                case "SA-24 Igla-S manpad":
                case "Igla manpad INS":
                case "SA-18 Igla manpad":
                    Ident = AH64.EPointIdent.TG_16; // igla
                    return;
                case "SA-17 Buk M1-2 LN 9A310M1-2":
                    Ident = AH64.EPointIdent.TG_17;
                    return;
                case "2S6 Tunguska":
                case "PantsirS1":
                case "PantsirS2":
                    Ident = AH64.EPointIdent.TG_S6;
                    return;
                case "flak18":
                case "bofors40":
                case "S-60_Type59_Artillery":
                case "KS-19":
                case "M6 Linebacker":
                case "PGL_625":
                case "2S38":
                case "ZSU_57_2":
                    Ident = AH64.EPointIdent.TG_AA;
                    return;
                case "Gepard":
                    Ident = AH64.EPointIdent.TG_GP;
                    return;
                case "Vulcan":
                    Ident = AH64.EPointIdent.TG_VU;
                    return;
                case "HEMTT_C-RAM_Phalanx":
                    Ident = AH64.EPointIdent.TG_G2; // There isn't anything better...
                    return;
                case "M1097 Avenger":
                case "Stinger comm dsr":
                case "Stinger comm":
                case "Soldier stinger":
                    Ident = AH64.EPointIdent.TG_ST;
                    return;
            }

            if (unit.TypeName.StartsWith("S-300") && unit.TypeName.EndsWith(" ln"))
            {
                Ident = AH64.EPointIdent.TG_10;
                return;
            }

            if (unit.TypeName.Contains("ZU-23") || unit.TypeName.Contains("ZU23"))
            {
                Ident = AH64.EPointIdent.TG_ZU;
                return;
            }

            switch (unit.Type.Level3)
            {
                case DCSUnitTypeInformation.ELevel3Type.NoWeapon:
                    Ident = AH64.EPointIdent.TG_TG;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Gun:
                    if (unit.Type.Level4 == 90) // generic infantry
                    {
                        Ident = AH64.EPointIdent.TG_MK;
                        return;
                    }
                    if (unit.Type.Level4 == 16) // generic tank
                    {
                        Ident = AH64.EPointIdent.TG_GU;
                        return;
                    }
                    if (unit.TypeName == "TACAN_beacon")
                    {
                        Ident = AH64.EPointIdent.TG_TG;
                        return;
                    }
                    Ident = AH64.EPointIdent.TG_GU;
                    return;
                case DCSUnitTypeInformation.ELevel3Type.Miss:
                    Ident = AH64.EPointIdent.TG_TG;
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
                        Ident = AH64.EPointIdent.TG_GU;
                        return;
                    }
                    Ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_Miss:
                    Ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_MissGun:
                    Ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Radar_Gun:
                    Ident = AH64.EPointIdent.TG_AA;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.AS_TRAIN_Missile:
                    if (unit.TypeName.ToUpper().EndsWith(" TR"))
                    {
                        Ident = AH64.EPointIdent.TG_TR;
                        return;
                    }
                    if (unit.TypeName.ToUpper().EndsWith(" SR") || unit.TypeName.ToUpper().StartsWith("EWR ") || unit.TypeName.ToUpper().EndsWith(" EWR"))
                    {
                        Ident = AH64.EPointIdent.TG_SR;
                        return;
                    }
                    Ident = AH64.EPointIdent.TG_U;
                    break;
                case DCSUnitTypeInformation.ELevel3Type.Battleplane:
                case DCSUnitTypeInformation.ELevel3Type.Fighter:
                case DCSUnitTypeInformation.ELevel3Type.F_Bomber:
                case DCSUnitTypeInformation.ELevel3Type.Intercepter:
                    Ident = AH64.EPointIdent.TG_TG;
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
        public AH64.EPointType PointType { get; set; }

        /// <summary>
        /// Gets or sets the ident of the point.
        /// </summary>
        /// <value>
        /// The ident of the point.
        /// </value>
        public AH64.EPointIdent Ident { get; set; }

        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
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
        /// <exception cref="Exception">Bad point type</exception>
        public override string ToString()
        {
            string pointTypeStr = string.Empty;
            switch (PointType)
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
            return pointTypeStr + " / " + Ident.ToString().Substring(3);
        }
    }
}
