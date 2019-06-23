using System;
namespace CoordinateConverter
{
    /// <summary>
    /// Describes the relation between two points.
    /// </summary>
    public class BRA
    {
        private double bearing = 0.0;
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

        public double Range { get; set; }
        public double? Altitude { get; set; }
        /// <summary>
        /// Creates a new BRAA
        /// </summary>
        /// <param name="bearing">Bearing in Degrees</param>
        /// <param name="range">Range in nautical miles</param>
        /// <param name="altitude">Altitude in ft</param>
        /// <param name="attitude">Attitude in degrees</param>
        public BRA(double bearing, double range, double? altitude = null)
        {
            Bearing = bearing;
            Range = range;
            Altitude = altitude;
        }

        public override string ToString()
        {
            return (Math.Round(Bearing * 10)/10).ToString(System.Globalization.CultureInfo.InvariantCulture).PadLeft(3, '0') + "° /" +
                " " + (Math.Round(Range * 10) / 10).ToString(System.Globalization.CultureInfo.InvariantCulture) + " nmi" +
                (Altitude.HasValue ? " @ " + Altitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + " ft" : String.Empty );
        }
    }
}