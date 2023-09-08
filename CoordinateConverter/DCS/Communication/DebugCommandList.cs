using CoordinateConverter.DCS.Aircraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Communication
{
    /// <summary>
    /// Interface that will send any command that is placed into the list right away.
    /// </summary>
    public class DebugCommandList : List<DCSCommand>
    {
        /// <summary>
        /// Adds the specified command. And also sends it to the game straight away.
        /// </summary>
        /// <param name="command">The command.</param>
        public new void Add(DCSCommand command)
        {
            base.Add(command);
            DCSMessage message = new DCSMessage() { Commands = new List<DCSCommand>() { command } };
            DCSConnection.sendRequest(message);

            // force sleep, so it prevents stepping through the instructions so fast that the server doesn't have time to open the connection again or is still busy typing
            System.Threading.Thread.Sleep(command.Delay * 2);
        }

        /// <summary>
        /// Adds each command in the range to the collection, but also transmits the command straight away.
        /// </summary>
        /// <param name="commands">The commands.</param>
        public new void AddRange(IEnumerable<DCSCommand> commands)
        {
            base.AddRange(commands);
            DCSMessage message = new DCSMessage() { Commands = commands.ToList() };
            DCSConnection.sendRequest(message);

            // force sleep, so it prevents stepping through the instructions so fast that the server doesn't have time to open the connection again or is still busy typing
            System.Threading.Thread.Sleep(commands.Sum(x => x.Delay) * 2);
        }
    }
}
