using Newtonsoft.Json;
using System;

namespace CoordinateConverter
{
    public class DCSCommand
    {
        [JsonConstructor]
        public DCSCommand()
        {
            // Empty
        }

        public DCSCommand(int device, int code, int delay, int activate, bool addDepress)
        {
            Device = device;
            Code = code;
            Delay = delay;
            Activate = activate;
            AddDepress = addDepress;
        }
    
        [JsonProperty("device")]
        public int Device { get; set; }
        
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("delay")]
        public int Delay { get; set; }

        [JsonProperty("activate")]
        public int Activate { get; set; }

        [JsonProperty("addDepress")]
        public string AddDepressStr
        {
            get
            {
                return AddDepress.ToString().ToLower();
            }
            set
            {
                if (string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
                {
                    AddDepress = false;
                }
                else if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                {
                    AddDepress = true;
                }
                else
                {
                    throw new ArgumentException("Bad value");
                }
            }
        }
        [JsonIgnore]
        public bool AddDepress { get; set; }
    }
}
