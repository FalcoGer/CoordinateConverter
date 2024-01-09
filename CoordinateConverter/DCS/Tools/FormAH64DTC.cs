using CoordinateConverter.DCS.Aircraft.AH64;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Dictionary<AH64RadioPresetData.EPrimaryRadioSetting, RadioButton> primaryRadioButtonAssociation = null;

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

            {
                // Adjust Frequency NumericUpDowns
                var preset = data.GetAH64RadioPreset(AH64DTCData.EPreset.Preset1);
                nudFM1Freq.Minimum = preset.FM1Frequency.Minimum;
                nudFM1Freq.Maximum = preset.FM1Frequency.Maximum;
                nudFM1Freq.Increment = preset.FM1Frequency.Increment;
                nudFM2Freq.Minimum = preset.FM2Frequency.Minimum;
                nudFM2Freq.Maximum = preset.FM2Frequency.Maximum;
                nudFM2Freq.Increment = preset.FM2Frequency.Increment;
                nudHFRXFreq.Minimum = preset.HFRXFrequency.Minimum;
                nudHFRXFreq.Maximum = preset.HFRXFrequency.Maximum;
                nudHFRXFreq.Increment = preset.HFRXFrequency.Increment;
                nudHFTXFreq.Minimum = preset.HFTXFrequency.Minimum;
                nudHFTXFreq.Maximum = preset.HFTXFrequency.Maximum;
                nudHFTXFreq.Increment = preset.HFTXFrequency.Increment;
                nudUHFFreq.Minimum = preset.UHFFrequency.Minimum;
                nudUHFFreq.Maximum = preset.UHFFrequency.Maximum;
                nudUHFFreq.Increment = preset.UHFFrequency.Increment;
                nudUHFHQ.Minimum = preset.UHFHaveQuickNet.Minimum;
                nudUHFHQ.Maximum = preset.UHFHaveQuickNet.Maximum;
                nudUHFHQ.Increment = preset.UHFHaveQuickNet.Increment;
                nudVHFFreq.Minimum = preset.VHFFrequency.Minimum;
                nudVHFFreq.Maximum = preset.VHFFrequency.Maximum;
                nudVHFFreq.Increment = preset.VHFFrequency.Increment;
            }

            // create primaryRadioButtonAssociation
            primaryRadioButtonAssociation = new Dictionary<AH64RadioPresetData.EPrimaryRadioSetting, RadioButton>()
            {
                { AH64RadioPresetData.EPrimaryRadioSetting.None, rbRadioPrimaryNone },
                { AH64RadioPresetData.EPrimaryRadioSetting.VHF_Single_Channel, rbRadioPrimaryVHF_SC },
                { AH64RadioPresetData.EPrimaryRadioSetting.UHF_Single_Channel, rbRadioPrimaryUHF_SC },
                { AH64RadioPresetData.EPrimaryRadioSetting.UHF_HaveQuick, rbRadioPrimaryUHF_HQ },
                { AH64RadioPresetData.EPrimaryRadioSetting.FM1_Single_Channel, rbRadioPrimaryFM1_SC },
                { AH64RadioPresetData.EPrimaryRadioSetting.FM1_SINCGARS, rbRadioPrimaryFM1_SINC },
                { AH64RadioPresetData.EPrimaryRadioSetting.FM2_Single_Channel, rbRadioPrimaryFM2_SC },
                { AH64RadioPresetData.EPrimaryRadioSetting.FM2_SINCGARS, rbRadioPrimaryFM2_SINC },
                { AH64RadioPresetData.EPrimaryRadioSetting.HF_Single_Channel, rbRadioPrimaryHF_SC },
                { AH64RadioPresetData.EPrimaryRadioSetting.HF_ALE, rbRadioPrimaryHF_ALE },
                { AH64RadioPresetData.EPrimaryRadioSetting.HF_Preset, rbRadioPrimaryHF_PRE },
                { AH64RadioPresetData.EPrimaryRadioSetting.HF_ECCM, rbRadioPrimaryHF_ECCM }
            };

            // Set up ComboBoxes
            ddlRadioPresetSelection.ValueMember = "Value";
            ddlRadioPresetSelection.DisplayMember = "Text";
            foreach (AH64DTCData.EPreset preset in Enum.GetValues(typeof(AH64DTCData.EPreset)))
            {
                string text = Enum.GetName(typeof(AH64DTCData.EPreset), preset).Insert("Preset".Length, " ");
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

        private AH64RadioPresetData GetSelectedPreset()
        {
            AH64DTCData.EPreset selectedPresetIdent = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlRadioPresetSelection);
            AH64RadioPresetData selectedPreset = data.GetAH64RadioPreset(selectedPresetIdent);
            return selectedPreset;
        }

        private void btnReset_Click(object objSender, EventArgs e)
        {
            // objSender and e may be null

            // Reset DTC
            data = new AH64DTCData(IsPilot);
            
            // Reset Radio Presets
            ddlRadioPresetSelection.SelectedIndex = 0;
            // Force refresh
            ddlRadioPresetSelection_SelectedIndexChanged(ddlRadioPresetSelection, e);
            cbRadioPresetPrimaryEnable_CheckedChanged(cbRadioPresetPrimaryEnable, e);
            cbPresetNet_Enable_CheckedChanged(cbPresetNet_Enable, e);
            cbPresetModem_Enable_CheckedChanged(cbPresetModem_Enable, e);
            cbPresetVHF_Enable_CheckedChanged(cbPresetVHF_Enable, e);
            cbPresetUHF_Enable_CheckedChanged(cbPresetUHF_Enable, e);
            cbPresetFM1_Enable_CheckedChanged(cbPresetFM1_Enable, e);
            cbPresetFM2_Enable_CheckedChanged(cbPresetFM2_Enable, e);
            cbPresetHF_Enable_CheckedChanged(cbPresetHF_Enable, e);
            cbPresetVHFAllowRecvOnlyRange_CheckedChanged(cbPresetVHFAllowRecvOnlyRange, e);

            // Reset DL Entry as that is not part of the preset
            tbPresetNetCS.Text = string.Empty;
            tbPresetNetID.Text = string.Empty;
            cbPresetNetPrimary.Checked = false;
            cbPresetNetTeam.Checked = false;

            // TODO: Reset Ownship DL to values in data
            // TODO: Reset IFF/XPNDR to values in data
            // TODO: Reset ASE to values in data
            
        }

        #region PresetGeneral
        private void ddlRadioPresetSelection_SelectedIndexChanged(object objSender, EventArgs e)
        {
            ComboBox sender = objSender as ComboBox;
            AH64DTCData.EPreset selectedPresetIdent = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(sender);
            AH64RadioPresetData selectedPreset = data.GetAH64RadioPreset(selectedPresetIdent);
            // Update all the information
            // VHF
            cbPresetVHF_Enable.Checked = selectedPreset.ContainsVHFData;
            cbPresetVHFAllowRecvOnlyRange.Checked = selectedPreset.VHFFrequencyIsReceiveOnly;
            nudVHFFreq.Value = selectedPreset.VHFFrequency.Frequency;
            // UHF
            cbPresetUHF_Enable.Checked = selectedPreset.ContainsUHFData;
            nudUHFHQ.Value = selectedPreset.UHFHaveQuickNet.Frequency;
            nudUHFFreq.Value = selectedPreset.UHFFrequency.Frequency;
            ddlUHFCNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlUHFCNV, selectedPreset.UHFCNV) ?? 0;
            // FM 1
            cbPresetFM1_Enable.Checked = selectedPreset.ContainsFM1Data;
            ddlFM1CNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlFM1CNV, selectedPreset.FM1CNV) ?? 0;
            nudFM1Hopset.Value = selectedPreset.FM1Hopset;
            nudFM1Freq.Value = selectedPreset.FM1Frequency.Frequency;
            // FM 2
            cbPresetFM2_Enable.Checked = selectedPreset.ContainsFM2Data;
            ddlFM2CNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlFM2CNV, selectedPreset.FM2CNV) ?? 0;
            nudFM2Hopset.Value = selectedPreset.FM2Hopset;
            nudFM2Freq.Value = selectedPreset.FM2Frequency.Frequency;
            // HF
            cbPresetHF_Enable.Checked = selectedPreset.ContainsHFData;
            ddlHFCNV.SelectedIndex = ComboItem<AH64RadioCNVSetting>.FindValue(ddlHFCNV, selectedPreset.HFCNV) ?? 0;
            nudHFPresetCH.Value = selectedPreset.HFPresetChannel;
            nudHFALENet.Value = selectedPreset.HFALENet;
            nudHFECCMNet.Value = selectedPreset.HFECCMNet;
            cbHFSame.Checked = selectedPreset.TxRxLinked;
            nudHFRXFreq.Value = selectedPreset.HFRXFrequency.Frequency;
            nudHFTXFreq.Value = selectedPreset.HFTXFrequency.Frequency;
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
            // Primary Radio
            cbRadioPresetPrimaryEnable.Checked = selectedPreset.ContainsRadioPrimaryData;
            primaryRadioButtonAssociation[selectedPreset.PrimaryRadioSetting].Checked = true;

            // IFF/XPNDR
            // Mode4
            ddlXPNDRMode4Key.SelectedIndex = ComboItem<AH64DTCData.EMode4Options>.FindValue(ddlXPNDRMode4Key, data.Mode4) ?? 0;
            ddlXPNDRReply.SelectedIndex = ComboItem<AH64DTCData.EIFFReply>.FindValue(ddlXPNDRReply, data.IFFReply) ?? 0;
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
            }
            catch (Exception ex)
            {
                sender.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(ex.Message, sender, sender.Width, 0, 5000);
            }
        }
        #endregion

        #region PrimaryRadio
        private void cbRadioPresetPrimaryEnable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            foreach (RadioButton rb in primaryRadioButtonAssociation.Values)
            {
                rb.Enabled = status;
            }
            GetSelectedPreset().ContainsRadioPrimaryData = status;
            // Disable N/I controls
            rbRadioPrimaryUHF_HQ.Enabled = false;
            rbRadioPrimaryFM1_SINC.Enabled = false;
            rbRadioPrimaryFM2_SINC.Enabled = false;
            rbRadioPrimaryHF_ALE.Enabled = false;
            rbRadioPrimaryHF_ECCM.Enabled = false;
            rbRadioPrimaryHF_PRE.Enabled = false;
        }

        private void rbRadioPrimary_CheckedChanged(object objsender, EventArgs e)
        {
            RadioButton sender = objsender as RadioButton;
            AH64RadioPresetData.EPrimaryRadioSetting primary = primaryRadioButtonAssociation.ToList().Find(x => x.Value.Name == sender.Name).Key;
            GetSelectedPreset().PrimaryRadioSetting = primary;
        }
        #endregion

        #region PresetVHF
        private void cbPresetVHF_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
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
                GetSelectedPreset().VHFFrequency.Frequency = control.Value;
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
            ddlUHFCNV.Enabled = status;
            nudUHFFreq.Enabled = status;
            nudUHFHQ.Enabled = status;
            // Disable N/I controls
            ddlUHFCNV.Enabled = false;
            nudUHFHQ.Enabled = false;

            GetSelectedPreset().ContainsUHFData = status;
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
                GetSelectedPreset().UHFHaveQuickNet.Frequency = control.Value;
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
                GetSelectedPreset().UHFFrequency.Frequency = control.Value;
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
                GetSelectedPreset().FM1Frequency.Frequency = control.Value;
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
                GetSelectedPreset().FM2Frequency.Frequency = control.Value;
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
                GetSelectedPreset().HFRXFrequency.Frequency = (int)control.Value;
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
                GetSelectedPreset().HFTXFrequency.Frequency = (int)control.Value;
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
            AH64DTCData.EPreset selectedPresetIdent = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlRadioPresetSelection);
            AddMemberToPreset(selectedPresetIdent);
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
        private void tbOwnshipDL_TextChanged(object sender, EventArgs e)
        {
            string callsignText = tbOwnshipCallsign.Text.ToUpper();
            string subscriberIDText = tbOwnshipSubscriberID.Text.ToUpper();

            bool isError = false;
            toolTip.Hide(this);
            tbOwnshipCallsign.BackColor = default;
            tbOwnshipSubscriberID.BackColor = default;

            // Reset ownship DL info when both strings empty
            if (string.IsNullOrEmpty(callsignText) && string.IsNullOrEmpty(subscriberIDText))
            {
                data.OwnshipDL = null;
                return;
            }

            // Display errors when either field is invalid
            string errorStringCS = AH64DTCData.CheckDLCallSign(callsignText);

            if (errorStringCS != null)
            {
                isError = true;
                tbOwnshipCallsign.BackColor = MainForm.ERROR_COLOR;
                toolTip.Show(errorStringCS, tbOwnshipCallsign, tbOwnshipCallsign.Width, 0, 5000);
            }

            string errorStringID = AH64DTCData.CheckDLSubscriberID(subscriberIDText);
            if (errorStringID != null)
            {
                isError = true;
                tbOwnshipSubscriberID.BackColor = MainForm.ERROR_COLOR;
                if (string.IsNullOrEmpty(errorStringCS))
                {
                    // only show tooltip error for ID when CS is valid.
                    toolTip.Show(errorStringID, tbOwnshipSubscriberID, tbOwnshipSubscriberID.Width, 0, 5000);
                }
            }

            // Don't set when either field is invalid
            if (isError)
            {
                return;
            }

            // Update ownship info.
            data.OwnshipDL = new AH64DataLinkMember(callsignText, subscriberIDText, true, true);
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
