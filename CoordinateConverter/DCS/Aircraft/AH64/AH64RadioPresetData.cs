using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents the Data in an AH64 Radio Preset for <seealso cref="AH64DTCData"/>.
    /// </summary>
    public class AH64RadioPresetData
    {
        #region Common
        /// <summary>
        /// The valid settings for the primary radio selection.
        /// </summary>
        public enum EPrimaryRadioSetting
        {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            VHF_SingleChannel,
            UHF_SingleChannel,
            UHF_HaveQuick,
            FM1_SingleChannel,
            FM1_SINCGARS,
            FM2_SingleChannel,
            FM2_SINCGARS,
            HF_SingleChannel,
            HF_Preset,
            HF_ALE,
            HF_ECCM,
            None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        private string unitId = String.Empty;
        #endregion
        #region GeneralPresetSettings
        /// <summary>
        /// Gets or sets the unit identifier.
        /// </summary>
        /// <value>
        /// The unit identifier.
        /// </value>
        /// <exception cref="System.ArgumentException">Contains invalid characters or too big or too small</exception>
        public string UnitId
        {
            get
            {
                return unitId;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    unitId = value;
                    return;
                }
                value = value.ToUpper();
                List<char> validChars = new List<char>();
                for (char ch = '0'; ch <= '9'; ch++) { validChars.Add(ch); }
                for (char ch = 'A'; ch <= 'Z'; ch++) { validChars.Add(ch); }
                validChars.Add('-');
                validChars.Add(' ');
                if (!AH64.GetIsValidTextForKU(value, 3, 8, validChars)) { throw new ArgumentException("Contains invalid characters or too big or too small"); }
                unitId = value;
            }
        }

        private string callsign = string.Empty;
        /// <summary>
        /// Gets or sets the callsign.
        /// </summary>
        /// <value>
        /// The callsign.
        /// </value>
        /// <exception cref="System.ArgumentException">Contains invalid characters or too big or too small</exception>
        public string Callsign
        {
            get
            {
                return callsign;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    callsign = value;
                    return;
                }
                value = value.ToUpper();
                List<char> validChars = new List<char>();
                for (char ch = '0'; ch <= '9'; ch++) { validChars.Add(ch); }
                for (char ch = 'A'; ch <= 'Z'; ch++) { validChars.Add(ch); }
                validChars.Add('-');
                validChars.Add(' ');
                if (!AH64.GetIsValidTextForKU(value, 3, 5, validChars)) { throw new ArgumentException("Contains invalid characters or too big or too small"); }
                callsign = value;
            }
        }
        #endregion
        /// <summary>
        /// The primary radio setting, null for no change.
        /// </summary>
        public EPrimaryRadioSetting? PrimaryRadioSetting = null;

        #region VHF        
        /// <summary>
        /// Gets or sets a value indicating whether [contains VHF data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains VHF data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsVHFData { get; set; } = false;
        /// <summary>
        /// Gets or sets the VHF frequency.
        /// </summary>
        /// <value>
        /// The VHF frequency.
        /// </value>
        public RadioFrequency VHFFrequency { get; set; } = new RadioFrequency(127.5m, 108.0m, 151.975m, 0.025m);
        #endregion
        #region UHF        
        /// <summary>
        /// Gets or sets a value indicating whether [contains uhf data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains uhf data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsUHFData { get; set; } = false;
        /// <summary>
        /// Gets or sets the uhf cnv.
        /// </summary>
        /// <value>
        /// The uhf cnv.
        /// </value>
        public AH64RadioCNVSetting UHFCNV { get; set; } = new AH64RadioCNVSetting(1);
        /// <summary>
        /// Gets or sets the uhf havequick net.
        /// </summary>
        /// <value>
        /// The uhf havequick net.
        /// </value>
        public decimal UHFHaveQuickNet { get; set; } = decimal.Zero;
        /// <summary>
        /// Gets or sets the uhf frequency.
        /// </summary>
        /// <value>
        /// The uhf frequency.
        /// </value>
        public RadioFrequency UHFFrequency { get; set; } = new RadioFrequency(225.0m, 225.0m, 399.975m, 0.025m);
        #endregion
        #region FM1        
        /// <summary>
        /// Gets or sets a value indicating whether [contains fm1 data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains fm1 data]; otherwise, <c>false</c>.
        /// </value>
        bool ContainsFM1Data { get; set; } = false;
        /// <summary>
        /// Gets or sets the FM1 CNV.
        /// </summary>
        /// <value>
        /// The FM1 CNV.
        /// </value>
        public AH64RadioCNVSetting FM1CNV { get; set; } = new AH64RadioCNVSetting(1);
        private int fm1Hopset = 1;
        /// <summary>
        /// Gets or sets the FM1 hopset.
        /// </summary>
        /// <value>
        /// The FM1 hopset.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value needs to be >= 0 and ???.</exception>
        public int FM1Hopset
        {
            get
            {
                return fm1Hopset;
            }
            set
            {
                // TODO: Check for valid hopset values
                if (value < 0) { throw new ArgumentOutOfRangeException("value needs to be >= 0 and ???."); }
                fm1Hopset = value;
            }
        }
        // TODO: Fix Step size when ED fixes radio.        
        /// <summary>
        /// Gets or sets the FM1 frequency.
        /// </summary>
        /// <value>
        /// The FM1 frequency.
        /// </value>
        public RadioFrequency FM1Frequency { get; set; } = new RadioFrequency(30.0m, 30.0m, 87.975m, 0.005m);
        #endregion
        #region FM2
        bool ContainsFM2Data { get; set; } = false;
        /// <summary>
        /// Gets or sets the FM2 CNV.
        /// </summary>
        /// <value>
        /// The FM2 CNV.
        /// </value>
        public AH64RadioCNVSetting FM2CNV { get; set; } = new AH64RadioCNVSetting(1);
        private int fm2Hopset = 1;
        /// <summary>
        /// Gets or sets the FM2 hopset.
        /// </summary>
        /// <value>
        /// The FM2 hopset.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value needs to be >= 0 and ???.</exception>
        public int FM2Hopset
        {
            get
            {
                return fm2Hopset;
            }
            set
            {
                // TODO: Check for valid hopset values
                if (value < 0) { throw new ArgumentOutOfRangeException("value needs to be >= 0 and ???."); }
                fm2Hopset = value;
            }
        }
        // TODO: Fix Step size when ED fixes radio.        
        /// <summary>
        /// Gets or sets the FM2 frequency.
        /// </summary>
        /// <value>
        /// The FM2 frequency.
        /// </value>
        public RadioFrequency FM2Frequency { get; set; } = new RadioFrequency(30.0m, 30.0m, 87.975m, 0.005m);
        #endregion
        #region HF        
        /// <summary>
        /// Gets or sets a value indicating whether [contains hf data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains hf data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsHFData { get; set; } = false;
        /// <summary>
        /// Gets or sets the HF CNV.
        /// </summary>
        /// <value>
        /// The HF CNV.
        /// </value>
        public AH64RadioCNVSetting HFCNV { get; set; } = new AH64RadioCNVSetting(1);
        int hfPresetChannel = 1;
        int hfAleNet = 1;
        int hfEccmNet = 1;
        private RadioFrequency hfRxFrequency = new RadioFrequency(2.0m, 2.0m, 29.9999m, 0.0001m);
        /// <summary>
        /// Gets or sets the HFRX frequency. Also Sets HFTX frequency when link is enabled.
        /// </summary>
        /// <value>
        /// The HFRX frequency.
        /// </value>
        public RadioFrequency HFRXFrequency
        {
            get
            {
                return hfRxFrequency;
            }
            set
            {
                hfRxFrequency = value;
                if (TxRxLinked)
                {
                    hfTxFrequency = value;
                }
            }
        }
        private RadioFrequency hfTxFrequency = new RadioFrequency(2.0m, 2.0m, 29.9999m, 0.0001m);
        /// <summary>
        /// Gets or sets the HFTX frequency.
        /// </summary>
        /// <value>
        /// The HFTX frequency.
        /// </value>
        /// <exception cref="System.Exception">Can't set HFTXFrequency when linked.</exception>
        public RadioFrequency HFTXFrequency
        {
            get
            {
                return TxRxLinked ? HFRXFrequency : hfTxFrequency;
            }
            set
            {
                if (TxRxLinked) { throw new Exception("Can't set HFTXFrequency when linked."); }
                hfTxFrequency = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [tx and rx are linked].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tx and rx are linked]; otherwise, <c>false</c>.
        /// </value>
        public bool TxRxLinked { get; set; } = true;
        #endregion
        #region Net        
        /// <summary>
        /// Gets or sets a value indicating whether [contains net data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains net data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsNetData { get; set; } = false;
        /// <summary>
        /// Gets or sets the link members.
        /// </summary>
        /// <value>
        /// The link members.
        /// </value>
        public List<AH64DataLinkMember> LinkMembers { get; set; } = new List<AH64DataLinkMember>();
        #endregion
        #region Modem        
        /// <summary>
        /// The possible protocols for datalink.
        /// </summary>
        public enum ELinkProtocol
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Datalink,
            Tacfire,
            Internet,
            FireSupport,
            None
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// How many times data is tried to be resent if no acknowledge was received.
        /// </summary>
        public enum ERetries
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Zero,
            One,
            Two
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// The baudrate for data transmission.
        /// </summary>
        public enum EBaudRate
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Baud16000
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// Whether this preset info contains modem data
        /// </summary>
        public bool ContainsModemData = false;
        /// <summary>
        /// Gets or sets the link protocol.
        /// </summary>
        /// <value>
        /// The link protocol.
        /// </value>
        public ELinkProtocol LinkProtocol { get; set; } = ELinkProtocol.Datalink;
        /// <summary>
        /// Gets or sets the number of retries for sending.
        /// </summary>
        /// <value>
        /// The retries.
        /// </value>
        public ERetries Retries { get; set; }
        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        /// <value>
        /// The baud rate.
        /// </value>
        public EBaudRate BaudRate { get; set; }
        #endregion
    }
}
