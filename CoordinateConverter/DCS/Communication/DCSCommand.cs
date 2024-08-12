using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Is a command to be interpreted by the DCS lua script as a cockpit action.
    /// To get the numbers for any specific action, check `devices.lua` and `clickabledata.lua` in
    /// the DCS installation directory and then under `/Mods/aircraft/&lt;Type&gt;/Cockpit/Scripts/`
    /// </summary>
    public class DCSCommand
    {

        /// <summary>
        /// Sleeps for commands to finish.
        /// </summary>
        /// <param name="commands">The commands to wait for.</param>
        static public void SleepForCommands(List<DCSCommand> commands)
        {
            int totalDelay = (int)(commands.Sum(c => c.Delay) * 1.2);
            if (totalDelay > 0)
            {
                Thread.Sleep(totalDelay);
            }
        }

        /// <summary>
        /// Runs the commands and sleep.
        /// </summary>
        /// <param name="commands">The commands to run.</param>
        /// <returns>true if it succeeded, false if it failed.</returns>
        static public bool RunAndSleep(List<DCSCommand> commands)
        {
            if ((commands?.Count ?? 0) == 0)
            {
                return true; // don't do anything for empty command list
            }

            var message = new Communication.DCSMessage() { Commands = commands };
            message = DCSConnection.SendRequest(message);

            if (message == null)
            {
                return false;
            }
            DCSCommand.SleepForCommands(commands);
            return true;
        }

        /// <summary>
        /// Empty default constructor for newtonsoft
        /// </summary>
        [JsonConstructor]
        public DCSCommand()
        {
            // Empty
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Device ID</param>
        /// <param name="code">Device button ID</param>
        /// <param name="delay">Delay before depressing the button and/or pressing the next button</param>
        /// <param name="value">Either 1 or -1. Actually a double in DCS, but only for axis commands.</param>
        /// <param name="addDepress">Adds the button depress action right after</param>
        public DCSCommand(int device, int code, int delay = 100, double value = 1, bool addDepress = true)
        {
            Device = device;
            Code = code;
            Delay = delay;
            Value = value;
            AddDepress = addDepress;
        }
    
        /// <summary>
        /// Device ID in cockpit
        /// </summary>
        [JsonProperty("device")]
        public int Device { get; set; }
        
        /// <summary>
        /// Button ID on the device in the cockpit
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Delay before depressing the button and/or pressing the next button
        /// </summary>
        [JsonProperty("delay")]
        public int Delay { get; set; }

        /// <summary>
        /// For hard buttons this is 1 or -1
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }

        /// <summary>
        /// If true, also adds the depress action
        /// </summary>
        [JsonProperty("addDepress")]
        public bool AddDepress { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("D:{0}, C:{1}, Dly: {2}, Val:{3}, Dp: {4}", Device, Code, Delay, Value, AddDepress ? 1 : 0);
        }
    }
}
