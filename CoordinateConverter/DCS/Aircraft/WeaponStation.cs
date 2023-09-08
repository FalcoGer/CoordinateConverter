using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents a weapon station in DCS
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class WeaponStation :IEquatable<WeaponStation>
    {
        /// <summary>
        /// Gets or sets the station number.
        /// </summary>
        /// <value>
        /// The station number.
        /// </value>
        [JsonProperty("number")]
        public int StationNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the weapon.
        /// </summary>
        /// <value>
        /// The name of the weapon.
        /// </value>
        [JsonProperty("CLSID")]
        public string WeaponName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this pylon is a weapon container, such as a BRU or TER.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a weapon container; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("container")]
        public bool? IsContainer { get; set; }

        /// <summary>
        /// Gets or sets the count of weapons on the pylon.
        /// </summary>
        /// <value>
        /// The number of weapons.
        /// </value>
        [JsonProperty("count")]
        public int? Count { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(WeaponStation other)
        {
            return WeaponName == other.WeaponName && IsContainer == other.IsContainer && Count == other.Count;
        }
    }
}
