using System;
namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Data for a point, specific to the AH64
    /// </summary>
    /// <seealso cref="OH58DSpecificData" />
    public class OH58DSpecificData: AircraftSpecificData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OH58DSpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        public OH58DSpecificData(OH58D.EPointType pointType)
        {
            PointType = pointType;
        }
        
        /// <summary>
        /// Gets or sets the type of the point.
        /// </summary>
        /// <value>
        /// The type of the point.
        /// </value>
        public OH58D.EPointType PointType { get; set; }
        
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
        
        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
        public override AircraftSpecificData Clone()
        {
            return new OH58DSpecificData(PointType);
        }
    }
}
