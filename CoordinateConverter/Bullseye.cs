using CoordinateSharp;
using System;

namespace CoordinateConverter
{
    public class Bullseye
    {
        private const double METER_PER_FEET = 3.2808399;
        private const double FEET_PER_METER = 1 / METER_PER_FEET;
        private const double METER_PER_NMI = 1852.0;
        private const double NMI_PER_METER = 1 / METER_PER_NMI;

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
            if (bullseyeCoord == null)
            {
                throw new ArgumentNullException(nameof(bullseyeCoord));
            }

            this.bullseyeCoord = bullseyeCoord;
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