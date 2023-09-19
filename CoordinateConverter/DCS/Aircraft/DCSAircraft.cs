using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents an aircraft in DCS and provides the interface for command sending.
    /// </summary>
    public abstract class DCSAircraft
    {
        /// <summary>
        /// The TCP endpoint
        /// </summary>
        public int SendToDCS(List<CoordinateDataEntry> coordinateList)
        {
            List<DCSCommand> commands = GenerateCommands(coordinateList);
            if (commands == null)
            {
                return 0;
            }

            DCSMessage message = new DCSMessage()
            {
                Commands = commands
            };

            message = DCSConnection.SendRequest(message);
            if (message.ServerErrors != null && message.ServerErrors.Count > 0)
            {
                string errorMessage = string.Join("\n", message.ServerErrors);
                throw new InvalidOperationException(errorMessage);
            }

            return commands.Count;
        }
        private List<DCSCommand> GenerateCommands(List<CoordinateDataEntry> coordinateList)
        {
            coordinateList = coordinateList.Where(x => x.XFer).ToList(); // filter out the ones that are not set for xfer
            if (coordinateList.Count == 0)
            {
                return null;
            }

            List<DCSCommand> commands = GetPrePointActions();
            foreach (CoordinateDataEntry entry in coordinateList)
            {
                commands.AddRange(GetPointActions(entry).Where(x => x != null));
            }
            commands.AddRange(GetPostPointActions());
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

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>The list of actions.</returns>
        public abstract List<DCSCommand> GetPrePointActions();

        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="coordinate">The coordinate for that point.</param>
        /// <returns>The list of actions.</returns>
        public abstract List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate);
        /// <summary>
        /// Gets the actions to be used after points have been entered.
        /// </summary>
        /// <returns>The list of actions.</returns>
        public abstract List<DCSCommand> GetPostPointActions();
    }
}
