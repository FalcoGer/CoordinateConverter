using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
    public class AH64SpecificData : AircraftSpecificData
    {
        private AH64.EPointType pointType;
        private AH64.EPointIdent ident;

        [JsonConstructor]
        public AH64SpecificData(AH64.EPointType pointType = AH64.EPointType.Waypoint, AH64.EPointIdent ident = AH64.EPointIdent.WP_WP)
        {
            this.pointType = pointType;
            this.ident = ident;
        }

        public AH64SpecificData(string pointType, string ident)
        {
            PointType = pointType;
            Ident = ident;
        }

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
