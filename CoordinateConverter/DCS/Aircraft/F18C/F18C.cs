using CoordinateConverter.DCS.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Aircraft.F18C
{
    /// <summary>
    /// Represents F18C aircraft
    /// </summary>
    /// <seealso cref="DCSAircraft" />
    public class F18C : DCSAircraft
    {
        private int numberOfWaypointsEntered = 0;
        private bool isPrecise = false;
        private bool isLLDec = false;
        private int currentWaypoint = 0;
        private bool wpSeqActive = false;

        /// <summary>
        /// Gets the starting waypoint.
        /// </summary>
        /// <value>
        /// The starting waypoint.
        /// </value>
        public int StartingWaypoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="F18C"/> class.
        /// </summary>
        /// <param name="startingWaypoint">The starting waypoint.</param>
        public F18C(int startingWaypoint)
        {
            this.StartingWaypoint = startingWaypoint;
        }

        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="item">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="System.ArgumentException">SLAM-ER single STPT entry not supported</exception>
        protected override List<DCSCommand> GetActions(object item)
        {
            if (numberOfWaypointsEntered + StartingWaypoint >= 59)
            {
                // full list, don't overwrite 59 (Bullseye) or markpoints.
                return null;
            }
            CoordinateDataEntry coordinate = item as CoordinateDataEntry;
            if (!coordinate.AircraftSpecificData.ContainsKey(typeof(F18C)))
            {
                coordinate.AircraftSpecificData.Add(typeof(F18C), new F18CSpecificData());
            }
            F18CSpecificData extraData = coordinate.AircraftSpecificData[typeof(F18C)] as F18CSpecificData;

            if (extraData.PointType != EPointType.WAYPOINT)
            {
                return null; // don't enter non waypoints
            }

            // Is standard waypoint

            // var commands = new DebugCommandList()
            var commands = new List<DCSCommand>
            {
                
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB12, 300), // Increment WP#
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB05, 300), // Open UFC
                new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 300) // POSN
            };

            // Enter latitude
            double latMinutes = coordinate.Coordinate.Latitude.DecimalMinute;
            string latMDecimal = string.Format("{0}\n", (int)Math.Round((latMinutes - (int)latMinutes) * 10000));
            string latDM = string.Format("{0}{1}{2}\n",
                coordinate.Coordinate.Latitude.Position.ToString(),
                coordinate.Coordinate.Latitude.Degrees.ToString().PadLeft(2, '0'),
                coordinate.Coordinate.Latitude.Minutes.ToString().PadLeft(2, '0')
                );
            commands.AddRange(UFCEnterString(latDM));
            commands.AddRange(UFCEnterString(latMDecimal));

            // Enter longitude
            double lonMinutes = coordinate.Coordinate.Longitude.DecimalMinute;
            string lonMDecimal = string.Format("{0}\n", (int)Math.Round((lonMinutes - (int)lonMinutes) * 10000));
            string lonDMS = string.Format("{0}{1}{2}\n",
                coordinate.Coordinate.Longitude.Position.ToString(),
                coordinate.Coordinate.Longitude.Degrees.ToString().PadLeft(2, '0'),
                coordinate.Coordinate.Longitude.Minutes.ToString().PadLeft(2, '0')
                );
            commands.AddRange(UFCEnterString(lonDMS));
            commands.AddRange(UFCEnterString(lonMDecimal));

            // Enter altitude
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB3, 300)); // ELEV
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_PB1, 300)); // FEET
            commands.AddRange(UFCEnterString(((int)Math.Round(coordinate.GetAltitudeValue(true))).ToString() + "\n"));
            numberOfWaypointsEntered++;
            return commands;
        }

        /// <summary>
        /// Enters string into UFC
        /// </summary>
        /// <param name="str">The string to enter. Must only contain digits, '-' and '\n'</param>
        /// <returns>The commands required to enter the string.</returns>
        private List<DCSCommand> UFCEnterString(string str)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            const int delay = 100;
            if (str == "0\n")
            {
                str = "00\n";
            }
            commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_ENT, 600, 0)); // give it a good, long wait
            foreach (char ch in str.ToUpper())
            {
                switch (ch)
                {
                    case '0':
                    case '-':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB0_NEG, delay));
                        break;
                    case '1':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB1, delay));
                        break;
                    case 'N':
                    case '2':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB2_N, delay));
                        break;
                    case '3':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB3, delay));
                        break;
                    case '4':
                    case 'W':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB4_W, delay));
                        break;
                    case '5':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB5, delay));
                        break;
                    case '6':
                    case 'E':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB6_E, delay));
                        break;
                    case '7':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB7, delay));
                        break;
                    case '8':
                    case 'S':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB8_S, delay));
                        break;
                    case '9':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB9, delay));
                        break;
                    case '\n':
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_ENT, 500));
                        commands.Add(new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_ENT, 500, 0)); // wait a bit after pressing enter
                        break;
                    default:
                        throw new ArgumentException(nameof(str) + " must only contain 0 .. 9, -, N, E, S, W, and \\n. But has '" + ch + "'");
                }
            }
            return commands;
        }

        /// <summary>
        /// List of point types
        /// </summary>
        public enum EPointType
        {
            /// <summary>
            /// A waypoint
            /// </summary>
            WAYPOINT,
            /// <summary>
            /// A preplanned target point
            /// </summary>
            WPN_REF_PT
        }

        /// <summary>
        /// The point type strings
        /// </summary>
        public static readonly Dictionary<EPointType, string> PointTypeStrings = new Dictionary<EPointType, string>()
        {
            { EPointType.WAYPOINT, "Waypoint" },
            { EPointType.WPN_REF_PT, "Weapon Reference Point" }
        };

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            return new List<string>
            {
                pointTypeStr
            };
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>
        /// A list of valid point types.
        /// </returns>
        public override List<string> GetPointTypes()
        {
            return Enum.GetNames(typeof(EPointType)).ToList();
        }

        /// <summary>
        /// Gets the actions to be used after points have been entered.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPostActions()
        {
            List<DCSCommand> commands = new List<DCSCommand>
            {
                new DCSCommand((int)EDevices.UFC, (int)EKeyCodes.UFC_KB_CLR, 300)  // clear ufc
            };
            // go back to waypoint we started with (in current waypoint).
            int waypointNumberInAC = StartingWaypoint + numberOfWaypointsEntered - 1;
            int buttonPresses = Math.Abs(currentWaypoint - waypointNumberInAC);

            for (int ctr = 0; ctr < buttonPresses; ctr++)
            {
                if (currentWaypoint < waypointNumberInAC)
                {
                    commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB13, 200)); // back one waypoint
                }
                else
                {
                    commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB12, 200)); // forward one waypoint
                }
            }

            // go further back to last waypoint

            if (!isPrecise)
            {
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB19, 300)); // de-select precise if it wasn't selected before
            }

            if (wpSeqActive)
            {
                // turn sequence back on
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB15, 300));
            }

            if (!isLLDec)
            {
                // select LL Seconds as it was before
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB06, 300));
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB15, 300));
            }

            commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB10, 200)); // go to HSI

            return commands;
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPreActions()
        {
            numberOfWaypointsEntered = 0;

            List<DCSCommand> commands = new List<DCSCommand>
            {
                // Setup AMPCD for waypoints
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 300), // go to TAC or SUPT page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB20, 300), // go to TGT Data or Fuel page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 300), // go to TAC page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB18, 300), // go to SUPT page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB02, 300), // go to HSI page
                new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB10, 300), // go to DATA page
            };

            if (!DCSCommand.RunAndSleep(commands))
            {
                return null;
            }

            // read display to check if precise and coorect coordinate format is selected
            DCSMessage message = new DCSMessage()
            {
                GetCockpitDisplayData = new List<int> { (int)EDisplays.AMPCD }
            };

            var displayData = DCSConnection.SendRequest(message).CockpitDisplayData[(int)EDisplays.AMPCD];

            isPrecise = displayData.ContainsKey("PRECISE_1_box__id:26");
            isLLDec = displayData["WYPT_Longitude"].EndsWith("'");
            currentWaypoint = int.Parse(displayData["WYPT_Page_Number"].Replace("M", "6"));
            if (currentWaypoint > 60) // markpoints start with MK1 and go up to MK9, so wpt 60 should be MK1
            {
                currentWaypoint--;
            }

            wpSeqActive = displayData.ContainsKey("    _1_box__id:21");

            commands = new List<DCSCommand>();

            if (!isPrecise)
            {
                // Select precise coordinates
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB19, 300));
            }

            if (wpSeqActive)
            {
                // press button 5 times to turn sequence off but keep it on the same sequence
                for (int cnt = 0; cnt < 5; cnt++)
                {
                    commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB15, 300));
                }
            }

            // Press PB_12 (Up) or PB_13 (Down) until we reach the starting waypoint
            int buttonPresses = Math.Abs(StartingWaypoint - currentWaypoint) - 1; // we advance the point by 1 first GetActions
            for (int cnt = 0; cnt < buttonPresses; cnt++)
            {
                if (StartingWaypoint > currentWaypoint)
                {
                    commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB12, 300)); // forward 1 point
                }
                else
                {
                    commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB13, 300)); // back 1 point
                }
            }

            if (!isLLDec)
            {
                // select LL Decimal
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB06, 300)); // goto A/C
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB15, 300)); // Select LL Decimal
                commands.Add(new DCSCommand((int)EDevices.AMPCD, (int)EKeyCodes.MDI_PB07, 300)); // go back to WYPT
            }

            return commands;
        }


        /// <summary>
        /// List of displays
        /// </summary>
        public enum EDisplays
        {
            /// <summary>
            /// The heads uo display
            /// </summary>
            HUD = 1,
            /// <summary>
            /// The left MDI
            /// </summary>
            MDI_LEFT = 2,
            /// <summary>
            /// The right MDI
            /// </summary>
            MDI_RIGHT = 3,
            /// <summary>
            /// The advanced multi purpose color display (center MDI)
            /// </summary>
            AMPCD = 4,
            /// <summary>
            /// The IFEI (Engine Instrumentation)
            /// </summary>
            IFEI = 5,
            /// <summary>
            /// The up front controller
            /// </summary>
            UFC = 6,
            /// <summary>
            /// The RWR
            /// </summary>
            RWR = 7,
            /// <summary>
            /// The helmet mounted display
            /// </summary>
            HMD = 8,
        }

        /// <summary>
        /// List of input devices
        /// </summary>
        public enum EDevices
        {
            /// <summary>
            /// The up front controller
            /// </summary>
            UFC = 25,
            /// <summary>
            /// The left MDI
            /// </summary>
            MDI_LEFT = 35,
            /// <summary>
            /// The advanced multi purpose color display (center MDI)
            /// </summary>
            AMPCD = 37,
            /// <summary>
            /// The stores management system (arming panel)
            /// </summary>
            SMS = 23
        }

        // from command_defs.lua, numbers are calculated from the counter() function.        
        /// <summary>
        /// List of key codes
        /// </summary>
        public enum EKeyCodes
        {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            MDI_PB01 = 3011,
            MDI_PB02,
            MDI_PB03,
            MDI_PB04,
            MDI_PB05,
            MDI_PB06, // 3016
            MDI_PB07,
            MDI_PB08,
            MDI_PB09,
            MDI_PB10,
            MDI_PB11, // 3021
            MDI_PB12,
            MDI_PB13,
            MDI_PB14,
            MDI_PB15,
            MDI_PB16, // 3026
            MDI_PB17,
            MDI_PB18,
            MDI_PB19,
            MDI_PB20, // 3030

            UFC_AP = 3001,
            UFC_IFF,
            UFC_TCN,
            UFC_ILS,
            UFC_DL,
            UFC_BCN,
            UFC_ON_OFF,
            UFC_COMM1_FN,
            UFC_COMM2_FN,
            UFC_PB1,
            UFC_PB2,
            UFC_PB3,
            UFC_PB4,
            UFC_PB5,
            UFC_IP,
            UFC_ADF,
            UFC_EMCON,
            UFC_KB0_NEG,
            UFC_KB1,
            UFC_KB2_N,
            UFC_KB3,
            UFC_KB4_W,
            UFC_KB5,
            UFC_KB6_E,
            UFC_KB7,
            UFC_KB8_S,
            UFC_KB9,
            UFC_KB_CLR,
            UFC_KB_ENT,

            AA_MASTER_MODE_SW = 3001,
            AG_MASTER_MODE_SW
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
    }
}
