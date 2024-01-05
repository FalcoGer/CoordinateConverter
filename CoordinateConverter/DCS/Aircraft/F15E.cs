using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents an F15E Aircraft
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSAircraft" />
    public class F15E : DCSAircraft
    {
        /// <summary>
        /// Gets the actions to be added for each item.
        /// </summary>
        /// <param name="item">The item for which the commands are generated.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override List<DCSCommand> GetActions(object item)
        {
            CoordinateDataEntry coordinate = item as CoordinateDataEntry;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>
        /// A list of valid point types.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<string> GetPointTypes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the actions to be used after items have been processed.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override List<DCSCommand> GetPostActions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the actions to be added before items are processed.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override List<DCSCommand> GetPreActions()
        {
            throw new NotImplementedException();
        }
    }
}
