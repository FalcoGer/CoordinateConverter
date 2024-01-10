using CoordinateConverter.DCS.Aircraft.AH64;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoordinateConverter.DCS.Tools
{
    /// <summary>
    /// Form to enter DTC Data for AH64
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormAH64DTC : Form
    {
        private AH64DTCData data = null;
        private readonly ToolTip toolTip = new ToolTip()
        {
            IsBalloon = false,
            InitialDelay = 0,
            ToolTipIcon = ToolTipIcon.Error,
            UseFading = false,
            UseAnimation = false,
            ShowAlways = true
        };

        /// <summary>
        /// Gets a value indicating whether this instance is pilot.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pilot; otherwise, <c>false</c>.
        /// </value>
        public bool IsPilot { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAH64DTC"/> class.
        /// </summary>
        public FormAH64DTC(bool isPilot)
        {
            IsPilot = isPilot;
            InitializeComponent();

            data = new AH64DTCData(isPilot);

            {
                // Adjust Frequency NumericUpDowns
                var preset = data.GetAH64RadioPreset(AH64DTCData.EPreset.Preset1);
                nudFM1Freq.Minimum = preset.FM1FrequencyMinimum;
                nudFM1Freq.Maximum = preset.FM1FrequencyMaximum;
                nudFM1Freq.Increment = preset.FM1FrequencyIncrement;
                nudFM2Freq.Minimum = preset.FM2FrequencyMinimum;
                nudFM2Freq.Maximum = preset.FM2FrequencyMaximum;
                nudFM2Freq.Increment = preset.FM2FrequencyIncrement;
                nudHFRXFreq.Minimum = preset.HFRXFrequencyMinimum;
                nudHFRXFreq.Maximum = preset.HFRXFrequencyMaximum;
                nudHFRXFreq.Increment = preset.HFRXFrequencyIncrement;
                nudHFTXFreq.Minimum = preset.HFTXFrequencyMinimum;
                nudHFTXFreq.Maximum = preset.HFTXFrequencyMaximum;
                nudHFTXFreq.Increment = preset.HFTXFrequencyIncrement;
                nudUHFFreq.Minimum = preset.UHFFrequencyMinimum;
                nudUHFFreq.Maximum = preset.UHFFrequencyMaximum;
                nudUHFFreq.Increment = preset.UHFFrequencyIncrement;
                nudUHFHQ.Minimum = preset.UHFHaveQuickNetMinimum;
                nudUHFHQ.Maximum = preset.UHFHaveQuickNetMaximum;
                nudUHFHQ.Increment = preset.UHFHaveQuickNetIncrement;
                nudVHFFreq.Minimum = preset.VHFFrequencyMinimum;
                nudVHFFreq.Maximum = preset.VHFFrequencyMaximum;
                nudVHFFreq.Increment = preset.VHFFrequencyIncrement;
            }

            // Set up ComboBoxes
            ddlPresetPrimaryRadio.ValueMember = "Value";
            ddlPresetPrimaryRadio.DisplayMember = "Text";
            // List of radio settings that DCS doesn't implement yet.
            List<AH64RadioPresetData.EPrimaryRadioSetting> notImplemented = new List<AH64RadioPresetData.EPrimaryRadioSetting>()
            {
                AH64RadioPresetData.EPrimaryRadioSetting.FM1_SINCGARS,
                AH64RadioPresetData.EPrimaryRadioSetting.FM2_SINCGARS,
                AH64RadioPresetData.EPrimaryRadioSetting.HF_ALE,
                AH64RadioPresetData.EPrimaryRadioSetting.HF_ECCM,
                AH64RadioPresetData.EPrimaryRadioSetting.HF_Preset,
                AH64RadioPresetData.EPrimaryRadioSetting.HF_Single_Channel,
                AH64RadioPresetData.EPrimaryRadioSetting.UHF_HaveQuick
            };
            foreach (AH64RadioPresetData.EPrimaryRadioSetting primary in Enum.GetValues(typeof(AH64RadioPresetData.EPrimaryRadioSetting)))
            {
                if (notImplemented.Contains(primary))
                {
                    continue;
                }
                var item = new ComboItem<AH64RadioPresetData.EPrimaryRadioSetting>(primary.ToString().Replace('_', ' '), primary);
                ddlPresetPrimaryRadio.Items.Add(item);
            }

            ddlRadioPresetSelection.ValueMember = "Value";
            ddlRadioPresetSelection.DisplayMember = "Text";
            foreach (AH64DTCData.EPreset preset in Enum.GetValues(typeof(AH64DTCData.EPreset)))
            {
                string text = GetPresetName(preset);
                ComboItem<AH64DTCData.EPreset> item = new ComboItem<AH64DTCData.EPreset>(text, preset);
                ddlRadioPresetSelection.Items.Add(item);
            }

            ddlUHFCNV.ValueMember = "Value";
            ddlUHFCNV.DisplayMember = "Text";
            ddlFM1CNV.ValueMember = "Value";
            ddlFM1CNV.DisplayMember = "Text";
            ddlFM2CNV.ValueMember = "Value";
            ddlFM2CNV.DisplayMember = "Text";
            ddlHFCNV.ValueMember = "Value";
            ddlHFCNV.DisplayMember = "Text";
            for (int cnvValue = 1; cnvValue <= 6; cnvValue++)
            {
                AH64RadioCNVSetting cnv = new AH64RadioCNVSetting(cnvValue);
                ddlUHFCNV.Items.Add(new ComboItem<AH64RadioCNVSetting>("CNV " + cnvValue.ToString(), cnv));
                ddlFM1CNV.Items.Add(new ComboItem<AH64RadioCNVSetting>("CNV " + cnvValue.ToString(), cnv));
                ddlFM2CNV.Items.Add(new ComboItem<AH64RadioCNVSetting>("CNV " + cnvValue.ToString(), cnv));
                ddlHFCNV.Items.Add(new ComboItem<AH64RadioCNVSetting>("CNV " + cnvValue.ToString(), cnv));
            }

            ddlRadioPresetModemProtocol.ValueMember = "Value";
            ddlRadioPresetModemProtocol.DisplayMember = "Text";
            // Entries added later in preset changed callback

            ddlPresetModemRetriesCount.ValueMember = "Value";
            ddlPresetModemRetriesCount.DisplayMember = "Text";
            foreach (AH64RadioPresetData.ERetries retryCount in Enum.GetValues(typeof(AH64RadioPresetData.ERetries)))
            {
                string text = Enum.GetName(typeof(AH64RadioPresetData.ERetries), retryCount);
                ComboItem<AH64RadioPresetData.ERetries> item = new ComboItem<AH64RadioPresetData.ERetries>(text, retryCount);
                ddlPresetModemRetriesCount.Items.Add(item);
            }

            ddlPresetModemBaudRate.ValueMember = "Value";
            ddlPresetModemBaudRate.DisplayMember = "Text";
            foreach (AH64RadioPresetData.EBaudRate baudRate in Enum.GetValues(typeof(AH64RadioPresetData.EBaudRate)))
            {
                string text = Enum.GetName(typeof(AH64RadioPresetData.EBaudRate), baudRate).Remove(0, "LBaud".Length);
                ComboItem<AH64RadioPresetData.EBaudRate> item = new ComboItem<AH64RadioPresetData.EBaudRate>(text, baudRate);
                ddlPresetModemBaudRate.Items.Add(item);
            }

            ddlXPNDRMode4Key.ValueMember = "Value";
            ddlXPNDRMode4Key.DisplayMember = "Text";
            foreach (AH64DTCData.EMode4Options mode4Options in Enum.GetValues(typeof(AH64DTCData.EMode4Options)))
            {
                ddlXPNDRMode4Key.Items.Add(new ComboItem<AH64DTCData.EMode4Options>(mode4Options.ToString().Replace('_', ' '), mode4Options));
            }

            ddlXPNDRReply.ValueMember = "Value";
            ddlXPNDRReply.DisplayMember = "Text";
            foreach (AH64DTCData.EIFFReply replyOption in Enum.GetValues(typeof(AH64DTCData.EIFFReply)))
            {
                ddlXPNDRReply.Items.Add(new ComboItem<AH64DTCData.EIFFReply>(replyOption.ToString().Replace('_', ' '), replyOption));
            }

            ddlASEAutopage.ValueMember = "Value";
            ddlASEAutopage.DisplayMember = "Text";
            foreach (AH64DTCData.EASEAutopage option in Enum.GetValues(typeof(AH64DTCData.EASEAutopage)))
            {
                ddlASEAutopage.Items.Add(new ComboItem<AH64DTCData.EASEAutopage>(option.ToString().Replace('_', ' '), option));
            }

            ddlASEBurstCount.ValueMember = "Value";
            ddlASEBurstCount.DisplayMember = "Text";
            foreach (AH64DTCData.EASEBurstCount option in Enum.GetValues(typeof(AH64DTCData.EASEBurstCount)))
            {
                ddlASEBurstCount.Items.Add(new ComboItem<AH64DTCData.EASEBurstCount>(option.ToString().Replace('_', ' '), option));
            }

            ddlASEBurstInterval.ValueMember = "Value";
            ddlASEBurstInterval.DisplayMember = "Text";
            foreach (AH64DTCData.EASEBurstInterval option in Enum.GetValues(typeof(AH64DTCData.EASEBurstInterval)))
            {
                ddlASEBurstInterval.Items.Add(new ComboItem<AH64DTCData.EASEBurstInterval>(option.ToString().Replace('_', ' '), option));
            }

            ddlASESalvoCount.ValueMember = "Value";
            ddlASESalvoCount.DisplayMember = "Text";
            foreach (AH64DTCData.EASESalvoCount option in Enum.GetValues(typeof(AH64DTCData.EASESalvoCount)))
            {
                ddlASESalvoCount.Items.Add(new ComboItem<AH64DTCData.EASESalvoCount>(option.ToString().Replace('_', ' '), option));
            }

            ddlASESalvoInterval.ValueMember = "Value";
            ddlASESalvoInterval.DisplayMember = "Text";
            foreach (AH64DTCData.EASESalvoInterval option in Enum.GetValues(typeof(AH64DTCData.EASESalvoInterval)))
            {
                ddlASESalvoInterval.Items.Add(new ComboItem<AH64DTCData.EASESalvoInterval>(option.ToString().Replace('_', ' '), option));
            }

            // Call reset
            btnReset_Click(null, null);
        }

        private AH64DTCData.EPreset GetSelectedPresetIdent()
        {
            return ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlRadioPresetSelection);
        }

        private AH64RadioPresetData GetSelectedPreset()
        {
            AH64DTCData.EPreset selectedPresetIdent = GetSelectedPresetIdent();
            AH64RadioPresetData selectedPreset = data.GetAH64RadioPreset(selectedPresetIdent);
            return selectedPreset;
        }

        private void btnReset_Click(object objSender, EventArgs e)
        {
            // Reset DTC
            data = new AH64DTCData(IsPilot);

            // Refresh Controls
            RefreshControls();
        }

        private void RefreshControls()
        {
            // Reset Radio Presets
            ddlRadioPresetSelection.SelectedIndex = 0;

            foreach (AH64DTCData.EPreset preset in Enum.GetValues(typeof(AH64DTCData.EPreset)))
            {
                UpdatePresetNameInDDL(preset);
            }

            // Reset preset config to values in data via the callbacks
            ddlRadioPresetSelection_SelectedIndexChanged(ddlRadioPresetSelection, null);
            cbPresetNet_Enable_CheckedChanged(cbPresetNet_Enable, null);
            cbPresetModem_Enable_CheckedChanged(cbPresetModem_Enable, null);
            cbPresetVHF_Enable_CheckedChanged(cbPresetVHF_Enable, null);
            cbPresetUHF_Enable_CheckedChanged(cbPresetUHF_Enable, null);
            cbPresetFM1_Enable_CheckedChanged(cbPresetFM1_Enable, null);
            cbPresetFM2_Enable_CheckedChanged(cbPresetFM2_Enable, null);
            cbPresetHF_Enable_CheckedChanged(cbPresetHF_Enable, null);
            cbPresetVHFAllowRecvOnlyRange_CheckedChanged(cbPresetVHFAllowRecvOnlyRange, null);

            // Reset DL Entry as that is not part of the preset
            tbPresetNetCS.Text = string.Empty;
            tbPresetNetID.Text = string.Empty;
            cbPresetNetPrimary.Checked = false;
            cbPresetNetTeam.Checked = false;

            // Reset Ownship DL to values in data
            tbOwnshipCallsign.Text = data.OwnshipCallsign;
            tbOwnshipSubscriberID.Text = data.OwnshipSubscriberID;
            // Reset IFF/XPNDR to values in data
            tbXPNDRMode1.Text = data.Mode1;
            tbXPNDRMode2.Text = data.Mode2;
            tbXPNDRMode3A.Text = data.Mode3A;
            tbXPNDRModeSAddr.Text = data.ModeSFlightAddress;
            tbXPNDRModeSID.Text = data.ModeSFlightID;
            ddlXPNDRMode4Key.SelectedIndex = ComboItem<AH64DTCData.EMode4Options>.FindValue(ddlXPNDRMode4Key, data.Mode4) ?? 0;
            ddlXPNDRReply.SelectedIndex = ComboItem<AH64DTCData.EIFFReply>.FindValue(ddlXPNDRReply, data.IFFReply) ?? 0;
            // Reset ASE to values in data
            ddlASEAutopage.SelectedIndex = ComboItem<AH64DTCData.EASEAutopage>.FindValue(ddlASEAutopage, data.ASEAutopage) ?? 0;
            ddlASEBurstCount.SelectedIndex = ComboItem<AH64DTCData.EASEBurstCount>.FindValue(ddlASEBurstCount, data.ASEBurstCount) ?? 0;
            ddlASEBurstInterval.SelectedIndex = ComboItem<AH64DTCData.EASEBurstInterval>.FindValue(ddlASEBurstInterval, data.ASEBurstInterval) ?? 0;
            ddlASESalvoCount.SelectedIndex = ComboItem<AH64DTCData.EASESalvoCount>.FindValue(ddlASESalvoCount, data.ASESalvoCount) ?? 0;
            ddlASESalvoInterval.SelectedIndex = ComboItem<AH64DTCData.EASESalvoInterval>.FindValue(ddlASESalvoInterval, data.ASESalvoInterval) ?? 0;
            // TODO: Reset ADF to values in data
        }

        #region PresetGeneral
        private void ddlRadioPresetSelection_SelectedIndexChanged(object objSender, EventArgs e)
        {
            ComboBox sender = objSender as ComboBox;
            AH64DTCData.EPreset selectedPresetIdent = GetSelectedPresetIdent();
            AH64RadioPresetData selectedPreset = data.GetAH64RadioPreset(selectedPresetIdent);
            // Update all the information
            // Primary Radio
            ddlPresetPrimaryRadio.SelectedIndex = ComboItem<AH64RadioPresetData.EPrimaryRadioSetting>.FindValue(ddlPresetPrimaryRadio, selectedPreset.PrimaryRadioSetting) ?? 0;
            // Preset UID & CS
            tbRadioPresetUnitID.Text = selectedPreset.UnitId;
            tbRadioPresetCallsign.Text = selectedPreset.Callsign;
            // VHF
            cbPresetVHF_Enable.Checked = selectedPreset.ContainsVHFData;
            cbPresetVHFAllowRecvOnlyRange.Checked = selectedPreset.VHFFrequencyIsReceiveOnly;
            nudVHFFreq.Value = selectedPreset.VHFFrequency;
            // UHF
            cbPresetUHF_Enable.Checked = selectedPreset.ContainsUHFData;
            nudUHFHQ.Value = selectedPreset.UHFHaveQuickNet;
            nudUHFFreq.Value = selectedPreset.UHFFrequency;
            ddlUHFCNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlUHFCNV, selectedPreset.UHFCNV) ?? 0;
            // FM 1
            cbPresetFM1_Enable.Checked = selectedPreset.ContainsFM1Data;
            ddlFM1CNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlFM1CNV, selectedPreset.FM1CNV) ?? 0;
            nudFM1Hopset.Value = selectedPreset.FM1Hopset;
            nudFM1Freq.Value = selectedPreset.FM1Frequency;
            // FM 2
            cbPresetFM2_Enable.Checked = selectedPreset.ContainsFM2Data;
            ddlFM2CNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlFM2CNV, selectedPreset.FM2CNV) ?? 0;
            nudFM2Hopset.Value = selectedPreset.FM2Hopset;
            nudFM2Freq.Value = selectedPreset.FM2Frequency;
            // HF
            cbPresetHF_Enable.Checked = selectedPreset.ContainsHFData;
            ddlHFCNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlHFCNV, selectedPreset.HFCNV) ?? 0;
            nudHFPresetCH.Value = selectedPreset.HFPresetChannel;
            nudHFALENet.Value = selectedPreset.HFALENet;
            nudHFECCMNet.Value = selectedPreset.HFECCMNet;
            cbHFSame.Checked = selectedPreset.TxRxLinked;
            nudHFRXFreq.Value = selectedPreset.HFRXFrequency;
            nudHFTXFreq.Value = selectedPreset.HFTXFrequency;
            // Net
            cbPresetNet_Enable.Checked = selectedPreset.ContainsNetData;
            cbPresetModem_Enable.Checked = selectedPreset.ContainsModemData;
            RepopulateNetMembers();
            // Modem
            ddlRadioPresetModemProtocol.Items.Clear();
            foreach (AH64RadioPresetData.ELinkProtocol linkProtocol in Enum.GetValues(typeof(AH64RadioPresetData.ELinkProtocol)))
            {
                if (selectedPresetIdent <= AH64DTCData.EPreset.Preset8
                    || linkProtocol == AH64RadioPresetData.ELinkProtocol.Internet
                    || linkProtocol == AH64RadioPresetData.ELinkProtocol.FireSupport
                    || linkProtocol == AH64RadioPresetData.ELinkProtocol.None)
                {
                    ddlRadioPresetModemProtocol.Items.Add(new ComboItem<AH64RadioPresetData.ELinkProtocol>(linkProtocol.ToString(), linkProtocol));
                }
            }
            ddlRadioPresetModemProtocol.SelectedIndex = ComboItem<AH64RadioPresetData.ELinkProtocol>.FindValue(ddlRadioPresetModemProtocol, selectedPreset.LinkProtocol)
                ?? ComboItem<AH64RadioPresetData.ELinkProtocol>.FindValue(ddlRadioPresetModemProtocol, AH64RadioPresetData.ELinkProtocol.None)
                ?? 0;
            ddlPresetModemRetriesCount.SelectedIndex = ComboItem<AH64RadioPresetData.ERetries>.FindValue(ddlPresetModemRetriesCount, selectedPreset.Retries) ?? 0;
            ddlPresetModemBaudRate.SelectedIndex = ComboItem<AH64RadioPresetData.EBaudRate>.FindValue(ddlPresetModemBaudRate, selectedPreset.BaudRate) ?? 0;
        }

        /// <summary>
        /// Updates the preset name in the combo box.
        /// </summary>
        private void UpdatePresetNameInDDL(AH64DTCData.EPreset preset)
        {
            int presetIdx = ComboItem<AH64DTCData.EPreset>.FindValue(ddlRadioPresetSelection, preset) ?? 0;
            string newName = GetPresetName(preset);
            (ddlRadioPresetSelection.Items[presetIdx] as ComboItem<AH64DTCData.EPreset>).Text = newName;
            // Force refresh.
            ddlRadioPresetSelection.DisplayMember = "Value";
            ddlRadioPresetSelection.DisplayMember = "Text";
        }

        private string GetPresetName(AH64DTCData.EPreset preset)
        {
            string presetBaseName = Enum.GetName(typeof(AH64DTCData.EPreset), preset).Insert("Preset".Length, " ");
            string unitId = data.GetAH64RadioPreset(preset).UnitId;
            string callsign = data.GetAH64RadioPreset(preset).Callsign;
            return presetBaseName + " / " + (string.IsNullOrEmpty(unitId) ? "NoChg UID" : unitId) + " (" + (string.IsNullOrEmpty(callsign) ? "NoChg C/S" : (callsign)) + ")";
        }

        private void tbRadioPresetUnitID_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();

            toolTip.Hide(this);
            sender.BackColor = default;

            try
            {
                GetSelectedPreset().UnitId = text;
                UpdatePresetNameInDDL(GetSelectedPresetIdent());
            }
            catch (Exception ex)
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, sender, sender.Width, 0, 5000);
            }
        }

        private void tbRadioPresetCallsign_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();
            toolTip.Hide(this);
            sender.BackColor = default;

            // SIC, same check as for DL C/S
            string error = AH64DTCData.CheckDLCallSign(text);
            if (error != null && !string.IsNullOrEmpty(text))
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(error, sender, sender.Width, 0, 5000);
                return;
            }

            try
            {
                GetSelectedPreset().Callsign = text;
                UpdatePresetNameInDDL(GetSelectedPresetIdent());
            }
            catch (Exception ex)
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, sender, sender.Width, 0, 5000);
            }
        }
        #endregion

        #region PrimaryRadio
        private void ddlPresetPrimaryRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().PrimaryRadioSetting = ComboItem<AH64RadioPresetData.EPrimaryRadioSetting>.GetSelectedValue(ddlPresetPrimaryRadio);
        }
        #endregion

        #region PresetVHF
        private void cbPresetVHF_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsVHFData = status;

            nudVHFFreq.Enabled = status;
            cbPresetVHFAllowRecvOnlyRange.Enabled = status;
        }

        private void cbPresetVHFAllowRecvOnlyRange_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox self = sender as CheckBox;
            nudVHFFreq.Minimum = self.Checked ? 108.0m : 116.0m;
        }

        private void nudVHFFreq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().VHFFrequency = control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }
        #endregion

        #region PresetUHF
        private void cbPresetUHF_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsUHFData = status;

            ddlUHFCNV.Enabled = status;
            nudUHFFreq.Enabled = status;
            nudUHFHQ.Enabled = status;
            // Disable N/I controls
            ddlUHFCNV.Enabled = false;
            nudUHFHQ.Enabled = false;
        }

        private void ddlUHFCNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().UHFCNV = ComboItem<AH64RadioCNVSetting>.GetSelectedValue(sender as ComboBox);
        }

        private void nudUHFHQ_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().UHFHaveQuickNet = control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudUHFFreq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().UHFFrequency = control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }
        #endregion

        #region PresetFM1
        private void cbPresetFM1_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsFM1Data = status;

            nudFM1Freq.Enabled = status;
            ddlFM1CNV.Enabled = status;
            nudFM1Hopset.Enabled = status;
            // Disable N/I fields
            ddlFM1CNV.Enabled = false;
            nudFM1Hopset.Enabled = false;
        }

        private void ddlFM1CNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().FM1CNV = ComboItem<AH64RadioCNVSetting>.GetSelectedValue(sender as ComboBox);
        }

        private void nudFM1Hopset_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().FM1Hopset = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudFM1Freq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().FM1Frequency = control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        #endregion

        #region PresetFM2
        private void cbPresetFM2_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsFM1Data = status;

            nudFM2Freq.Enabled = status;
            ddlFM2CNV.Enabled = status;
            nudFM2Hopset.Enabled = status;
            // Disable N/I fields
            ddlFM2CNV.Enabled = false;
            nudFM2Hopset.Enabled = false;
        }

        private void ddlFM2CNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().FM2CNV = ComboItem<AH64RadioCNVSetting>.GetSelectedValue(sender as ComboBox);
        }

        private void nudFM2Hopset_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().FM2Hopset = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudFM2Freq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().FM2Frequency = control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        #endregion

        #region PresetHF
        private void cbPresetHF_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsHFData = status;

            ddlHFCNV.Enabled = status;
            nudHFALENet.Enabled = status;
            nudHFECCMNet.Enabled = status;
            nudHFPresetCH.Enabled = status;
            nudHFRXFreq.Enabled = status;
            cbHFSame.Enabled = status;
            nudHFTXFreq.Enabled = status && !cbHFSame.Checked;
            // Disable N/I fields
            ddlHFCNV.Enabled = false;
            nudHFALENet.Enabled = false;
            nudHFECCMNet.Enabled = false;
            nudHFPresetCH.Enabled = false;
            nudHFRXFreq.Enabled = false;
            nudHFTXFreq.Enabled = false;
        }

        private void cbHFSame_CheckedChanged(object sender, EventArgs e)
        {
            nudHFTXFreq.Enabled = !cbHFSame.Checked && cbPresetHF_Enable.Checked;
            if (cbHFSame.Checked)
            {
                nudHFTXFreq.Value = nudHFRXFreq.Value;
            }
        }

        private void ddlHFCNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().HFCNV = ComboItem<AH64RadioCNVSetting>.GetSelectedValue(sender as ComboBox);
        }

        private void nudHFPresetCH_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().HFPresetChannel = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudHFALENet_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().HFALENet = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudHFECCMNet_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().HFECCMNet = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudHFRXFreq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().HFRXFrequency = (int)control.Value;
                if (cbHFSame.Checked)
                {
                    nudHFTXFreq.Value = control.Value;
                }
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void nudHFTXFreq_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                GetSelectedPreset().HFTXFrequency = (int)control.Value;
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        #endregion

        #region PresetNet
        private void tbPresetNetCS_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();

            UpdateNetControlsEnableStatus();

            toolTip.Hide(this);
            sender.BackColor = default;

            string error = AH64DTCData.CheckDLCallSign(text);
            if (error != null && !string.IsNullOrEmpty(text))
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(error, sender, sender.Width, 0, 5000);
                return;
            }
        }

        private void tbPresetNetID_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();

            toolTip.Hide(this);
            sender.BackColor = default;

            string error = AH64DTCData.CheckDLSubscriberID(text);
            if (error != null && !string.IsNullOrEmpty(text))
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(error, sender, sender.Width, 0, 5000);
                return;
            }

            UpdateNetControlsEnableStatus();
        }

        private void cbPresetNet_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsNetData = status;

            tbPresetNetCS.Enabled = status;
            tbPresetNetID.Enabled = status;
            cbPresetNetPrimary.Enabled = status;
            lbPresetNetMembers.Enabled = status;
            btnPresetNetAdd.Enabled = status;
            btnPresetNetAddToAll.Enabled = status;
            btnPresetNetRemove.Enabled = status;
            UpdateNetControlsEnableStatus();
            
        }

        private void AddMemberToPreset(AH64DTCData.EPreset presetIdent)
        {
            AH64RadioPresetData presetToAddMembersTo = data.GetAH64RadioPreset(presetIdent);

            string subscriberID = tbPresetNetID.Text;
            string callsign = tbPresetNetCS.Text;
            bool primary = cbPresetNetPrimary.Checked;
            bool team = cbPresetNetTeam.Checked && cbPresetNetTeam.Enabled;

            try
            {
                string errorMessage = presetToAddMembersTo.SetOrAddLinkMember(subscriberID, callsign, primary, team);
                if (!(errorMessage is null))
                {
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured when trying to add or edit a member of " + presetIdent.ToString() + ":";
                errorMessage += "\n\n" + ex.Message + "\n\n";
                errorMessage += new string('=', 40) + "\n";
                errorMessage += ex.StackTrace;

                MessageBox.Show(errorMessage, "Error " + presetIdent.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                RepopulateNetMembers();
            }
        }

        /// <summary>
        /// Repopulates the net members list from the preset.
        /// </summary>
        private void RepopulateNetMembers()
        {
            AH64RadioPresetData selectedPreset = GetSelectedPreset();

            lbPresetNetMembers.Items.Clear();
            lbPresetNetMembers.DisplayMember = "Text";
            lbPresetNetMembers.ValueMember = "Value";
            uint netSlotIdx = 0;
            AH64DataLinkMember member;
            while (!((member = selectedPreset.GetLinkMemberSlot(netSlotIdx++)) is null))
            {
                lbPresetNetMembers.Items.Add(new ListBoxItem<AH64DataLinkMember>(member.ToString(), member));
            }

            UpdateNetControlsEnableStatus();
        }

        private void UpdateNetControlsEnableStatus()
        {
            int teamMemberCount = 0;
            bool containsCurrentID = false;
            bool currentIDIsTeamMember = false;
            foreach (ListBoxItem<AH64DataLinkMember> comboItem in lbPresetNetMembers.Items)
            {
                if (comboItem.Value.Team)
                {
                    teamMemberCount++;
                }
                if (comboItem.Value.SubscriberID == tbPresetNetID.Text)
                {
                    containsCurrentID = true;
                    currentIDIsTeamMember = comboItem.Value.Team;
                }
            }
            cbPresetNetTeam.Enabled = cbPresetNet_Enable.Checked
                && (teamMemberCount < 7 || currentIDIsTeamMember);

            btnPresetNetAdd.Enabled = cbPresetNet_Enable.Checked
                && (lbPresetNetMembers.Items.Count < 15 || containsCurrentID)
                && string.IsNullOrEmpty(AH64DTCData.CheckDLCallSign(tbPresetNetCS.Text.ToUpper()))
                && string.IsNullOrEmpty(AH64DTCData.CheckDLSubscriberID(tbPresetNetID.Text.ToUpper()));

            btnPresetNetAddToAll.Enabled = cbPresetNet_Enable.Checked
                && string.IsNullOrEmpty(AH64DTCData.CheckDLCallSign(tbPresetNetCS.Text.ToUpper()))
                && string.IsNullOrEmpty(AH64DTCData.CheckDLSubscriberID(tbPresetNetID.Text.ToUpper()));

            btnPresetNetRemove.Enabled = cbPresetNet_Enable.Checked
                && lbPresetNetMembers.SelectedIndex != -1;

            // select the index in the list, but don't update anything
            if (lbPresetNetMembers.Enabled)
            {
                lbPresetNetMembers.SelectedIndexChanged -= lbPresetNetMembers_SelectedIndexChanged;
                int idx = 0;
                foreach (ListBoxItem<AH64DataLinkMember> lbi in lbPresetNetMembers.Items)
                {
                    if (lbi.Value.SubscriberID == tbPresetNetID.Text.ToUpper())
                    {
                        break;
                    }
                    idx++;
                }
                lbPresetNetMembers.SelectedIndex = (idx == lbPresetNetMembers.Items.Count) ? -1 : idx;
                lbPresetNetMembers.SelectedIndexChanged += lbPresetNetMembers_SelectedIndexChanged;
            }
        }

        private void btnPresetNetAdd_Click(object sender, EventArgs e)
        {
            AddMemberToPreset(GetSelectedPresetIdent());
        }


        private void btnPresetNetAddToAll_Click(object sender, EventArgs e)
        {
            foreach (AH64DTCData.EPreset preset in Enum.GetValues(typeof(AH64DTCData.EPreset)))
            {
                AddMemberToPreset(preset);
            }
        }

        private void btnPresetNetRemove_Click(object sender, EventArgs e)
        {
            AH64RadioPresetData selectedPreset = GetSelectedPreset();

            AH64DataLinkMember member = ListBoxItem<AH64DataLinkMember>.GetSelectedValue(lbPresetNetMembers);
            if (member is null)
            {
                return;
            }
            selectedPreset.RemoveLinkMember(member.SubscriberID);
            RepopulateNetMembers();
        }

        private void lbPresetNetMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            AH64DataLinkMember member = ListBoxItem<AH64DataLinkMember>.GetSelectedValue(sender as ListBox);
            tbPresetNetCS.Text = member.Callsign;
            tbPresetNetID.Text = member.SubscriberID;
            cbPresetNetPrimary.Checked = member.Primary;
            cbPresetNetTeam.Checked = member.Team;

            UpdateNetControlsEnableStatus();
        }
        #endregion

        #region Modem
        private void cbPresetModem_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsModemData = status;

            ddlRadioPresetModemProtocol.Enabled = status;
            ddlPresetModemRetriesCount.Enabled = status;
            ddlPresetModemBaudRate.Enabled = status;
            // Disable N/I Controls
            ddlPresetModemBaudRate.Enabled = false;
        }

        private void ddlRadioPresetModemProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().LinkProtocol = ComboItem<AH64RadioPresetData.ELinkProtocol>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlPresetModemRetriesCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().Retries = ComboItem<AH64RadioPresetData.ERetries>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlPresetModemBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectedPreset().BaudRate = ComboItem<AH64RadioPresetData.EBaudRate>.GetSelectedValue(sender as ComboBox);
        }

        #endregion

        #region OwnshipDL
        private void tbOwnshipCallsign_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();
            toolTip.Hide(this);
            tbOwnshipCallsign.BackColor = default;

            string error = AH64DTCData.CheckDLCallSign(text);
            if (!string.IsNullOrEmpty(error))
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(error, sender, sender.Width, 0, 5000);
                return;
            }
            try
            {
                data.OwnshipCallsign = text;
            }
            catch (Exception ex)
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, sender, sender.Width, 0, 5000);
            }
        }

        private void tbOwnshipSubscriberID_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();
            toolTip.Hide(this);
            tbOwnshipCallsign.BackColor = default;

            string error = AH64DTCData.CheckDLSubscriberID(text);
            if (!string.IsNullOrEmpty(error))
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(error, sender, sender.Width, 0, 5000);
                return;
            }
            try
            {
                data.OwnshipSubscriberID = text;
            }
            catch (Exception ex)
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, sender, sender.Width, 0, 5000);
            }
        }

        #endregion

        #region IFF_XPNDR
        private void tbXpndrTextBox_TextChanged(object sender, EventArgs e)
        {
            var control = sender as TextBox;
            toolTip.Hide(this);
            control.BackColor = default;
            try
            {
                if (control.Name == tbXPNDRMode1.Name)
                {
                    data.Mode1 = control.Text;
                }
                else if (control.Name == tbXPNDRMode2.Name)
                {
                    data.Mode2 = control.Text;
                }
                else if (control.Name == tbXPNDRMode3A.Name)
                {
                    data.Mode3A = control.Text;
                }
                else if (control.Name == tbXPNDRModeSID.Name)
                {
                    data.ModeSFlightID = control.Text.ToUpper();
                }
                else if (control.Name == tbXPNDRModeSAddr.Name)
                {
                    data.ModeSFlightAddress = control.Text;
                }
                else
                {
                    throw new ArgumentException("Unknown text box in callback " + control, nameof(sender));
                }
            }
            catch (Exception ex)
            {
                control.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, control, control.Width, 0, 5000);
            }
        }

        private void ddlXPNDRMode4Key_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.Mode4 = ComboItem<AH64DTCData.EMode4Options>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlXPNDRReply_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.IFFReply = ComboItem<AH64DTCData.EIFFReply>.GetSelectedValue(sender as ComboBox);
        }


        #endregion

        #region ASE
        private void ddlASEAutopage_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ASEAutopage = ComboItem<AH64DTCData.EASEAutopage>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlASEBurstCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ASEBurstCount = ComboItem<AH64DTCData.EASEBurstCount>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlASEBurstInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ASEBurstInterval = ComboItem<AH64DTCData.EASEBurstInterval>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlASESalvoCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ASESalvoCount = ComboItem<AH64DTCData.EASESalvoCount>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlASESalvoInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.ASESalvoInterval = ComboItem<AH64DTCData.EASESalvoInterval>.GetSelectedValue(sender as ComboBox);
        }
        #endregion

        #region ADF
        #endregion

        #region File management

        private readonly OpenFileDialog ofd = new OpenFileDialog()
        {
            Title = "Open AH64 DTC Data File",
            AddExtension = true,
            DefaultExt = "json",
            Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            FileName = "AH64_DTC.json",
            Multiselect = false,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            ShowReadOnly = false
        };

        private void btnLoad_Click(object sender, EventArgs e)
        {
            toolTip.Hide(this);

            ofd.CheckFileExists = true;
            DialogResult result = ofd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            string filePath = ofd.FileName;
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                toolTip.Show("File does not exist.", toolStrip1, 0, toolStrip1.Height, 5000);
                return;
            }

            try
            {
                using (FileStream fileHandle = fi.Open(FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fileHandle, System.Text.Encoding.UTF8))
                    {
                        string data = sr.ReadToEnd();
                        this.data = Newtonsoft.Json.JsonConvert.DeserializeObject<AH64DTCData>(data, MainForm.JsonSerializerSettings);
                        this.data.IsPilot = IsPilot;
                        RefreshControls();
                    }
                }
            }
            catch (Exception ex)
            {
                toolTip.Show(ex.Message, toolStrip1, 0, toolStrip1.Height, 5000);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            toolTip.Hide(this);
            ofd.CheckFileExists = false;

            DialogResult result = ofd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            string filePath = ofd.FileName;
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                FormAskBinaryQuestion overwriteFile = new FormAskBinaryQuestion(this, "Overwrite file?", "Overwrite", "Preserve file", "You are about to overwrite this file.");
                if (!overwriteFile.Result)
                {
                    return;
                }
            }

            try
            {
                using (FileStream fileHandle = fi.Open(FileMode.Create, FileAccess.Write))
                {
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this.data, MainForm.JsonSerializerSettings);
                    byte[] data = new UTF8Encoding(true).GetBytes(jsonData);
                    fileHandle.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                toolTip.Show(ex.Message, toolStrip1, 0, toolStrip1.Height, 5000);
            }
        }

        #endregion
    }
}
