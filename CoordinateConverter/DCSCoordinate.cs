using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace CoordinateConverter
{
    /// <summary>
    /// Coordinate format as handled by DCS for ground elevation requests
    /// </summary>
    public class DCSCoordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DCSCoordinate"/> class.
        /// </summary>
        public DCSCoordinate()
        {
            // Empty, just for serialization
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DCSCoordinate"/> class.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        /// <param name="lon">The longitude.</param>
        /// <param name="alt">The altitude.</param>
        /// <param name="elev">The ground elevation.</param>
        public DCSCoordinate(double lat, double lon, double? alt = null, double? elev = null)
        {
            Lat = lat;
            Lon = lon;
            Alt = alt;
            Elev = elev;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DCSCoordinate"/> class.
        /// </summary>
        /// <param name="coordinate">A coordinate.</param>
        public DCSCoordinate(CoordinateSharp.Coordinate coordinate)
        {
            Lat = coordinate.Latitude.DecimalDegree;
            Lon = coordinate.Longitude.DecimalDegree;
            Alt = null;
            Elev = null;
        }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        /// <returns></returns>
        public CoordinateSharp.Coordinate getCoordinate()
        {
            return new CoordinateSharp.Coordinate(Lat, Lon);
        }

        /// <summary>
        /// Lattitude
        /// </summary>
        /// <value>
        /// The lattitude
        /// </value>
        [JsonProperty("Lat")]
        public double Lat { get; set; }

        /// <summary>
        /// Gets or sets the lonigutde.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("Lon")]
        public double Lon { get; set; }

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>
        /// The altitude.
        /// </value>
        [JsonProperty("Alt")]
        public double? Alt { get; set; }

        /// <summary>
        /// Gets or sets the ground elevation.
        /// </summary>
        /// <value>
        /// The elevation.
        /// </value>
        [JsonProperty("Elev")]
        public double? Elev { get; set; }
    }
}
