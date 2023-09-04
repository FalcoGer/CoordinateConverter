using System;

namespace CoordinateConverter
{
    internal class CoordinateDataEntry
    {
        private int id = 0;
        private int altitude = 0;
        private string name = string.Empty;
        private CoordinateSharp.CoordinateFormatOptions formatOptions = new CoordinateSharp.CoordinateFormatOptions
        {
            Display_Degree_Symbol = true,
            Display_Minute_Symbol = true,
            Display_Seconds_Symbol = true,
            Display_Leading_Zeros = true,
            Display_Trailing_Zeros = true,

            // Out LL
            Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds,
            Round = 2
        };

        public CoordinateDataEntry(int id, CoordinateSharp.Coordinate coordinate, int altitude = 0, string name = "")
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }
            this.id = id;
            Coordinate = coordinate;
            Altitude = altitude;
            name = name ?? String.Empty;
        }

        public string Name { get => name; set => name = value.Length <= 12 ? value : value.Substring(0, 12); }
        public int Altitude { get => altitude; set => altitude = value; }
        public CoordinateSharp.Coordinate Coordinate { get; set; } = null;
        public int Id { get => id; }

        public void SwapIds(CoordinateDataEntry other)
        {
            int temp = Id;
            this.id = other.id;
            other.id = temp;
        }

        public string getCoordinateStrLL()
        {
            formatOptions.Round = 2;
            formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds;
            Coordinate.FormatOptions = formatOptions;
            return Coordinate.Display.Replace("º", "°").Replace(",", ".");
        }

        public string getCoordinateStrLLDec()
        {
            formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Decimal_Minutes;
            formatOptions.Round = 4;
            Coordinate.FormatOptions = formatOptions;
            return Coordinate.Display.Replace("º", "°").Replace(",", ".");
        }

        public string getCoordinateStrMGRS(int precision = 5)
        {
            if (precision < 0)
            {
                precision = 0;
            }
            if (precision > 5)
            {
                precision = 5;
            }

            CoordinateSharp.MilitaryGridReferenceSystem mgrs = Coordinate.MGRS;

            string ret = mgrs.LongZone.ToString() + mgrs.LatZone + " " + mgrs.Digraph;
            if (precision > 0)
            {
                int factor = (int)Math.Pow(10, 5 - precision);
                int northing = (int)Math.Round(mgrs.Northing / factor);
                int easting = (int)Math.Round(mgrs.Easting / factor);

                ret += " " + northing.ToString() + " " + easting.ToString();
            }
            return ret;
        }

        public string getCoordinateStrUTM()
        {
            CoordinateSharp.UniversalTransverseMercator utm = Coordinate.UTM;
            return utm.ToString();
        }

        public string getCoordinateStrBullseye(Bullseye bullseye)
        {
            if (bullseye == null)
            {
                return "Bullseye not set";
            }
            else
            {
                return bullseye.GetBRA(Coordinate).ToString();
            }
        }

    }
}
