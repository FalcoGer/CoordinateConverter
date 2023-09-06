using CoordinateSharp;
using System;

namespace CoordinateConverter
{
    /// <summary>
    /// Represents the bullseye
    /// </summary>
    public class Bullseye
    {
        private Coordinate bullseyeCoord = new CoordinateSharp.Coordinate(0.0, 0.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullseye"/> class.
        /// </summary>
        public Bullseye()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullseye"/> class.
        /// </summary>
        /// <param name="lat">The latitude of that point.</param>
        /// <param name="lon">The longitude of that point.</param>
        public Bullseye(double lat, double lon)
        {
            bullseyeCoord = new CoordinateSharp.Coordinate(lat: lat, longi: lon);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullseye"/> class.
        /// </summary>
        /// <param name="bullseyeCoord">The bullseye coordinates.</param>
        /// <exception cref="System.ArgumentNullException">bullseyeCoord</exception>
        public Bullseye(Coordinate bullseyeCoord)
        {
            this.bullseyeCoord = bullseyeCoord ?? throw new ArgumentNullException(nameof(bullseyeCoord));
        }

        /// <summary>
        /// Sets the bullseye.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        /// <param name="lon">The longitude.</param>
        public void SetBullseye(double lat, double lon)
        {
            bullseyeCoord = new CoordinateSharp.Coordinate(lat: lat, longi: lon);
        }

        /// <summary>
        /// Sets the bullseye.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public void SetBullseye(Coordinate coordinate)
        {
            this.bullseyeCoord = coordinate;
        }

        /// <summary>
        /// Gets the bullseye coordinates.
        /// </summary>
        /// <returns>Bullseye coordinates</returns>
        public Coordinate GetBullseye()
        {
            return bullseyeCoord;
        }

        /// <summary>
        /// Gets the coordinates at a specific offset and range.
        /// </summary>
        /// <param name="bra">The bearing and range offset.</param>
        /// <returns></returns>
        public Coordinate GetOffsetPosition(BRA bra)
        {
            Coordinate ret = new Coordinate(bullseyeCoord.Latitude.ToDouble(), bullseyeCoord.Longitude.ToDouble());
            ret.Move(distance: new Distance(bra.Range, DistanceType.NauticalMiles), bra.Bearing, Shape.Ellipsoid);
            return ret;
        }

        /// <summary>
        /// Gets the bearing and range from the bullseye to another point.
        /// </summary>
        /// <param name="coord">The coordinates to get the offset for.</param>
        /// <returns>The bearing and range from bullseye to <paramref name="coord"/></returns>
        /// <exception cref="System.ArgumentNullException">coord</exception>
        public BRA GetBRA(Coordinate coord)
        {
            if (coord == null)
            {
                throw new ArgumentNullException(nameof(coord));
            }

            Distance dist = new Distance(bullseyeCoord, coord, Shape.Ellipsoid);
            return new BRA(bearing: dist.Bearing, range: dist.NauticalMiles);
        }
    }
}