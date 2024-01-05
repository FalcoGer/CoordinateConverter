using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents an object which can be turned into a list of key presses to be sent to DCS.
    /// </summary>
    public abstract class DCSCommandsPackage
    {
        /// <summary>
        /// The TCP endpoint
        /// </summary>
        public int SendToDCS(object items)
        {
            List<DCSCommand> commands = GenerateCommands(items);
            if (commands == null)
            {
                return 0;
            }

            DCSMessage message = new DCSMessage()
            {
                Commands = commands
            };

            message = DCSConnection.SendRequest(message);

            if (message == null)
            {
                return 0;
            }

            if (message.ServerErrors != null && message.ServerErrors.Count > 0)
            {
                string errorMessage = string.Join("\n", message.ServerErrors);
                throw new InvalidOperationException(errorMessage);
            }

            return commands.Count;
        }

        /// <summary>
        /// Generates the commands.
        /// </summary>
        /// <param name="items">The list of items to generate the commands for.</param>
        /// <returns>A list of commands to be executed by DCS</returns>
        protected abstract List<DCSCommand> GenerateCommands(object items);

        /// <summary>
        /// Gets the actions to be added before items are processed.
        /// </summary>
        /// <returns>The list of actions.</returns>
        protected abstract List<DCSCommand> GetPreActions();

        /// <summary>
        /// Gets the actions to be added for each item.
        /// </summary>
        /// <param name="item">The item for which the commands are generated.</param>
        /// <returns>The list of actions.</returns>
        protected abstract List<DCSCommand> GetActions(object item);
        /// <summary>
        /// Gets the actions to be used after items have been processed.
        /// </summary>
        /// <returns>The list of actions.</returns>
        protected abstract List<DCSCommand> GetPostActions();
    }
}
