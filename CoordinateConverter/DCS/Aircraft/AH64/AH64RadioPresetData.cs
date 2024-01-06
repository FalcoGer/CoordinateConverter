using CoordinateConverter.DCS.Tools;
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
            VHF_Single_Channel,
            UHF_Single_Channel,
            UHF_HaveQuick,
            FM1_Single_Channel,
            FM1_SINCGARS,
            FM2_Single_Channel,
            FM2_SINCGARS,
            HF_Single_Channel,
            HF_Preset,
            HF_ALE,
            HF_ECCM,
            None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets a value indicating whether there is data to be entered for this preset.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [needs editing]; otherwise, <c>false</c>.
        /// </value>
        public bool NeedsEditing
        {
            get
            {
                return !string.IsNullOrEmpty(UnitId) ||
                    !string.IsNullOrEmpty(Callsign) ||
                    ContainsVHFData ||
                    ContainsUHFData ||
                    ContainsHFData ||
                    ContainsFM1Data ||
                    ContainsFM2Data ||
                    ContainsRadioPrimaryData ||
                    ContainsNetData;

            }
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
                return unitId ?? string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    unitId = string.Empty;
                    return;
                }
                value = value.ToUpper();
                if (!AH64.GetIsValidTextForKU(value, 3, 8))
                {
                    throw new ArgumentException("Contains invalid characters or too big or too small");
                }
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
                return callsign ?? string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    callsign = string.Empty;
                    return;
                }
                value = value.ToUpper();
                if (!AH64.GetIsValidTextForKU(value, 3, 5))
                {
                    throw new ArgumentException("Contains invalid characters or too big or too small");
                }
                callsign = value;
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether [contains radio primary data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains radio primary data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsRadioPrimaryData { get; set; } = false;
        /// <summary>
        /// The primary radio setting, null for no change.
        /// </summary>
        public EPrimaryRadioSetting PrimaryRadioSetting = EPrimaryRadioSetting.None;

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
        public RadioFrequency VHFFrequency { get; set; } = new RadioFrequency(127.5m, 108.0m, 155.995m, 0.005m);
        /// <summary>
        /// Gets a value indicating whether [VHF frequency is receive only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [VHF frequency is receive only]; otherwise, <c>false</c>.
        /// </value>
        public bool VHFFrequencyIsReceiveOnly
        {
            get
            {
                return VHFFrequency.Frequency < 116.0m;
            }
        }
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
        public RadioFrequency UHFHaveQuickNet { get; set; } = new RadioFrequency(0.0m, 0.0m, 99.975m, 0.005m);
        /// <summary>
        /// Gets or sets the uhf frequency.
        /// </summary>
        /// <value>
        /// The uhf frequency.
        /// </value>
        public RadioFrequency UHFFrequency { get; set; } = new RadioFrequency(225.0m, 225.0m, 399.985m, 0.005m);
        #endregion
        #region FM1        
        /// <summary>
        /// Gets or sets a value indicating whether [contains FM 1 data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains FM 1 data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsFM1Data { get; set; } = false;
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
                if (value < 1 || value > 6) { throw new ArgumentOutOfRangeException("value needs to be >= 1 and <= 6."); }
                fm1Hopset = value;
            }
        }    
        /// <summary>
        /// Gets or sets the FM1 frequency.
        /// </summary>
        /// <value>
        /// The FM1 frequency.
        /// </value>
        public RadioFrequency FM1Frequency { get; set; } = new RadioFrequency(30.0m, 30.0m, 87.995m, 0.005m);
        #endregion
        #region FM2        
        /// <summary>
        /// Gets or sets a value indicating whether [contains FM 2 data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains FM 2 data]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsFM2Data { get; set; } = false;
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
                if (value < 1 || value > 6) { throw new ArgumentOutOfRangeException("value needs to be >= 1 and <= 6."); }
                fm2Hopset = value;
            }
        }       
        /// <summary>
        /// Gets or sets the FM2 frequency.
        /// </summary>
        /// <value>
        /// The FM2 frequency.
        /// </value>
        public RadioFrequency FM2Frequency { get; set; } = new RadioFrequency(30.0m, 30.0m, 87.995m, 0.005m);
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
        
        private int hfPresetChannel = 1;
        /// <summary>
        /// Gets or sets the hf preset channel.
        /// </summary>
        /// <value>
        /// The hf preset channel.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Valid range 1..20</exception>
        public int HFPresetChannel
        {
            get
            {
                return hfPresetChannel;
            }
            set
            {
                if (value < 1 || value > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(HFPresetChannel) + " must be in range 1..20, but was " + value);
                }
                hfPresetChannel = value;
            }
        }
        
        private int hfAleNet = 1;
        /// <summary>
        /// Gets or sets the HF ALE net.
        /// </summary>
        /// <value>
        /// The HF ALE net.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Valid range 1..20</exception>
        public int HFALENet
        {
            get
            {
                return hfAleNet;
            }
            set
            {
                if (value < 1 || value > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(hfAleNet) + " must be in range 1..20, but was " + value);
                }
                hfAleNet = value;
            }
        }
        private int hfEccmNet = 1;
        /// <summary>
        /// Gets or sets the HF ECCM net.
        /// </summary>
        /// <value>
        /// The HF ECCM net.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Valid range 1..12</exception>
        public int HFECCMNet
        {
            get
            {
                return hfEccmNet;
            }
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentOutOfRangeException(nameof(HFECCMNet) + " must be in range 1..12, but was " + value);
                }
                hfEccmNet = value;
            }
        }

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

        private List<AH64DataLinkMember> linkMembers = new List<AH64DataLinkMember>();
        /// <summary>
        /// Gets the link member in the slot slot.
        /// </summary>
        /// <param name="slotID">The slot identifier.</param>
        /// <returns>The link member in the appropriate slot</returns>
        public AH64DataLinkMember GetLinkMemberSlot(uint slotID)
        {
            if (slotID >= linkMembers.Count)
            {
                return null;
            }
            return linkMembers[(int)slotID];
        }

        /// <summary>
        /// Gets the link member with subscriber identifier.
        /// </summary>
        /// <param name="subscriberID">The subscriber identifier.</param>
        /// <returns>The link member with that SubscriberID</returns>
        public AH64DataLinkMember GetLinkMemberWithSubscriberID(string subscriberID)
        {
            return linkMembers.Find(x => x.SubscriberID == subscriberID);
        }

        /// <summary>
        /// Sets the or adds a new link member.
        /// </summary>
        /// <param name="subscriberID">The subscriber identifier.</param>
        /// <param name="callsign">The callsign.</param>
        /// <param name="primary">if set to <c>true</c> [primary].</param>
        /// <param name="team">if set to <c>true</c> [team].</param>
        /// <returns>An error message or null</returns>
        public string SetOrAddLinkMember(string subscriberID, string callsign, bool primary, bool team)
        {
            const int MAX_TEAM_MEMBERS = 7;
            const int MAX_MEMBERS = 15;
            
            int idx = linkMembers.FindIndex(x => x.SubscriberID == subscriberID);
            if (idx == -1)
            {
                // Not found
                if (linkMembers.Count >= MAX_MEMBERS)
                {
                    return "Maximum member capacity (" + MAX_MEMBERS.ToString() + ").";
                }
                if (team && linkMembers.FindAll(x => x.Team).Count >= MAX_TEAM_MEMBERS)
                {
                    return "Maximum team member limit (" + MAX_TEAM_MEMBERS.ToString() + ").";
                }
                linkMembers.Add(new AH64DataLinkMember(callsign, subscriberID, primary, team));
                return null;
            }
            // was found
            linkMembers[idx] = new AH64DataLinkMember(callsign, subscriberID, primary, team);
            return null;
        }

        /// <summary>
        /// Removes the link member.
        /// </summary>
        /// <param name="subscriberID">The subscriber identifier.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">subscriberID not found</exception>
        public void RemoveLinkMember(string subscriberID)
        {
            int idx = linkMembers.FindIndex(x => x.SubscriberID == subscriberID);
            if (idx == -1)
            {
                throw new ArgumentOutOfRangeException(nameof(subscriberID));
            }
            linkMembers.RemoveAt(idx);
        }
        #endregion
        #region Modem        
        /// <summary>
        /// The possible protocols for datalink.
        /// </summary>
        public enum ELinkProtocol
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            Longbow,
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
            Zero = 0,
            One = 1,
            Two = 2
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        /// <summary>
        /// The baudrate for data transmission.
        /// </summary>
        public enum EBaudRate
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            LBaud16000,
            LBaud8000,
            LBaud2400,
            LBaud1200,
            LBaud600,
            LBaud300,
            LBaud150,
            LBaud75,
            /*
            TBaudAir_1200,
            TBaudAir_600,
            TBaudAir_300,
            TBaudAir_75,
            TBaudTacfire_1200,
            TBaudTacfire_600,
            FBaudSINC_16000,
            FBaudSINC_4800,
            FBaudSINC_2400,
            FBaudSINC_1200,
            FBaudSINC_600,
            FBaudEnhanced_9600,
            FBaudEnhanced_4800,
            FBaudEnhanced_2400,
            FBaudEnhanced_1200
            */
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
        public ELinkProtocol LinkProtocol { get; set; } = ELinkProtocol.Longbow;
        /// <summary>
        /// Gets or sets the number of retries for sending.
        /// </summary>
        /// <value>
        /// The retries.
        /// </value>
        public ERetries Retries { get; set; } = ERetries.Two;
        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        /// <value>
        /// The baud rate.
        /// </value>
        public EBaudRate BaudRate { get; set; } = EBaudRate.LBaud16000;
        #endregion
    }
}
