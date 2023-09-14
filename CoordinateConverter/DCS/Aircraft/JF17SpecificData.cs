namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Point data specific to the JF17
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.AircraftSpecificData" />
    public class JF17SpecificData : AircraftSpecificData
    {
        /// <summary>
        /// Gets the type of the point.
        /// </summary>
        /// <value>
        /// The type of the point.
        /// </value>
        public JF17.EPointType PointType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JF17SpecificData"/> class.
        /// </summary>
        /// <param name="pointType">Type of the point.</param>
        public JF17SpecificData(JF17.EPointType pointType)
        {
            PointType = pointType;
        }
        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
        public override AircraftSpecificData Clone()
        {
            return new JF17SpecificData(PointType);
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
