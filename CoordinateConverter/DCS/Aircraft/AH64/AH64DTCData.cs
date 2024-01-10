using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            // Set Primary radio for presets 1 through 5 (rest defaults to none)
            presetDataDictionary[EPreset.Preset1].PrimaryRadioSetting = AH64RadioPresetData.EPrimaryRadioSetting.VHF_Single_Channel;
            presetDataDictionary[EPreset.Preset2].PrimaryRadioSetting = AH64RadioPresetData.EPrimaryRadioSetting.UHF_Single_Channel;
            presetDataDictionary[EPreset.Preset3].PrimaryRadioSetting = AH64RadioPresetData.EPrimaryRadioSetting.FM1_Single_Channel;
            presetDataDictionary[EPreset.Preset4].PrimaryRadioSetting = AH64RadioPresetData.EPrimaryRadioSetting.FM2_Single_Channel;
            presetDataDictionary[EPreset.Preset5].PrimaryRadioSetting = AH64RadioPresetData.EPrimaryRadioSetting.HF_Single_Channel;
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
                    || !string.IsNullOrEmpty(modeSFlightIDValue)
                    || modeSFlightAddressValue.HasValue
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
            throw new NotImplementedException();
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
    }

}
