using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Data for a <see cref="CoordinateDataEntry"/> specific to the KA50-2 and KA50-3
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.AircraftSpecificData" />
    public class KA50SpecificData : AircraftSpecificData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KA50SpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        public KA50SpecificData(KA50.EPointType pointType)
        {
            PointType = pointType;
        }

        /// <summary>
        /// Gets or sets the type of the point.
        /// </summary>
        /// <value>
        /// The type of the point.
        /// </value>
        public KA50.EPointType PointType { get; private set; }
        /// <summary>
        /// Clones the instance and returns a copy of itself.
        /// </summary>
        /// <returns>
        /// A clone of the data
        /// </returns>
        public override AircraftSpecificData Clone()
        {
            return new KA50SpecificData(PointType);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return PointType.ToString();
        }
    }
}
