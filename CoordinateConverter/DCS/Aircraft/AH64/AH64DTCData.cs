using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents the DTC Data for an AH64
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSCommandsPackage" />
    public class AH64DTCData : DCSCommandsPackage
    {
        /// <summary>
        /// Generates the commands.
        /// </summary>
        /// <param name="items">The list of items to generate the commands for.</param>
        /// <returns>
        /// A list of commands to be executed by DCS
        /// </returns>
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
                commands.AddRange(GetActions(entry).Where(x => x != null));
            }
            commands.AddRange(GetPostActions());
            return commands;
        }

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
