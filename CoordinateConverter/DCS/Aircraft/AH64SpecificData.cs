using Newtonsoft.Json;
using System;

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
