using Newtonsoft.Json;
using System;

namespace CoordinateConverter.DCS.Aircraft.F18C
{
    /// <summary>
    /// coordinate data specific to F18C
    /// </summary>
    /// <seealso cref="AircraftSpecificData" />
    public class F18CSpecificData : AircraftSpecificData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="F18CSpecificData"/> class.
        /// This will be a default waypoint
        /// </summary>
        public F18CSpecificData()
        {
             // Empty
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F18CSpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of point.</param>
        [JsonConstructor]
        public F18CSpecificData(F18C.EPointType pointType)
        {
            this.PointType = pointType;
        }

        /// <summary>
        /// Gets or sets the type of the point.
        /// </summary>
        /// <value>
        /// The type of the point.
        /// </value>
        public F18C.EPointType PointType { get; set; } = F18C.EPointType.WAYPOINT;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return F18C.PointTypeStrings[PointType];
        }

        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
        public override AircraftSpecificData Clone()
        {
            return new F18CSpecificData(pointType: PointType);
        }
    }
}
