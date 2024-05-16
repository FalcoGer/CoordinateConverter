using CoordinateConverter.DCS.Aircraft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Communication
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
        /// If true, cancels current command input.
        /// </summary>
        /// <value>
        /// Whether to cancel command inputs.
        /// </value>
        [JsonProperty("Stop")]
        public bool? Stop { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to fetch the aircraft type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the response should contain the aircraft type; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("FetchAircraftType")]
        public bool? FetchAircraftType { get; set; } = null;

        /// <summary>
        /// The aircraft type returned from the server
        /// </summary>
        [JsonProperty("Model")]
        public string AircraftType = null;

        /// <summary>
        /// Gets or sets the get handle request.
        /// </summary>
        /// <value>
        /// A list of handle names for which to get the data.
        /// </value>
        [JsonProperty("GetHandleData")]
        public List<string> GetHandleData { get; set; } = null;

        /// <summary>
        /// Gets or sets the handle data.
        /// </summary>
        /// <value>
        /// The handle data returned by the server.
        /// </value>
        [JsonProperty("HandleData")]
        public Dictionary<string, string> HandleData { get; set; } = null;

        /// <summary>
        /// Gets or sets whether to fetch cockpit display data.
        /// </summary>
        /// <value>
        /// Whether cockpit display data is to be gotten
        /// </value>
        [JsonProperty("GetCockpitDisplayData")]
        public List<int> GetCockpitDisplayData { get; set; } = null;
        /// <summary>
        /// Gets or sets the cockpit display data.
        /// </summary>
        /// <value>
        /// The cockpit display data returned by the server.
        /// </value>
        [JsonProperty("CockpitDisplayData")]
        public Dictionary<int, Dictionary<string, string>> CockpitDisplayData { get; set; } = null;

        /// <summary>
        /// Ground elevation requests for lat/long and their responses
        /// </summary>
        [JsonProperty("Altitudes")]
        public List<DCSCoordinate> Altitudes { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to fetch the aircraft type.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the response should contain the aircraft type; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("FetchCameraPosition")]
        public bool? FetchCameraPosition { get; set; } = null;

        /// <summary>
        /// The camera position response from DCS
        /// </summary>
        [JsonProperty("CameraPosition")]
        public DCSCoordinate CameraPosition = null;

        /// <summary>
        /// Is the camera in F10 view?
        /// </summary>
        /// <value>
        /// Serialization only
        /// </value>
        [JsonProperty("isF10")]
        public bool? IsF10View { get; set; } = null;

        /// <summary>
        /// Gets or sets the server error messages.
        /// </summary>
        /// <value>
        /// A potential error messages from the server.
        /// </value>
        [JsonProperty("ErrorList")]
        public List<string> ServerErrors { get; set; } = null;

        /// <summary>
        /// The current command index
        /// </summary>
        [JsonProperty("CmdIdx")]
        public int? CurrentCommandIndex = null;

        /// <summary>
        /// if true, ask the server to send weapon station information
        /// </summary>
        [JsonProperty("FetchWeaponStations")]
        public bool? FetchWeaponStations = null;

        /// <summary>
        /// Gets or sets the weapon stations.
        /// </summary>
        /// <value>
        /// The weapon stations.
        /// </value>
        [JsonProperty("Stations")]
        public List<WeaponStation> WeaponStations { get; set; } = null;

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

        /// <summary>
        /// Gets or sets a value indicating whether dcs should return a list of units
        /// </summary>
        /// <value>
        ///   <c>true</c> will fetch units; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("FetchUnits")]
        public bool? FetchUnits { get; set; } = null;

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        [JsonProperty("Units")]
        public List<DCSUnit> Units { get; set; } = null;
    }
}
