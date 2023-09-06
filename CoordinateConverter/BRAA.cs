using System;
namespace CoordinateConverter
{
    /// <summary>
    /// Describes the relation between two points.
    /// </summary>
    public class BRA
    {
        private double bearing = 0.0;
        /// <summary>
        /// Gets or sets the bearing.
        /// </summary>
        /// <value>
        /// The bearing.
        /// </value>
        public double Bearing
        {
            get
            {
                return bearing;
            }
            set
            {
                bearing = value % 360;
                if (bearing < 0)
                {
                    bearing += 360;
                }
            }
        }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        public double Range { get; set; }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        public double? Altitude { get; set; }
        /// <summary>
        /// Creates a new BRAA
        /// </summary>
        /// <param name="bearing">Bearing in Degrees</param>
        /// <param name="range">Range in nautical miles</param>
        /// <param name="altitude">Altitude in ft</param>
        public BRA(double bearing, double range, double? altitude = null)
        {
            Bearing = bearing;
            Range = range;
            Altitude = altitude;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return (Math.Round(Bearing * 10)/10).ToString(System.Globalization.CultureInfo.InvariantCulture).PadLeft(3, '0') + "° /" +
                " " + (Math.Round(Range * 10) / 10).ToString(System.Globalization.CultureInfo.InvariantCulture) + " nmi" +
                (Altitude.HasValue ? " @ " + Altitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + " ft" : String.Empty );
        }
    }
}