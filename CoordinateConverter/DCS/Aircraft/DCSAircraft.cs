using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents an aircraft in DCS and provides the interface for command sending.
    /// </summary>
    public abstract class DCSAircraft : DCSCommandsPackage
    {
        /// <summary>
        /// Generates the commands.
        /// </summary>
        /// <param name="items">The list of items to generate the commands for.</param>
        /// <returns>A list of commands to be executed by DCS</returns>
        protected override List<DCSCommand> GenerateCommands(object items)
        {
            List<CoordinateDataEntry> coordinateList = items as List<CoordinateDataEntry>;
            coordinateList = coordinateList.Where(x => x.XFer).ToList(); // filter out the ones that are not set for xfer
            if (coordinateList.Count == 0)
            {
                return null;
            }

            List<DCSCommand> commands = GetPreActions();
            foreach (CoordinateDataEntry entry in coordinateList)
            {
                var actions = GetActions(entry);
                if (actions == null)
                {
                    continue;
                }

                commands.AddRange(actions.Where(x => x != null));
            }
            commands.AddRange(GetPostActions());
            return commands;
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>A list of valid point types.</returns>
        public abstract List<string> GetPointTypes();

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes"/>.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>A list of names for point options.</returns>
        public abstract List<string> GetPointOptionsForType(string pointTypeStr);
    }
}
