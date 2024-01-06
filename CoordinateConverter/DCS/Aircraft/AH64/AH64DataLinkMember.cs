using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents a DataLinkMember
    /// </summary>
    public class AH64DataLinkMember : IEquatable<AH64DataLinkMember>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64DataLinkMember"/> class.
        /// </summary>
        /// <param name="callsign">The callsign. Minimum length 3.</param>
        /// <param name="subscriberID">The subscriber identifier, See <seealso cref="SubscriberID"/> for format info.</param>
        /// <param name="primary">if set to <c>true</c> member will be a primary member.</param>
        /// <param name="team">if set to <c>true</c> member will be a team member.</param>
        public AH64DataLinkMember(string callsign, string subscriberID, bool primary, bool team)
        {
            Callsign = callsign;
            SubscriberID = subscriberID;
            Primary = primary;
            Team = team;
        }

        private string callsign;
        /// <summary>
        /// Gets or sets the callsign.
        /// </summary>
        /// <value>
        /// The callsign.
        /// </value>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.ArgumentException">Contains invalid characters or too big or too small</exception>
        public string Callsign
        {
            get
            {
                return callsign;
            }
            set
            {
                // must be length 3 to 5 and only contain valid KU inputs
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                value = value.ToUpper();
                string error = AH64DTCData.CheckDLCallSign(value);
                if (error != null)
                {
                    throw new ArgumentException(error, nameof(Callsign));
                }
                callsign = value;
            }
        }
        private string subscriberID;
        /// <summary>
        /// Gets or sets the subscriber identifier.<br></br>
        /// Must be 1 or 2 characters and match the regular expression "^[1-2]?[A-Z]|3[A-I]|[1-3]?[0-9]$".
        /// </summary>
        /// <value>
        /// The subscriber identifier.
        /// </value>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.ArgumentException">SubscriberID has the wrong format</exception>
        public string SubscriberID
        {
            get
            {
                return subscriberID;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value");
                }
                value = value.ToUpper();
                string error = AH64DTCData.CheckDLSubscriberID(value);
                if (error != null)
                {
                    throw new ArgumentException(error, nameof(SubscriberID));
                }
                subscriberID = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AH64DataLinkMember"/> is a primary member.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this member is a primary member; otherwise, <c>false</c>.
        /// </value>
        public bool Primary { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AH64DataLinkMember"/> is a team member.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this member is a team member; otherwise, <c>false</c>.
        /// </value>
        public bool Team { get; set; } = false;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("ID: {0}  C/S: {1} - {2} {3}", SubscriberID.PadLeft(2), Callsign.PadRight(5), Team ? "TEAM" : "    ", Primary ? "PRI" : "   ");
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(AH64DataLinkMember other)
        {
            return SubscriberID == other.SubscriberID && Callsign == other.Callsign && Team == other.Team && Primary == other.Primary;
        }
    }
}
