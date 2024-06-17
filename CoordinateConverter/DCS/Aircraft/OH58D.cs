using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// This class represents the OH58D aircraft
    /// </summary>
    /// <seealso cref="DCSAircraft" />
    public class OH58D : DCSAircraft
    {
        private int delay = 100;
        private bool? isPilot;

        /// <summary>
        /// Initializes a new instance of the <see cref="OH58D"/> class.
        /// </summary>
        public OH58D()
        {
#if DEBUG
            delay = 1000;
#endif
        }
        /// <summary>
        /// Gets or sets a value indicating whether the user is in the pilot or CPG seat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pilot; otherwise, <c>false</c>.
        /// </value>
        public bool? IsPilot
        {
            //I don't think this is going to work
            get
            {
                DCSMessage message = new DCSMessage()
                {
                    GetHandleData = new List<string>()
                    {
                        "SEAT"
                    },
                };
                message = DCSConnection.SendRequest(message);
                if (message != null && message.HandleData.TryGetValue("SEAT", out string value))
                {
                    isPilot = value == "0";
                }
                return isPilot;
            }
            private set
            {
                isPilot = value;
            }

        }
        private enum EDeviceCode
        {
            RMFD = 11,
            MFK = 14,
            LMFD = 23
        }
        
        private enum EKeyCode
        {
            //Keyboard unit
            MFK_1 = 3006,
            MFK_2,
            MFK_3,
            MFK_4,
            MFK_5,
            MFK_6,
            MFK_7,
            MFK_8,
            MFK_9,
            MFK_0,
            MFK_DOT,
            MFK_CLR,
            MFK_ENTER = 3023,
            MFK_MINUS,
            MFK_A,
            MFK_B,
            MFK_C,
            MFK_D,
            MFK_E,
            MFK_F,
            MFK_G,
            MFK_H,
            MFK_I,
            MFK_J,
            MFK_K,
            MFK_L,
            MFK_M,
            MFK_N,
            MFK_O,
            MFK_P,
            MFK_Q,
            MFK_R,
            MFK_S,
            MFK_T,
            MFK_U,
            MFK_V,
            MFK_W,
            MFK_X,
            MFK_Y,
            MFK_Z,

            //MFD
            MFD_L1 = 3001,
            MFD_L2,
            MFD_L3,
            MFD_L4,
            MFD_L5,
            MFD_B1 = 3008,
            MFD_B2,
            MFD_B3,
            MFD_B4,
            MFD_R1 = 3013,
            MFD_R2,
            MFD_R3,
            MFD_R4,
            MFD_R5
        }

        /// <summary>
        /// The valid point types for the OH58D
        /// </summary>
        public enum EPointType
        {
            #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Waypoint,
            Target
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPreActions()
        {
            //This reset the display to the HSD and then 
            List<DCSCommand> commands = new List<DCSCommand>
            {
                new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_B4),
                new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_B2)
            };
            return commands;
        }

        /// <summary>
        /// Gets the actions to be added for each item.
        /// </summary>
        /// <param name="item">The item for which the commands are generated.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Bad Point Type</exception>
        protected override List<DCSCommand> GetActions(object item)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            CoordinateDataEntry coordinate = (CoordinateDataEntry)item;

            OH58DSpecificData extraData = (OH58DSpecificData)coordinate.AircraftSpecificData[typeof(OH58D)];
            commands.Add(new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_R2, delay)); //Nav Setup
            switch (extraData.PointType)
            {
                case EPointType.Waypoint:
                    commands.Add(new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_L4, delay));
                    break;
                case EPointType.Target:
                    commands.Add(new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_R4, delay));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(extraData.PointType.ToString());
            }
            //TODO: Could allow overriding existing points here, but for now will always add to the end of the list
            commands.Add(new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_L2, delay)); //POS
            commands.Add(new DCSCommand((int)EDeviceCode.MFK, (int)EKeyCode.MFK_CLR, delay));
            string strMgrs = RemoveWhitespace(coordinate.GetCoordinateStrMGRS(4));
            commands.AddRange(GetCommandsForMFKText(strMgrs));
            commands.Add(new DCSCommand((int)EDeviceCode.MFK, (int)EKeyCode.MFK_ENTER, delay)); //Accept position
            commands.Add(new DCSCommand((int)EDeviceCode.MFK, (int)EKeyCode.MFK_ENTER, delay)); //Accept altitude
            commands.Add(new DCSCommand((int)EDeviceCode.LMFD, (int)EKeyCode.MFD_R5, delay));   //Store
            commands.AddRange(GetPreActions());
            return commands;
        }

        /// <summary>
        /// Presses TSD button to reset the screen
        /// </summary>
        /// <returns>
        /// A list of actions that result in TSD being pressed.
        /// </returns>
        protected override List<DCSCommand> GetPostActions()
        {
            //None are necessary because we have to reset the display to the HSD after every point because the list could include waypoints and targets
            List<DCSCommand> commands = new List<DCSCommand>();
            return commands;
        }
        
        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>A list of valid point types.</returns>
        public override List<string> GetPointTypes()
        {
            return Enum.GetNames(typeof(EPointType)).ToList();
        }
        
        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes"/>.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>A list of names for point options.</returns>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            //Possible "PREPOINT" should be here
            return new List<string>
            {
                ""
            };
        }

        private static List<DCSCommand> GetCommandsForMFKText(string text)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            foreach (char c in text)
            {
                EKeyCode? keyCode = null;
                switch (c)
                {
                    //Add special characters
                    default:
                        keyCode = (EKeyCode)Enum.Parse(typeof(EKeyCode), "MFK_" + c, true);
                        break;
                }
                commands.Add(new DCSCommand((int)EDeviceCode.MFK, (int)keyCode));
            }
            return commands;
        }
        private static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
