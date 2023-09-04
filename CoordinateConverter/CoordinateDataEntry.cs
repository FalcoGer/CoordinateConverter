using System;

namespace CoordinateConverter
{
    internal class CoordinateDataEntry
    {
        private int id = 0;
        private double altitudeInM = 0;
        private string name = string.Empty;
        public readonly static double FT_PER_M = 3.28084;
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

        override public string ToString()
        {
            return string.Format("ID: {0}, Name: {1}, Position: {2} | {3}ft", Id, Name, Coordinate.ToString(), AltitudeInFt);
        }

        public CoordinateDataEntry(CoordinateDataEntry other)
        {
            this.id = other.Id;
            Coordinate = other.Coordinate;
            AltitudeInM = other.AltitudeInM;
            Name = other.Name ?? String.Empty;
        }

        public CoordinateDataEntry(int id, CoordinateSharp.Coordinate coordinate, double altitudeInM = 0, string name = "")
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }
            this.id = id;
            Coordinate = coordinate;
            AltitudeInM = altitudeInM;
            Name = name ?? String.Empty;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    name = string.Empty;
                }
                else
                {
                    name = value.Length <= 12 ? value : value.Substring(0, 12);
                }
            }
        }
        public double AltitudeInM { get => altitudeInM; set => altitudeInM = value; }
        public double AltitudeInFt { get => (altitudeInM * FT_PER_M); set => altitudeInM = value / FT_PER_M; }
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

            int factor = (int)Math.Pow(10, 5 - precision);
            int northing = (int)Math.Round(mgrs.Northing / factor);
            int easting = (int)Math.Round(mgrs.Easting / factor);
            char digraphEasting = mgrs.Digraph[0];
            char digraphNorthing = mgrs.Digraph[1];

            if (northing * factor >= 100000)
            {
                // Valid A-Z, excluding I and O
                northing = 0;
                if (digraphEasting < 'Z')
                {
                    digraphEasting += (char)1;
                }
                else
                {
                    digraphEasting = 'A';
                }
            }

            if (easting * factor >= 100000)
            {
                // Valid A-V, excluding I and O
                easting = 0;
                if (digraphNorthing < 'V')
                {
                    digraphNorthing += (char)1;
                }
                else
                {
                    digraphNorthing = 'A';
                }
            }
            string digraph = digraphEasting.ToString() + digraphNorthing.ToString();
            mgrs = new CoordinateSharp.MilitaryGridReferenceSystem(mgrs.LatZone, mgrs.LongZone, digraph, easting, northing);

            string ret = mgrs.LongZone.ToString().PadLeft(2, '0') + mgrs.LatZone + " " + mgrs.Digraph;

            if (precision > 0)
            {
                ret += " " + easting.ToString().PadLeft(precision, '0') + " " + northing.ToString().PadLeft(precision, '0');
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

        public CoordinateDataEntry Clone(int? newId = null)
        {
            CoordinateDataEntry entry = new CoordinateDataEntry(this);
            if (newId.HasValue)
            {
                entry.id = newId.Value;
            }
            return entry;
        }
    }
}
