using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents a DataLinkMember
    /// </summary>
    public class AH64DataLinkMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64DataLinkMember"/> class.
        /// </summary>
        /// <param name="callsign">The callsign. Minimum length 3.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="primary">if set to <c>true</c> [primary].</param>
        /// <param name="team">if set to <c>true</c> [team].</param>
        public AH64DataLinkMember(string callsign, string id, bool primary, bool team)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
