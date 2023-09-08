using CoordinateConverter.DCS.Aircraft;
using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter
{
    /// <summary>
    /// Holds all the data for a single point in space
    /// </summary>
    public class CoordinateDataEntry
    {
        private int id = 0;
        private string name = string.Empty;
        /// <summary>
        /// Factor for ft and m conversion.
        /// </summary>
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

        /// <summary>
        /// Gets a string representing the altitude.
        /// </summary>
        /// <param name="inFt">if set to <c>true</c> units will be in [ft], otherwise in [m].</param>
        /// <returns></returns>
        public string getAltitudeString(bool inFt)
        {
            string altString = inFt
                ? Math.Round(AltitudeInFt, 1).ToString(System.Globalization.CultureInfo.InvariantCulture) + " ft"
                : Math.Round(AltitudeInM, 1).ToString(System.Globalization.CultureInfo.InvariantCulture) + " m";

            if (AltitudeIsAGL)
            {
                altString += " AGL";
                if (GroundElevationInFt.HasValue)
                {
                    altString += inFt
                        ? string.Format(" [{0} ft]", Math.Round(AltitudeInFt + GroundElevationInFt.Value, 1).ToString(System.Globalization.CultureInfo.InvariantCulture))
                        : string.Format(" [{0} m]", Math.Round(AltitudeInM + GroundElevationInM.Value, 1).ToString(System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            else
            {
                altString += " MSL";
            }

            return altString;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        override public string ToString()
        {
            // altitude value of the point
            string altStr = getAltitudeString(true);
            return string.Format("ID: {0}, Name: {1}, Position: {2} | {3}", Id, Name, getCoordinateStrLLDeg(), altStr);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateDataEntry"/> class.
        /// </summary>
        [JsonConstructor]
        public CoordinateDataEntry()
        {
            Coordinate = new CoordinateSharp.Coordinate();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateDataEntry"/> class.
        /// </summary>
        /// <param name="other">The object to copy from</param>
        public CoordinateDataEntry(CoordinateDataEntry other)
        {
            this.id = other.Id;
            Coordinate = other.Coordinate;
            AltitudeInM = other.AltitudeInM;
            GroundElevationInM = other.GroundElevationInM;
            Name = other.Name ?? String.Empty;
            XFer = other.XFer;
            AltitudeIsAGL = other.AltitudeIsAGL;
            foreach (KeyValuePair<Type, AircraftSpecificData> kvp in other.AircraftSpecificData)
            {
                AircraftSpecificData.Add(kvp.Key, kvp.Value == null ? null : kvp.Value.Clone());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateDataEntry"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="altitudeInM">The altitude in m.</param>
        /// <param name="altitudeIsAGL">if set to <c>true</c> altitude is agl.</param>
        /// <param name="name">The label for this point.</param>
        /// <param name="xfer">if set to <c>true</c> will transfer to DCS.</param>
        /// <exception cref="System.ArgumentNullException">coordinate</exception>
        public CoordinateDataEntry(int id, CoordinateSharp.Coordinate coordinate, double altitudeInM = 0, bool altitudeIsAGL = false, string name = "", bool xfer = true)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }
            this.id = id;
            Coordinate = coordinate;
            AltitudeInM = altitudeInM;
            Name = name ?? String.Empty;
            XFer = xfer;
            AltitudeIsAGL = altitudeIsAGL;
            GroundElevationInM = null;
        }

        /// <summary>
        /// Gets or sets the name of this point.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
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

        /// <summary>
        /// Gets or sets a value indicating whether altitude is agl.
        /// </summary>
        /// <value>
        ///   <c>true</c> if altitude is agl; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "agl")]
        public bool AltitudeIsAGL { get; set; } = false;

        double? groundElevationInM = null;

        /// <summary>
        /// Gets or sets the ground elevation in m.
        /// </summary>
        /// <value>
        /// The ground elevation in m.
        /// </value>
        [JsonProperty(PropertyName = "groundElevation")]
        public double? GroundElevationInM
        {
            get
            {
                if (groundElevationInM == null)
                {
                    try
                    {
                        DCSMessage message = new DCSMessage()
                        {
                            Altitudes = new List<DCSCoordinate>()
                            {
                                new DCSCoordinate(Coordinate)
                            }
                        };
                        message = DCSConnection.sendRequest(message);
                        if (message == null || message.Altitudes == null)
                        {
                            return null;
                        }
                        groundElevationInM = message.Altitudes.First().Elev;
                    }
                    catch (Exception)
                    {
                        // empty
                    }
                }
                return groundElevationInM;
            }
            set
            {
                groundElevationInM = value;
            }
        }

        /// <summary>
        /// Gets the MSL altitude value, adjusted for AGL if available.
        /// </summary>
        /// <param name="inFt">if set to <c>true</c> returns value in feet, otherwise meter.</param>
        /// <returns></returns>
        public double GetAltitudeValue(bool inFt)
        {
            double offsetInM = 0;
            if (AltitudeIsAGL)
            {
                offsetInM = GroundElevationInM ?? 0.0;
            }

            return (AltitudeInM + offsetInM) * (inFt ? CoordinateDataEntry.FT_PER_M : 1.0);
        }

        /// <summary>
        /// Gets or sets the ground elevation in ft.
        /// </summary>
        /// <value>
        /// The ground elevation in ft.
        /// </value>
        [JsonIgnore]
        public double? GroundElevationInFt
        {
            get => GroundElevationInM.HasValue ? GroundElevationInM * FT_PER_M : (double?)null;
            set => groundElevationInM = value / FT_PER_M;
        }

        /// <summary>
        /// Gets or sets the altitude in meter above sea level or ground, depending on <see cref="AltitudeIsAGL"/>
        /// </summary>
        /// <value>
        /// The altitude in m.
        /// </value>
        [JsonProperty(PropertyName = "alt [m]")]
        public double AltitudeInM { get; set; } = 0;

        /// <summary>
        /// Gets or sets the altitude in feet above sea level or ground, depending on <see cref="AltitudeIsAGL"/>
        /// </summary>
        /// <value>
        /// The altitude in ft.
        /// </value>
        [JsonIgnore]
        public double AltitudeInFt { get => AltitudeInM * FT_PER_M; set => AltitudeInM = value / FT_PER_M; }

        private CoordinateSharp.Coordinate coordinate = null;

        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>
        /// The coordinate.
        /// </value>
        [JsonIgnore]
        public CoordinateSharp.Coordinate Coordinate
        {
            get
            {
                return coordinate;
            }
            set
            {
                coordinate = value;
                GroundElevationInM = null;
            }
        }

        /// <summary>
        /// Gets or sets the lattitude.
        /// </summary>
        /// <value>
        /// The lattitude.
        /// </value>
        [JsonProperty(PropertyName = "lat")]
        public double Lat
        {
            get
            {
                return Coordinate.Latitude.DecimalDegree;
            }
            set
            {
                Coordinate.Latitude.DecimalDegree = value;
                GroundElevationInM = null;
            }
        }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty(PropertyName = "long")]
        public double Longi { get => Coordinate.Longitude.DecimalDegree; set => Coordinate.Longitude.DecimalDegree = value; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public int Id { get => id; }

        /// <summary>
        /// Gets or sets a value indicating whether the point is to be transfered to DCS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if transfer is set; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "xfer")]
        public bool XFer { get; set; }

        /// <summary>
        /// Gets or sets the aircraft specific data for this point.
        /// </summary>
        /// <value>
        /// The aircraft specific data.
        /// </value>
        [JsonProperty("AircraftSpecificData")]
        public Dictionary<Type, AircraftSpecificData> AircraftSpecificData { get; set; } = new Dictionary<Type, AircraftSpecificData>();

        /// <summary>
        /// Swaps the ids of two points.
        /// </summary>
        /// <param name="other">The point to swap IDs with</param>
        public void SwapIds(CoordinateDataEntry other)
        {
            int temp = Id;
            this.id = other.id;
            other.id = temp;
        }

        /// <summary>
        /// Gets the coordinate in ll format as a string.
        /// </summary>
        /// <returns></returns>
        public string getCoordinateStrLL()
        {
            formatOptions.Round = 2;
            formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Minutes_Seconds;
            Coordinate.FormatOptions = formatOptions;
            return Coordinate.Display.Replace("º", "°").Replace(",", ".");
        }

        /// <summary>
        /// Gets the coordinate in ll decimal format as a string.
        /// </summary>
        /// <returns></returns>
        public string getCoordinateStrLLDec()
        {
            formatOptions.Format = CoordinateSharp.CoordinateFormatType.Degree_Decimal_Minutes;
            formatOptions.Round = 4;
            Coordinate.FormatOptions = formatOptions;
            return Coordinate.Display.Replace("º", "°").Replace(",", ".");
        }

        /// <summary>
        /// Gets the coordinate in ll deg format as a string.
        /// </summary>
        /// <returns></returns>
        public string getCoordinateStrLLDeg()
        {
            formatOptions.Format = CoordinateSharp.CoordinateFormatType.Decimal_Degree;
            formatOptions.Round = 10;
            Coordinate.FormatOptions = formatOptions;
            return Coordinate.Display.Replace("º", "°").Replace(",", ".");
        }

        /// <summary>
        /// Gets the coordinate in MGRS format as a string.
        /// </summary>
        /// <param name="precision">The precision.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the coordinate in utm format as a string.
        /// </summary>
        /// <returns></returns>
        public string getCoordinateStrUTM()
        {
            CoordinateSharp.UniversalTransverseMercator utm = Coordinate.UTM;
            return utm.ToString();
        }

        /// <summary>
        /// Gets the coordinate in bullseye offset format as a string.
        /// </summary>
        /// <param name="bullseye">The bullseye.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a clone of this point.
        /// </summary>
        /// <param name="newId">The id for the new point.</param>
        /// <returns>A deep copy clone of this object</returns>
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
