using System;
using System.Collections.Generic;
using System.Linq;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// A representation of the KA50-2 or KA50-3
    /// </summary>
    /// <seealso cref="DCSAircraft" />
    public class KA50 : DCSAircraft
    {
        /// <summary>
        /// Gets the actions to be added for each point.
        /// </summary>
        /// <param name="coordinate">The coordinate for that point.</param>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<DCSCommand> GetPointActions(CoordinateDataEntry coordinate)
        {
            if (!coordinate.AircraftSpecificData.ContainsKey(typeof(KA50)))
            {
                coordinate.AircraftSpecificData.Add(typeof(KA50), new KA50SpecificData(EPointType.Waypoint));
            }
            EPointType pt = (coordinate.AircraftSpecificData[typeof(KA50)] as KA50SpecificData).PointType;
            List<DCSCommand> commands = new List<DCSCommand>();
            int nextPointNumber = 0;
            int pointTypeButton = 0;
            switch (pt)
            {
                case EPointType.Waypoint:
                    nextPointNumber = nextPointIdForType[EPointType.Waypoint]++;
                    pointTypeButton = (int)EKeyCodes.Key_WPT;
                    break;
                case EPointType.ReferencePoint:
                    nextPointNumber = nextPointIdForType[EPointType.ReferencePoint]++;
                    pointTypeButton = (int)EKeyCodes.Key_FIXPT;
                    break;
                case EPointType.Airfield:
                    nextPointNumber = nextPointIdForType[EPointType.Airfield]++;
                    pointTypeButton = (int)EKeyCodes.Key_AIRFLD;
                    break;
                case EPointType.TargetPoint:
                    nextPointNumber = nextPointIdForType[EPointType.TargetPoint]++;
                    if (nextPointNumber == 10)
                    {
                        nextPointNumber = 0;
                    }
                    pointTypeButton = (int)EKeyCodes.Key_NavTGT;
                    break;
                default:
                    throw new ArgumentException("Unknown point type: \"" + pt.ToString() + "\"");
            }

            if (nextPointNumber > getMaxPointsForType(pt))
            {
                // Maximum points exceeded
                return commands;
            }

            // press the button to select this point type
            commands.Add(new DCSCommand(PVI_DEVICE_ID, pointTypeButton));

            // Enter point coordinates (Point ID  /  P DD MM m  /  P DDD MM m  /  Enter)
            CoordinateSharp.CoordinateFormatOptions formatOption = new CoordinateSharp.CoordinateFormatOptions()
            {
                Display_Symbols = false,
                Display_Trailing_Zeros = true,
                Display_Leading_Zeros = true,
                Display_Hyphens = false,
                Position_First = true,
                Round = 1,
                Format = CoordinateSharp.CoordinateFormatType.Degree_Decimal_Minutes
            };
            string dataStr = nextPointNumber.ToString() + coordinate.Coordinate.ToString(formatOption) + "\n";

            commands.AddRange(EnterIntoPVI(dataStr));

            // press the button to de-select this point type for the next point to be entered
            commands.Add(new DCSCommand(PVI_DEVICE_ID, pointTypeButton));

            return commands;
        }

        private List<DCSCommand> EnterIntoPVI(string data)
        {
            List<DCSCommand> commands = new List<DCSCommand>();
            foreach (char ch in data)
            {
                switch (ch)
                {
                    case 'N':
                    case 'E':
                    case '+':
                    case '0':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_0_Pos));
                        break;
                    case 'S':
                    case 'W':
                    case '-':
                    case '1':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_1_Neg));
                        break;
                    case '2':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_2));
                        break;
                    case '3':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_3));
                        break;
                    case '4':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_4));
                        break;
                    case '5':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_5));
                        break;
                    case '6':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_6));
                        break;
                    case '7':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_7));
                        break;
                    case '8':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_8));
                        break;
                    case '9':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_9));
                        break;
                    case '\n':
                        commands.Add(new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.Key_ENT));
                        break;
                    default:
                        // ignore anything else
                        break;
                }
            }
            return commands;
        }

        /// <summary>
        /// Gets the type of the point options for point types. <see cref="GetPointTypes" />.
        /// </summary>
        /// <param name="pointTypeStr">The point type's name as a string.</param>
        /// <returns>
        /// A list of names for point options.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<string> GetPointOptionsForType(string pointTypeStr)
        {
            return new List<string>() { "Point" };
        }

        /// <summary>
        /// Gets the types of points that are valid.
        /// </summary>
        /// <returns>
        /// A list of valid point types.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
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
        /// <exception cref="NotImplementedException"></exception>
        public override List<DCSCommand> GetPostPointActions()
        {
            nextPointIdForType = null;
            // Switch PVI mode to OPER
            return new List<DCSCommand>()
            {
                new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.ModeKnob, 100, (int)EModePositions.Operational / 10.0, false)
            };
        }

        /// <summary>
        /// Gets the actions to be added before any points are added.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<DCSCommand> GetPrePointActions()
        {
            nextPointIdForType = new Dictionary<EPointType, int>();
            foreach (EPointType pt in Enum.GetValues(typeof(EPointType)))
            {
                nextPointIdForType.Add(pt, 1);
            }

            // Set PVI mode to Enter
            return new List<DCSCommand>()
            {
                new DCSCommand(PVI_DEVICE_ID, (int)EKeyCodes.ModeKnob, 100, (int)EModePositions.Edit / 10.0, false)
            };
        }

        private int getMaxPointsForType(EPointType pointType)
        {
            switch (pointType)
            {
                case EPointType.Waypoint:
                    return 6;
                case EPointType.ReferencePoint:
                    return 4;
                case EPointType.Airfield:
                    return 2;
                case EPointType.TargetPoint:
                    return 10;
                default:
                    throw new ArgumentException("Unknown \"" + nameof(pointType) + "\": \"" + pointType.ToString() + "\"");
            }
        }

        private Dictionary<EPointType, int> nextPointIdForType = null;

        /// <summary>
        /// The type of the point
        /// </summary>
        public enum EPointType
        {
            /// <summary>
            /// A standard waypoint
            /// </summary>
            Waypoint,
            /// <summary>
            /// A reference point for in flight INU position update
            /// </summary>
            ReferencePoint,
            /// <summary>
            /// An airfield
            /// </summary>
            Airfield,
            /// <summary>
            /// A Nav/Tgt point
            /// </summary>
            TargetPoint
        }

        private const int PVI_DEVICE_ID = 20;
        enum EModePositions
        {
            Off = 0,
            Check,
            Edit,
            Operational,
            Simulate,
            K1,
            K2
        }

        enum EKeyCodes
        {
            Key_0_Pos = 3001,
            Key_1_Neg,
            Key_2,
            Key_3,
            Key_4,
            Key_5,
            Key_6,
            Key_7,
            Key_8,
            Key_9, // 3010
            Key_WPT = 3011,
            Key_INU_RST,
            Key_FIXPT,
            KEY_INU_PREC_ALGN,
            Key_AIRFLD,
            KEY_INU_NORM_ALGN,
            Key_NavTGT,
            Key_ENT = 3018,
            ModeKnob = 3026
            
            
        }
    }
}
