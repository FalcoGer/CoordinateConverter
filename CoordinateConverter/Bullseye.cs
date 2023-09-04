using CoordinateSharp;
using System;

namespace CoordinateConverter
{
    public class Bullseye
    {
        private Coordinate bullseyeCoord = new CoordinateSharp.Coordinate(0.0, 0.0);
        
        public Bullseye()
        {
        }

        public Bullseye(double lat, double lon)
        {
            bullseyeCoord = new CoordinateSharp.Coordinate(lat: lat, longi: lon);
        }

        public Bullseye(Coordinate bullseyeCoord)
        {
            this.bullseyeCoord = bullseyeCoord ?? throw new ArgumentNullException(nameof(bullseyeCoord));
        }

        public void SetBullseye(double lat, double lon)
        {
            bullseyeCoord = new CoordinateSharp.Coordinate(lat: lat, longi: lon);
        }

        public void SetBullseye(Coordinate coordinate)
        {
            this.bullseyeCoord = coordinate;
        }

        public Coordinate GetBullseye()
        {
            return bullseyeCoord;
        }

        public Coordinate GetOffsetPosition(BRA bra)
        {
            Coordinate ret = new Coordinate(bullseyeCoord.Latitude.ToDouble(), bullseyeCoord.Longitude.ToDouble());
            ret.Move(distance: new Distance(bra.Range, DistanceType.NauticalMiles), bra.Bearing, Shape.Ellipsoid);
            return ret;
        }

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