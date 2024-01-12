using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents the DTC Data for an AH64
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.DCSCommandsPackage" />
    public class AH64DTCData : DCSCommandsPackage
    {
        /// <summary>
        /// Gets a value indicating whether this instance is pilot.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pilot; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsPilot { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64DTCData"/> class.
        /// </summary>
        [JsonConstructor]
        public AH64DTCData()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AH64DTCData"/> class.
        /// </summary>
        /// <param name="isPilot">if set to <c>true</c> [is pilot].</param>
        public AH64DTCData(bool isPilot)
        {
            IsPilot = isPilot;
            Init();
        }

        private void Init()
        {
            // Fill preset data
            presetDataDictionary = new Dictionary<EPreset, AH64RadioPresetData>();
            foreach (EPreset preset in Enum.GetValues(typeof(EPreset)))
            {
                AH64RadioPresetData presetData = new AH64RadioPresetData();
                presetDataDictionary.Add(preset, presetData);
            }
        }

        /// <summary>
        /// Checks the dl call sign for validity.
        /// </summary>
        /// <param name="callsign">The callsign to check.</param>
        /// <returns>An error message or null</returns>
        public static string CheckDLCallSign(string callsign)
        {
            if (callsign.Length < 3)
            {
                return "Callsign must contain at least 3 characters";
            }
            if (callsign.Length > 5) {
                return "Callsign must contain at most 5 characters";
            }
            if (!AH64.GetIsValidTextForKU(callsign, 3, 5))
            {
                return "Callsign contains invalid characters";
            }
            return null;
        }

        /// <summary>
        /// Checks the datalink subscriber identifier.
        /// </summary>
        /// <param name="subscriberID">The value to check.</param>
        /// <returns>An error message or null</returns>
        public static string CheckDLSubscriberID(string subscriberID)
        {
            // must be 1 or 2 characters
            if (subscriberID.Length < 1 || subscriberID.Length > 2)
            {
                return "SubscriberID must be 1 or 2 characters";
            }

            // Must be 0-39 (no leading zeroes), A-Z, 1A-1Z, 2A-2Z, 3A-3I
            string pattern = "^[1-2]?[A-Z]|3[A-I]|[1-3]?[0-9]$";
            var match = Regex.Match(subscriberID, pattern);
            if (!match.Success || !(match.Length == subscriberID.Length))
            {
                return "SubscriberID must be 0-39 without leading zeroes, A-Z, 1A-1Z, 2A-2Z or 3A-3I";
            }
            return null;
        }

        #region Presets
        /// <summary>
        /// A Radio Preset ID
        /// </summary>
        public enum EPreset
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Preset1,
            Preset2,
            Preset3,
            Preset4,
            Preset5,
            Preset6,
            Preset7,
            Preset8,
            Preset9,
            Preset10
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        [JsonProperty("Presets")]
        private Dictionary<EPreset, AH64RadioPresetData> presetDataDictionary;
        /// <summary>
        /// Gets AH64 radio preset.
        /// </summary>
        /// <param name="preset">The preset.</param>
        /// <returns></returns>
        public AH64RadioPresetData GetAH64RadioPreset(EPreset preset)
        {
            return presetDataDictionary[preset];
        }

        /// <summary>
        /// Sets AH64 radio preset data.
        /// </summary>
        /// <param name="preset">The preset.</param>
        /// <param name="presetData">The new preset data.</param>
        public void SetAH64RadioPresetData(EPreset preset, AH64RadioPresetData presetData)
        {
            presetDataDictionary[preset] = presetData;
        }

        #endregion

        #region XPNDR        
        /// <summary>
        /// Gets a value indicating whether [this DTC contains transponder data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [this DTC contains transponder data]; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool ContainsTransponderData
        {
            get
            {
                return mode1Value.HasValue
                    || mode2Value.HasValue
                    || mode3AValue.HasValue
                    || Mode4 != EMode4Options.No_Change
                    || (!string.IsNullOrEmpty(modeSFlightIDValue) && modeSFlightAddressValue.HasValue)
                    || IFFReply != EIFFReply.No_Change;
            }
        }
        [JsonIgnore]
        private int? mode1Value = null; // 5 bits (0..31)        
        /// <summary>
        /// Gets or sets the Mode1 transponder code.
        /// </summary>
        /// <value>
        /// The Mode1 code.
        /// </value>
        /// <exception cref="System.ArgumentException">Must only use digits 0 though 7 - Mode1</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Mode1 - Must be between 0 and " + Convert.ToString(31, 8)</exception>
        public string Mode1
        {
            get
            {
                return mode1Value.HasValue ? Convert.ToString(mode1Value.Value, 8) : string.Empty;
            }
            set
            {
                const int DIGITS = 2;
                if (string.IsNullOrEmpty(value))
                {
                    mode1Value = null;
                    return;
                }

                if (value.Length != DIGITS)
                {
                    throw new ArgumentException("Must only use exactly " + DIGITS.ToString() + " digits (0-7)", nameof(Mode1));
                }

                int temp = 0;

                try
                {
                    temp = Convert.ToInt32(value, 8);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Must only use digits 0 though 7", nameof(Mode1), ex);
                }

                if (temp < 0 || temp > 31)
                {
                    throw new ArgumentOutOfRangeException(nameof(Mode1), temp, "Must be between 0 and " + Convert.ToString(31, 8));
                }

                mode1Value = temp;
            }
        }

        [JsonIgnore]
        private int? mode2Value = null;
        /// <summary>
        /// Gets or sets the mode2 transponder code.
        /// </summary>
        /// <value>
        /// The mode2.
        /// </value>
        /// <exception cref="System.ArgumentException">
        /// Must only use exactly " + DIGITS.ToString() + " digits (0-7) - Mode2
        /// or
        /// Must only use digits 0 though 7 - Mode2
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Mode2 - Must be between 0 and " + Convert.ToString(31, 8)</exception>
        public string Mode2
        {
            get
            {
                return mode2Value.HasValue ? Convert.ToString(mode2Value.Value, 8) : string.Empty;
            }
            set
            {
                const int DIGITS = 4;
                if (string.IsNullOrEmpty(value))
                {
                    mode2Value = null;
                    return;
                }

                if (value.Length != DIGITS)
                {
                    throw new ArgumentException("Must only use exactly " + DIGITS.ToString() + " digits (0-7)", nameof(Mode2));
                }

                int temp = 0;

                try
                {
                    temp = Convert.ToInt32(value, 8);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Must only use digits 0 though 7", nameof(Mode2), ex);
                }

                if (temp < 0 || temp > Convert.ToInt32("7777", 8))
                {
                    throw new ArgumentOutOfRangeException(nameof(Mode2), temp, "Must be between 0 and " + Convert.ToString(31, 8));
                }

                mode2Value = temp;
            }
        }

        [JsonIgnore]
        private int? mode3AValue = null;
        /// <summary>
        /// Gets or sets the mode3a transponder code.
        /// </summary>
        /// <value>
        /// The mode3a transponder code.
        /// </value>
        /// <exception cref="System.ArgumentException">
        /// Must only use exactly " + DIGITS.ToString() + " digits (0-7) - Mode3A
        /// or
        /// Must only use digits 0 though 7 - Mode3A
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Mode3A - Must be between 0 and " + Convert.ToString(31, 8)</exception>
        public string Mode3A
        {
            get
            {
                return mode3AValue.HasValue ? Convert.ToString(mode3AValue.Value, 8) : string.Empty;
            }
            set
            {
                const int DIGITS = 4;
                if (string.IsNullOrEmpty(value))
                {
                    mode3AValue = null;
                    return;
                }

                if (value.Length != DIGITS)
                {
                    throw new ArgumentException("Must only use exactly " + DIGITS.ToString() + " digits (0-7)", nameof(Mode3A));
                }

                int temp = 0;

                try
                {
                    temp = Convert.ToInt32(value, 8);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Must only use digits 0 though 7", nameof(Mode3A), ex);
                }

                if (temp < 0 || temp > Convert.ToInt32("7777", 8))
                {
                    throw new ArgumentOutOfRangeException(nameof(Mode3A), temp, "Must be between 0 and " + Convert.ToString(31, 8));
                }

                mode3AValue = temp;
            }
        }

        [JsonIgnore]
        private int? modeSFlightAddressValue = null;
        /// <summary>
        /// Gets or sets the mode s flight address.
        /// </summary>
        /// <value>
        /// The mode s flight address.
        /// </value>
        /// <exception cref="System.ArgumentException">
        /// Must only use exactly " + DIGITS.ToString() + " digits (0-7) - ModeSFlightAddress
        /// or
        /// Must only use digits 0 though 7 - ModeSFlightAddress
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">ModeSFlightAddress - Must be between 0 and " + Convert.ToString(31, 8)</exception>
        public string ModeSFlightAddress
        {
            get
            {
                return modeSFlightAddressValue.HasValue ? Convert.ToString(modeSFlightAddressValue.Value, 8) : string.Empty;
            }
            set
            {
                const int DIGITS = 8;
                if (string.IsNullOrEmpty(value))
                {
                    modeSFlightAddressValue = null;
                    return;
                }

                if (value.Length != DIGITS)
                {
                    throw new ArgumentException("Must only use exactly " + DIGITS.ToString() + " digits (0-7)", nameof(ModeSFlightAddress));
                }

                int temp = 0;

                try
                {
                    temp = Convert.ToInt32(value, 8);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Must only use digits 0 though 7", nameof(ModeSFlightAddress), ex);
                }

                if (temp < 0 || temp > Convert.ToInt32("77777777", 8))
                {
                    throw new ArgumentOutOfRangeException(nameof(ModeSFlightAddress), temp, "Must be between 0 and " + Convert.ToString(31, 8));
                }

                modeSFlightAddressValue = temp;
            }
        }

        [JsonIgnore]
        private string modeSFlightIDValue = null;
        /// <summary>
        /// Gets or sets the mode s flight identifier.
        /// </summary>
        /// <value>
        /// The mode s flight identifier.
        /// </value>
        /// <exception cref="System.ArgumentException">Must only use exactly " + REQ_LENGTH.ToString() + " valid characters. - ModeSFlightID</exception>
        public string ModeSFlightID
        {
            get
            {
                return modeSFlightIDValue ?? string.Empty;
            }
            set
            {
                const int REQ_LENGTH = 8;
                if (string.IsNullOrEmpty(value))
                {
                    modeSFlightIDValue = null;
                    return;
                }

                if (!AH64.GetIsValidTextForKU(value, REQ_LENGTH, REQ_LENGTH))
                {
                    throw new ArgumentException("Must only use exactly " + REQ_LENGTH.ToString() + " valid characters.", nameof(ModeSFlightID));
                }

                modeSFlightIDValue = value;
            }
        }

        /// <summary>
        /// Options for Mode 4 selection
        /// </summary>
        public enum EMode4Options
        {
            /// <summary>
            /// Do not change value currenly in the aircraft.
            /// </summary>
            No_Change,
            /// <summary>
            /// Use crypto key A
            /// </summary>
            A,
            /// <summary>
            /// Use crypto key B
            /// </summary>
            B
        }

        /// <summary>
        /// Gets or sets the mode4.
        /// </summary>
        /// <value>
        /// The mode4.
        /// </value>
        public EMode4Options Mode4 { get; set; } = EMode4Options.No_Change;

        /// <summary>
        /// The IFF Reply options
        /// </summary>
        public enum EIFFReply
        {
            /// <summary>
            /// Leave value in the aircraft as it is.
            /// </summary>
            No_Change,
            /// <summary>
            /// Display an advisory on IFF reply and an audio tone.
            /// </summary>
            UFD_and_Audio,
            /// <summary>
            /// Display an advisory on IFF reply.
            /// </summary>
            UFD_only,
            /// <summary>
            /// No advisories on IFF reply.
            /// </summary>
            None
        }

        /// <summary>
        /// Gets or sets the iff reply option.
        /// </summary>
        /// <value>
        /// The iff reply option.
        /// </value>
        public EIFFReply IFFReply { get; set; } = EIFFReply.No_Change;

        /// <summary>
        /// The IFF Antenna selection options
        /// </summary>
        public enum EIFFAntenna
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Top,
            Div,
            Bottom
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the iff antenna.
        /// </summary>
        /// <value>
        /// The iff antenna.
        /// </value>
        public EIFFAntenna IFFAntenna { get; set; } = EIFFAntenna.No_Change;
        #endregion

        #region CommandGeneration
        /// <summary>
        /// Generates the commands.
        /// </summary>
        /// <param name="items">The list of items to generate the commands for.</param>
        /// <returns>
        /// A list of commands to be executed by DCS
        /// </returns>
        protected override List<DCSCommand> GenerateCommands(object items)
        {
            List<DCSCommand> commands = GetPreActions();
            commands.AddRange(GetActions(items));
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
            var commands = new List<DCSCommand>();
            // var commands = new DebugCommandList();

            if (ContainsTransponderData)
            {
                // RMFD to comms
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_COM));
                // XPNDR
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T3));
                if (!string.IsNullOrEmpty(Mode1))
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R1));
                    commands.AddRange(AH64.GetCommandsForKUText(Mode1 + '\n', true, IsPilot));
                }
                if (!string.IsNullOrEmpty(Mode2))
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R2));
                    commands.AddRange(AH64.GetCommandsForKUText(Mode2 + '\n', true, IsPilot));
                }
                if (!string.IsNullOrEmpty(Mode3A))
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R3));
                    commands.AddRange(AH64.GetCommandsForKUText(Mode3A + '\n', true, IsPilot));
                }
                if (Mode4 != EMode4Options.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R4));
                    commands.AddRange(AH64.GetCommandsForKUText(Mode4.ToString() + '\n', true, IsPilot));
                }
                if (!string.IsNullOrEmpty(ModeSFlightID) && modeSFlightAddressValue.HasValue)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R5));
                    commands.AddRange(AH64.GetCommandsForKUText(ModeSFlightID + '\n' + ModeSFlightAddress + '\n', true, IsPilot));
                }
                if (IFFReply != EIFFReply.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B4));
                    switch (IFFReply)
                    {
                        case EIFFReply.No_Change:
                            throw new Exception("Logic error");
                        case EIFFReply.UFD_and_Audio:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B4));
                            break;
                        case EIFFReply.UFD_only:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B5));
                            break;
                        case EIFFReply.None:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B6));
                            break;
                    }
                }

                if (IFFAntenna != EIFFAntenna.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B2));
                    switch (IFFAntenna)
                    {
                        case EIFFAntenna.No_Change:
                            throw new Exception("Logic error");
                        case EIFFAntenna.Top:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B2));
                            break;
                        case EIFFAntenna.Div:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B3));
                            break;
                        case EIFFAntenna.Bottom:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B4));
                            break;
                    }
                }
            }

            // ASE
            if (ASEAutopage != EASEAutopage.No_Change)
            {
                commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_R1)); // Autopage
                switch (ASEAutopage)
                {
                    case EASEAutopage.No_Change:
                        throw new Exception("Logic error");
                    case EASEAutopage.Search:
                        commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_R1));
                        break;
                    case EASEAutopage.Acquisition:
                        commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_R2));
                        break;
                    case EASEAutopage.Track:
                        commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_R3));
                        break;
                    case EASEAutopage.Off:
                        commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_R4));
                        break;
                }
            }

            // Chaff Program
            if (ContainsChaffProgramData)
            {
                commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_T6)); // UTIL
                if (ASEBurstCount != EASEBurstCount.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L2));
                    switch (ASEBurstCount)
                    {
                        case EASEBurstCount.No_Change:
                            throw new Exception("Logic error");
                        case EASEBurstCount.One:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L6));
                            break;
                        case EASEBurstCount.Two:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L5));
                            break;
                        case EASEBurstCount.Three:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L4));
                            break;
                        case EASEBurstCount.Four:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3));
                            break;
                        case EASEBurstCount.Six:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L2));
                            break;
                        case EASEBurstCount.Eight:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L1));
                            break;
                    }
                }
                if (ASEBurstInterval != EASEBurstInterval.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3));
                    switch (ASEBurstInterval)
                    {
                        case EASEBurstInterval.No_Change:
                            throw new Exception("Logic error");
                        case EASEBurstInterval.One_Hundred_Milliseconds:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L6));
                            break;
                        case EASEBurstInterval.Two_Hundred_Milliseconds:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L5));
                            break;
                        case EASEBurstInterval.Three_Hundred_Milliseconds:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L4));
                            break;
                        case EASEBurstInterval.Four_Hundred_Milliseconds:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3));
                            break;
                    }
                }
                if (ASESalvoCount != EASESalvoCount.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L4));
                    switch (ASESalvoCount)
                    {
                        case EASESalvoCount.No_Change:
                            throw new Exception("Logic error");
                        case EASESalvoCount.One:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L5));
                            break;
                        case EASESalvoCount.Two:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L4));
                            break;
                        case EASESalvoCount.Four:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3));
                            break;
                        case EASESalvoCount.Eight:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L2));
                            break;
                        case EASESalvoCount.Continuous:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L1));
                            break;
                    }
                }
                if (ASESalvoInterval != EASESalvoInterval.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L5));
                    switch (ASESalvoInterval)
                    {
                        case EASESalvoInterval.No_Change:
                            throw new Exception("Logic error");
                        case EASESalvoInterval.One:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L5));
                            break;
                        case EASESalvoInterval.Two:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L4));
                            break;
                        case EASESalvoInterval.Three:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3));
                            break;
                        case EASESalvoInterval.Four:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L2));
                            break;
                        case EASESalvoInterval.Five:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L1));
                            break;
                        case EASESalvoInterval.Eight:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_T1));
                            break;
                        case EASESalvoInterval.Random:
                            commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L6));
                            break;
                    }
                }
                // Back to ASE main page
                commands.Add(new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_T6));
            }

            // Ownship DL info
            if (!string.IsNullOrEmpty(OwnshipCallsign) || !string.IsNullOrEmpty(OwnshipSubscriberID))
            {
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_COM));
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B4));
                if (!string.IsNullOrEmpty(ownshipCallsign))
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L1));
                    commands.AddRange(AH64.GetCommandsForKUText(OwnshipCallsign + '\n', true, IsPilot));
                }
                if (!string.IsNullOrEmpty(OwnshipSubscriberID))
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R1));
                    commands.AddRange(AH64.GetCommandsForKUText(OwnshipSubscriberID + '\n', true, IsPilot));
                }
            }

            // Presets
            foreach (var kvp in presetDataDictionary)
            {
                commands.AddRange(kvp.Value.GenerateCommands(kvp.Key, IsPilot));
            }

            // WPN
            if (ContainsWeaponData)
            {
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_WPN));
                // Set HMD as sight to force MSL selection to SAL
                commands.Add(new DCSCommand((int)(IsPilot ? AH64.EDeviceCode.PLT_HOTAS : AH64.EDeviceCode.CPG_HOTAS), (int)AH64.EKeyCode.MISSION_SIGHT_SELECT_SW_UP));
                // WAS GUN then MSL twice to de-WAS any potential weapon selection
                commands.Add(new DCSCommand((int)(IsPilot ? AH64.EDeviceCode.PLT_HOTAS : AH64.EDeviceCode.CPG_HOTAS), (int)AH64.EKeyCode.CYCLIC_WEAPONS_ACTION_SW_UP));
                commands.Add(new DCSCommand((int)(IsPilot ? AH64.EDeviceCode.PLT_HOTAS : AH64.EDeviceCode.CPG_HOTAS), (int)AH64.EKeyCode.CYCLIC_WEAPONS_ACTION_SW_RIGHT));
                commands.Add(new DCSCommand((int)(IsPilot ? AH64.EDeviceCode.PLT_HOTAS : AH64.EDeviceCode.CPG_HOTAS), (int)AH64.EKeyCode.CYCLIC_WEAPONS_ACTION_SW_RIGHT));
                // Select GUN then MSL twice to deselect any weapon selections on the MFD (not WAS)
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B2));
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B3));
                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B3));

                if (GunBurstLength != EGunBurstLength.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B2));
                    switch (GunBurstLength)
                    {
                        case EGunBurstLength.No_Change:
                            throw new Exception("Logic Error");
                        case EGunBurstLength.Burst_10:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L1));
                            break;
                        case EGunBurstLength.Burst_20:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L2));
                            break;
                        case EGunBurstLength.Burst_50:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L3));
                            break;
                        case EGunBurstLength.Burst_100:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L4));
                            break;
                        case EGunBurstLength.Burst_All:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L5));
                            break;
                    }
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B2));
                }

                if (RocketQuantity != ERocketQuantity.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B5));
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R1));
                    switch (RocketQuantity)
                    {
                        case ERocketQuantity.No_Change:
                            throw new Exception("Logic Error");
                        case ERocketQuantity.Quantity_1:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R1));
                            break;
                        case ERocketQuantity.Quantity_2:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R2));
                            break;
                        case ERocketQuantity.Quantity_4:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R3));
                            break;
                        case ERocketQuantity.Quantity_8:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R4));
                            break;
                        case ERocketQuantity.Quantity_12:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R5));
                            break;
                        case ERocketQuantity.Quantity_24:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_R6));
                            break;
                        case ERocketQuantity.Quantity_All:
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B6));
                            break;
                    }
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B5));
                }

                if (ManRange.HasValue)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B6));
                    commands.AddRange(AH64.GetCommandsForKUText(ManRange.Value.ToString() + '\n', true, IsPilot));
                }

                if (MissilePriorityChannel != EMissileChannel.No_Change
                    || MissileAlternateChannel != EMissileChannel.No_Change)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B3));

                    if (MissilePriorityChannel != EMissileChannel.No_Change)
                    {
                        commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L1));
                        switch (MissilePriorityChannel)
                        {
                            case EMissileChannel.No_Change:
                                throw new Exception("Logic Error");
                            case EMissileChannel.CH_1:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L1));
                                break;
                            case EMissileChannel.CH_2:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L2));
                                break;
                            case EMissileChannel.CH_3:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L3));
                                break;
                            case EMissileChannel.CH_4:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L4));
                                break;
                            case EMissileChannel.None:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L5));
                                break;
                        }
                    }

                    if (MissileAlternateChannel != EMissileChannel.No_Change
                        && MissilePriorityChannel != EMissileChannel.None)
                    {
                        commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L2));
                        switch (MissileAlternateChannel)
                        {
                            case EMissileChannel.No_Change:
                                throw new Exception("Logic Error");
                            case EMissileChannel.CH_1:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L1));
                                break;
                            case EMissileChannel.CH_2:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L2));
                                break;
                            case EMissileChannel.CH_3:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L3));
                                break;
                            case EMissileChannel.CH_4:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L4));
                                break;
                            case EMissileChannel.None:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_L5));
                                break;
                        }
                    }

                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_B3));
                }

                if (LrfdAndLstLaserChannel != ELaserChannel.No_Change
                    || ContainsLaserCodeFrequencyData)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T4)); // CODE

                    if (LrfdAndLstLaserChannel != ELaserChannel.No_Change)
                    {
                        for (int i = 0; i < 2; i++) // 2x
                        {
                            // switch LRFD / LST
                            commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T2));
                            // Select Channel
                            commands.Add(new DCSCommand(DeviceRMFD, GetLaserChannelButton(LrfdAndLstLaserChannel)));
                        }
                    }

                    if (ContainsLaserCodeFrequencyData)
                    {
                        commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T5)); // FREQ
                        foreach (var kvp in laserCodeFrequencies)
                        {
                            if (kvp.Value is null)
                            {
                                continue;
                            }
                            commands.Add(new DCSCommand(DeviceRMFD, GetLaserChannelButton(kvp.Key)));
                            commands.AddRange(AH64.GetCommandsForKUText(kvp.Value.LaserCode.ToString() + '\n', true, IsPilot));
                        }
                    }

                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T4)); // Deselect CODE
                }
                if (ContainsMissileChannelData)
                {
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T1)); // CHAN
                    foreach (var kvp in missileChannel)
                    {
                        if (kvp.Value == ELaserChannel.No_Change)
                        {
                            continue;
                        }
                        switch (kvp.Key)
                        {
                            case EMissileChannel.No_Change:
                                throw new Exception("Logic error");
                            case EMissileChannel.CH_1:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T2));
                                break;
                            case EMissileChannel.CH_2:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T3));
                                break;
                            case EMissileChannel.CH_3:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T4));
                                break;
                            case EMissileChannel.CH_4:
                                commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T5));
                                break;
                            case EMissileChannel.None:
                                throw new Exception("Logic error");
                        }
                        commands.Add(new DCSCommand(DeviceRMFD, GetLaserChannelButton(kvp.Value)));
                    }
                    commands.Add(new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_T1)); // Deselect CHAN
                }
            }

            // TODO: ADF

            return commands;
        }

        private int DeviceLMFD
        {
            get
            {
                return IsPilot ? (int)AH64.EDeviceCode.PLT_LMFD : (int)AH64.EDeviceCode.CPG_LMFD;
            }
        }

        private int DeviceRMFD
        {
            get
            {
                return IsPilot ? (int)AH64.EDeviceCode.PLT_RMFD : (int)AH64.EDeviceCode.CPG_RMFD;
            }
        }

        /// <summary>
        /// Gets the actions to be used after items have been processed.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPostActions()
        {
            var commands = new List<DCSCommand>()
            {
                new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_WPN),         // Left MFD to WPN
                new DCSCommand(DeviceRMFD, (int)AH64.EKeyCode.MFD_TSD)          // Right MFD to TSD
            };
            return commands;
        }

        /// <summary>
        /// Gets the actions to be added before items are processed.
        /// </summary>
        /// <returns>
        /// The list of actions.
        /// </returns>
        protected override List<DCSCommand> GetPreActions()
        {
            var commands = new List<DCSCommand>()
            {
                new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_B1_M),        // Left MFD to ASE to prevent autopage
                new DCSCommand(DeviceLMFD, (int)AH64.EKeyCode.MFD_L3)
            };
            // Left MFD to ASE to prevent autopage while right MFD is working on comms page
            return commands;
        }
        #endregion

        #region ADF
        #endregion

        #region OwnshipDL        

        private string ownshipSubscriberID = null;
        /// <summary>
        /// Gets or sets the ownship subscriber identifier.
        /// </summary>
        /// <value>
        /// The ownship subscriber identifier.
        /// </value>
        public string OwnshipSubscriberID
        {
            get
            {
                return ownshipSubscriberID;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ownshipSubscriberID = null;
                    return;
                }
                string error = CheckDLSubscriberID(value);
                if (!string.IsNullOrEmpty(error))
                {
                    throw new ArgumentException(error, nameof(OwnshipSubscriberID));
                }
                ownshipSubscriberID = value;
            }
        }

        private string ownshipCallsign = null;
        /// <summary>
        /// Gets or sets the ownship callsign.
        /// </summary>
        /// <value>
        /// The ownship callsign.
        /// </value>
        /// <exception cref="ArgumentException">error, nameof(OwnshipCallsign)</exception>
        public string OwnshipCallsign
        {
            get
            {
                return ownshipCallsign;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ownshipCallsign = null;
                    return;
                }
                string error = CheckDLCallSign(value);
                if (!string.IsNullOrEmpty(error))
                {
                    throw new ArgumentException(error, nameof(OwnshipCallsign));
                }
                ownshipCallsign = value;
            }
        }
        #endregion

        #region ASE        
        /// <summary>
        /// Gets a value indicating whether [contains ase data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains ase data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsASEData
        {
            get
            {
                return ASEAutopage != EASEAutopage.No_Change
                    || ASEBurstCount != EASEBurstCount.No_Change
                    || ASEBurstInterval != EASEBurstInterval.No_Change
                    || ASESalvoCount != EASESalvoCount.No_Change
                    || ASESalvoInterval != EASESalvoInterval.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [contains chaff program data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains chaff program data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsChaffProgramData
        {
            get
            {
                return ASEBurstCount != EASEBurstCount.No_Change
                    || ASEBurstInterval != EASEBurstInterval.No_Change
                    || ASESalvoCount != EASESalvoCount.No_Change
                    || ASESalvoInterval != EASESalvoInterval.No_Change;
            }
        }

        /// <summary>
        /// An ASE Autopage setting
        /// </summary>
        public enum EASEAutopage
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Search,
            Acquisition,
            Track,
            Off
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Gets or sets the ase autopage.
        /// </summary>
        /// <value>
        /// The ase autopage.
        /// </value>
        public EASEAutopage ASEAutopage { get; set; } = EASEAutopage.No_Change;

        /// <summary>
        /// A Burst Count setting
        /// </summary>
        public enum EASEBurstCount
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            One,
            Two,
            Three,
            Four,
            Six,
            Eight
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Gets or sets the ase burst count.
        /// </summary>
        /// <value>
        /// The ase burst count.
        /// </value>
        public EASEBurstCount ASEBurstCount { get; set; } = EASEBurstCount.No_Change;

        /// <summary>
        /// A Burst Interval setting
        /// </summary>
        public enum EASEBurstInterval
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            One_Hundred_Milliseconds,
            Two_Hundred_Milliseconds,
            Three_Hundred_Milliseconds,
            Four_Hundred_Milliseconds
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Gets or sets the ase burst interval.
        /// </summary>
        /// <value>
        /// The ase burst interval.
        /// </value>
        public EASEBurstInterval ASEBurstInterval { get; set; } = EASEBurstInterval.No_Change;

        /// <summary>
        /// A Salvo Count setting
        /// </summary>
        public enum EASESalvoCount
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            One,
            Two,
            Four,
            Eight,
            Continuous
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Gets or sets the ase salvo count.
        /// </summary>
        /// <value>
        /// The ase salvo count.
        /// </value>
        public EASESalvoCount ASESalvoCount { get; set; } = EASESalvoCount.No_Change;

        /// <summary>
        /// A Salvo Interval setting
        /// </summary>
        public enum EASESalvoInterval
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            One,
            Two,
            Three,
            Four,
            Five,
            Eight,
            Random
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Gets or sets the ase salvo interval.
        /// </summary>
        /// <value>
        /// The ase salvo interval.
        /// </value>
        public EASESalvoInterval ASESalvoInterval { get; set; } = EASESalvoInterval.No_Change;
        #endregion

        #region WPN

        /// <summary>
        /// Gets a value indicating whether [contains weapon data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains weapon data]; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool ContainsWeaponData
        {
            get
            {
                return ContainsLaserCodeFrequencyData
                    || ContainsMissileChannelData
                    || MissilePriorityChannel != EMissileChannel.No_Change
                    || MissileAlternateChannel != EMissileChannel.No_Change
                    || LrfdAndLstLaserChannel != ELaserChannel.No_Change
                    || GunBurstLength != EGunBurstLength.No_Change
                    || RocketQuantity != ERocketQuantity.No_Change
                    || ManRange.HasValue;
            }
        }

        /// <summary>
        /// A missile channel
        /// </summary>
        public enum EMissileChannel
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            CH_1,
            CH_2,
            CH_3,
            CH_4,
            None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        [JsonProperty("MissileChannel")]
        private Dictionary<EMissileChannel, ELaserChannel> missileChannel = new Dictionary<EMissileChannel, ELaserChannel>()
        {
            { EMissileChannel.CH_1, ELaserChannel.No_Change },
            { EMissileChannel.CH_2, ELaserChannel.No_Change },
            { EMissileChannel.CH_3, ELaserChannel.No_Change },
            { EMissileChannel.CH_4, ELaserChannel.No_Change }
        };

        /// <summary>
        /// Gets a value indicating whether missile channels have laser channels assigned to them.
        /// </summary>
        /// <value>
        ///   <c>true</c> if missile channels have laser channels assigned to them; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool ContainsMissileChannelData
        {
            get
            {
                return !missileChannel.Values.All(x => x == ELaserChannel.No_Change);
            }
        }

        /// <summary>
        /// Sets the missile channel to the specified laser channel.
        /// </summary>
        /// <param name="missileChannel">The missile channel.</param>
        /// <param name="laserChannel">The laser channel.</param>
        /// <exception cref="System.ArgumentException">Argument must not be None or No Change - missileChannel</exception>
        public void SetMissileChannel(EMissileChannel missileChannel, ELaserChannel laserChannel)
        {
            if (missileChannel == EMissileChannel.No_Change || missileChannel == EMissileChannel.None)
            {
                throw new ArgumentException("Argument must not be None or No Change", nameof(missileChannel));
            }
            this.missileChannel[missileChannel] = laserChannel;
        }

        /// <summary>
        /// Gets the missile channel.
        /// </summary>
        /// <param name="missileChannel">The missile channel.</param>
        /// <returns>The laser code assigned to this missile channel.</returns>
        public ELaserChannel GetMissileChannel(EMissileChannel missileChannel)
        {
            return this.missileChannel[missileChannel];
        }

        /// <summary>
        /// The missile priority channel
        /// </summary>
        public EMissileChannel MissilePriorityChannel = EMissileChannel.No_Change;

        /// <summary>
        /// The missile alternate channel
        /// </summary>
        public EMissileChannel MissileAlternateChannel = EMissileChannel.No_Change;

        /// <summary>
        /// A laser channel
        /// </summary>
        public enum ELaserChannel
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            A, B, C, D, E, F, G, H, J, K, L, M, N, P, Q, R
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets the laser channel button.
        /// </summary>
        /// <param name="laserChannel">The laser channel.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Logic error</exception>
        private int GetLaserChannelButton(ELaserChannel laserChannel)
        {
            switch (laserChannel)
            {
                case ELaserChannel.No_Change:
                    throw new Exception("Logic error");
                case ELaserChannel.A:
                    return (int)AH64.EKeyCode.MFD_L1;
                case ELaserChannel.B:
                    return (int)AH64.EKeyCode.MFD_L2;
                case ELaserChannel.C:
                    return (int)AH64.EKeyCode.MFD_L3;
                case ELaserChannel.D:
                    return (int)AH64.EKeyCode.MFD_L4;
                case ELaserChannel.E:
                    return (int)AH64.EKeyCode.MFD_L5;
                case ELaserChannel.F:
                    return (int)AH64.EKeyCode.MFD_L6;
                case ELaserChannel.G:
                    return (int)AH64.EKeyCode.MFD_B2;
                case ELaserChannel.H:
                    return (int)AH64.EKeyCode.MFD_B3;
                case ELaserChannel.J:
                    return (int)AH64.EKeyCode.MFD_B4;
                case ELaserChannel.K:
                    return (int)AH64.EKeyCode.MFD_B5;
                case ELaserChannel.L:
                    return (int)AH64.EKeyCode.MFD_R6;
                case ELaserChannel.M:
                    return (int)AH64.EKeyCode.MFD_R5;
                case ELaserChannel.N:
                    return (int)AH64.EKeyCode.MFD_R4;
                case ELaserChannel.P:
                    return (int)AH64.EKeyCode.MFD_R3;
                case ELaserChannel.Q:
                    return (int)AH64.EKeyCode.MFD_R2;
                case ELaserChannel.R:
                    return (int)AH64.EKeyCode.MFD_R1;
            }
            throw new Exception("Logic error");
        }

        /// <summary>
        /// Gets or sets the LRFD and LST laser channel.
        /// </summary>
        /// <value>
        /// The LRFD and LST laser channel.
        /// </value>
        public ELaserChannel LrfdAndLstLaserChannel { get; set; } = ELaserChannel.No_Change;

        /// <summary>
        /// Gets a value indicating whether this instance has laser code data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has laser code data; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool ContainsLaserCodeFrequencyData
        {
            get
            {
                return !laserCodeFrequencies.Values.All(x => x is null);
            }
        }

        [JsonProperty("LaserCodes")]
        private Dictionary<ELaserChannel, AH64LaserCode> laserCodeFrequencies = new Dictionary<ELaserChannel, AH64LaserCode>()
        {
            { ELaserChannel.A, null },
            { ELaserChannel.B, null },
            { ELaserChannel.C, null },
            { ELaserChannel.D, null },
            { ELaserChannel.E, null },
            { ELaserChannel.F, null },
            { ELaserChannel.G, null },
            { ELaserChannel.H, null },
            { ELaserChannel.J, null },
            { ELaserChannel.K, null },
            { ELaserChannel.L, null },
            { ELaserChannel.M, null },
            { ELaserChannel.N, null },
            { ELaserChannel.P, null },
            { ELaserChannel.Q, null },
            { ELaserChannel.R, null }
        };

        /// <summary>
        /// Sets the laser code.
        /// </summary>
        /// <param name="laserChannel">The laser channel.</param>
        /// <param name="laserCode">The laser code.</param>
        /// <exception cref="System.ArgumentException">Laser Channel must not be No_Change - laserChannel</exception>
        public void SetLaserCodeFrequency(ELaserChannel laserChannel, AH64LaserCode laserCode)
        {
            if (laserChannel == ELaserChannel.No_Change)
            {
                throw new ArgumentException("Laser Channel must not be " + ELaserChannel.No_Change.ToString(), nameof(laserChannel));
            }
            laserCodeFrequencies[laserChannel] = laserCode;
        }

        /// <summary>
        /// Gets the laser code frequency.
        /// </summary>
        /// <param name="laserChannel">The laser channel.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Laser Channel must not be No_Change - laserChannel</exception>
        public AH64LaserCode GetLaserCodeFrequency(ELaserChannel laserChannel)
        {
            if (laserChannel == ELaserChannel.No_Change)
            {
                throw new ArgumentException("Laser Channel must not be " + ELaserChannel.No_Change.ToString(), nameof(laserChannel));
            }
            return laserCodeFrequencies[laserChannel];
        }

        /// <summary>
        /// Gun burst length setting
        /// </summary>
        public enum EGunBurstLength
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Burst_10,
            Burst_20,
            Burst_50,
            Burst_100,
            Burst_All
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the length of the gun burst.
        /// </summary>
        /// <value>
        /// The length of the gun burst.
        /// </value>
        public EGunBurstLength GunBurstLength { get; set; } = EGunBurstLength.No_Change;

        /// <summary>
        /// Rocket quantity setting
        /// </summary>
        public enum ERocketQuantity
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Quantity_1,
            Quantity_2,
            Quantity_4,
            Quantity_8,
            Quantity_12,
            Quantity_24,
            Quantity_All
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the rocket quantity.
        /// </summary>
        /// <value>
        /// The rocket quantity.
        /// </value>
        public ERocketQuantity RocketQuantity { get; set; } = ERocketQuantity.No_Change;

        private int? manRange = null;

        /// <summary>
        /// Gets or sets the man range.
        /// </summary>
        /// <value>
        /// The man range.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// ManRange - Man RNG must be &gt;= 100
        /// or
        /// ManRange - Man RNG must be &lt; 10000
        /// </exception>
        public int? ManRange
        {
            get
            {
                return manRange;
            }
            set
            {
                if (!value.HasValue)
                {
                    manRange = null;
                    return;
                }
                if (value < 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(ManRange), value, "Man RNG must be >= 100");
                }
                if (value > 9999)
                {
                    throw new ArgumentOutOfRangeException(nameof(ManRange), value, "Man RNG must be < 10000");
                }
                manRange = value.Value;
            }
        }
        #endregion
    }

}
