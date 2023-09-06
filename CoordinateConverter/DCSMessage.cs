using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter
{
    /// <summary>
    /// Requests to be sent to DCS and answers received by it
    /// </summary>
    public class DCSMessage
    {
        /// <summary>
        /// List of commands to be sent to DCS cockpit
        /// </summary>
        /// <value>
        /// The commands to be sent.
        /// </value>
        [JsonProperty("Commands")]
        public List<DCSCommand> Commands { get; set; } = null;

        /// <summary>
        /// Serialization only
        /// </summary>
        /// <value>
        /// Serialization only
        /// </value>
        [JsonProperty("FetchAircraftType")]
        public string FetchAircraftTypeStr
        {
            get
            {
                return FetchAircraftType.ToString();
            }
            set
            {
                if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                {
                    FetchAircraftType = true;
                }
                else if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                {
                    FetchAircraftType = false;
                }
                else
                {
                    throw new ArgumentException("needs to be 'true' or 'false'");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to fetch the aircraft type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the response should contain the aircraft type; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool FetchAircraftType { get; set; } = false;

        /// <summary>
        /// The aircraft type returned from the server
        /// </summary>
        [JsonProperty("Model")]
        public string AircraftType = null;

        /// <summary>
        /// Ground elevation requests for lat/long and their responses
        /// </summary>
        [JsonProperty("Altitudes")]
        public List<DCSCoordinate> Altitudes { get; set; } = null;

        /// <summary>
        /// Serialization only
        /// </summary>
        /// <value>
        /// Serialization only
        /// </value>
        [JsonProperty("FetchCameraPosition")]
        public string FetchCameraPositionStr
        {
            get
            {
                return FetchCameraPosition.ToString();
            }
            set
            {
                if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                {
                    FetchCameraPosition = true;
                }
                else if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                {
                    FetchCameraPosition = false;
                }
                else
                {
                    throw new ArgumentException("needs to be 'true' or 'false'");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to fetch the aircraft type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the response should contain the aircraft type; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool FetchCameraPosition { get; set; } = false;

        /// <summary>
        /// The camera position response from DCS
        /// </summary>
        [JsonProperty("CameraPosition")]
        public DCSCoordinate CameraPosition = null;

        /// <summary>
        /// Gets or sets the server error message.
        /// </summary>
        /// <value>
        /// A potential error message from the server.
        /// </value>
        [JsonProperty("Error")]
        public string ServerError { get; set; } = null;

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp of when the message was sent or received.
        /// </value>
        [JsonIgnore]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        /// For serialization only
        /// </summary>
        /// <value>
        /// For serialization only
        /// </value>
        [JsonProperty("time")]
        public string TimeStampStr
        {
            get
            {
                return TimeStamp.ToString("s");
            }
            set
            {
                TimeStamp = DateTime.ParseExact(value, "s", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
