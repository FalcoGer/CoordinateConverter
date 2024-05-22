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

                nudTuneFM1ManualFreq.Minimum = preset.FM1FrequencyMinimum;
                nudTuneFM1ManualFreq.Maximum = preset.FM1FrequencyMaximum;
                nudTuneFM1ManualFreq.Increment = preset.FM1FrequencyIncrement;
                nudTuneFM2ManualFreq.Minimum = preset.FM2FrequencyMinimum;
                nudTuneFM2ManualFreq.Maximum = preset.FM2FrequencyMaximum;
                nudTuneFM2ManualFreq.Increment = preset.FM2FrequencyIncrement;
                nudTuneHFRXManualFreq.Minimum = preset.HFRXFrequencyMinimum;
                nudTuneHFRXManualFreq.Maximum = preset.HFRXFrequencyMaximum;
                nudTuneHFRXManualFreq.Increment = preset.HFRXFrequencyIncrement;
                nudTuneUHFManualFreq.Minimum = preset.UHFFrequencyMinimum;
                nudTuneUHFManualFreq.Maximum = preset.UHFFrequencyMaximum;
                nudTuneUHFManualFreq.Increment = preset.UHFFrequencyIncrement;
                nudTuneVHFManualFreq.Minimum = preset.VHFFrequencyMinimum;
                nudTuneVHFManualFreq.Maximum = preset.VHFFrequencyMaximum;
                nudTuneVHFManualFreq.Increment = preset.VHFFrequencyIncrement;
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
            for (int cnvValue = 0; cnvValue <= 6; cnvValue++)
            {
                AH64RadioCNVSetting cnv = (cnvValue != 0) ? new AH64RadioCNVSetting(cnvValue) : new AH64RadioCNVSetting(null);
                string name = (cnvValue != 0) ? "CNV " + cnvValue.ToString() : "No Change";

                ddlUHFCNV.Items.Add(new ComboItem<AH64RadioCNVSetting>(name, cnv));
                ddlFM1CNV.Items.Add(new ComboItem<AH64RadioCNVSetting>(name, cnv));
                ddlFM2CNV.Items.Add(new ComboItem<AH64RadioCNVSetting>(name, cnv));
                ddlHFCNV.Items.Add(new ComboItem<AH64RadioCNVSetting>(name, cnv));
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

            ddlTuneUHFGuardReciever.ValueMember = "Value";
            ddlTuneUHFGuardReciever.DisplayMember = "Text";
            foreach (AH64DTCData.EUHFGuardReceiver guardReceiver in Enum.GetValues(typeof(AH64DTCData.EUHFGuardReceiver)))
            {
                string text = Enum.GetName(typeof(AH64DTCData.EUHFGuardReceiver), guardReceiver);
                ComboItem<AH64DTCData.EUHFGuardReceiver> item = new ComboItem<AH64DTCData.EUHFGuardReceiver>(text, guardReceiver);
                ddlTuneUHFGuardReciever.Items.Add(item);
            }

            // XPNDR

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

            ddlXPNDRAntenna.ValueMember = "Value";
            ddlXPNDRAntenna.DisplayMember = "Text";
            foreach (AH64DTCData.EIFFAntenna antennaOption in Enum.GetValues(typeof(AH64DTCData.EIFFAntenna)))
            {
                ddlXPNDRAntenna.Items.Add(new ComboItem<AH64DTCData.EIFFAntenna>(antennaOption.ToString().Replace('_', ' '), antennaOption));
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

            foreach (var ddlLaserChannelSelection in new List<ComboBox>() { ddlLaserLRFD, ddlLaserLST, ddlMslChannel1, ddlMslChannel2, ddlMslChannel3, ddlMslChannel4 })
            {
                ddlLaserChannelSelection.ValueMember = "Value";
                ddlLaserChannelSelection.DisplayMember = "Text";
                foreach (AH64DTCData.ELaserChannel laserChannel in Enum.GetValues(typeof(AH64DTCData.ELaserChannel)))
                {
                    ddlLaserChannelSelection.Items.Add(new ComboItem<AH64DTCData.ELaserChannel>(laserChannel.ToString().Replace('_', ' '), laserChannel));
                }
            }

            ddlMslChannelPriority.ValueMember = "Value";
            ddlMslChannelPriority.DisplayMember = "Text";
            foreach (AH64DTCData.EMissileChannel priorityhannel in Enum.GetValues(typeof(AH64DTCData.EMissileChannel)))
            {
                ddlMslChannelPriority.Items.Add(new ComboItem<AH64DTCData.EMissileChannel>(priorityhannel.ToString().Replace('_', ' '), priorityhannel));
            }

            ddlMslChannelAlternate.ValueMember = "Value";
            ddlMslChannelAlternate.DisplayMember = "Text";
            foreach (AH64DTCData.EMissileChannel AlternateChannel in Enum.GetValues(typeof(AH64DTCData.EMissileChannel)))
            {
                ddlMslChannelAlternate.Items.Add(new ComboItem<AH64DTCData.EMissileChannel>(AlternateChannel.ToString().Replace('_', ' '), AlternateChannel));
            }

            ddlGunBurstLength.ValueMember = "Value";
            ddlGunBurstLength.DisplayMember = "Text";
            foreach (AH64DTCData.EGunBurstLength burstLength in Enum.GetValues(typeof(AH64DTCData.EGunBurstLength)))
            {
                ddlGunBurstLength.Items.Add(new ComboItem<AH64DTCData.EGunBurstLength>(burstLength.ToString().Replace('_', ' '), burstLength));
            }
            
            ddlRktQty.ValueMember = "Value";
            ddlRktQty.DisplayMember = "Text";
            foreach (AH64DTCData.ERocketQuantity rocketQuantity in Enum.GetValues(typeof(AH64DTCData.ERocketQuantity)))
            {
                ddlRktQty.Items.Add(new ComboItem<AH64DTCData.ERocketQuantity>(rocketQuantity.ToString().Replace('_', ' '), rocketQuantity));
            }

            // Tune
            foreach (ComboBox ddl in new List<ComboBox>()
            {
                ddlTuneFM1Preset, ddlTuneFM2Preset, ddlTuneHFPreset, ddlTuneUHFPreset, ddlTuneVHFPreset
            })
            {
                ddl.ValueMember = "Value";
                ddl.DisplayMember = "Text";
                
                foreach (AH64DTCData.EPreset preset in Enum.GetValues(typeof(AH64DTCData.EPreset)))
                {
                    ddl.Items.Add(new ComboItem<AH64DTCData.EPreset>(GetPresetName(preset), preset));
                }
            }

            // TSD
            ddl_TSD_type.ValueMember = "Value";
            ddl_TSD_type.DisplayMember = "Text";
            ddl_TSD_type.Items.Add(new ComboItem<AH64TSDOptionData.EMapType>("No Change", AH64TSDOptionData.EMapType.No_Change));
            ddl_TSD_type.Items.Add(new ComboItem<AH64TSDOptionData.EMapType>("Chart", AH64TSDOptionData.EMapType.Chart));
            ddl_TSD_type.Items.Add(new ComboItem<AH64TSDOptionData.EMapType>("Satellite", AH64TSDOptionData.EMapType.Satellite));
            ddl_TSD_type.Items.Add(new ComboItem<AH64TSDOptionData.EMapType>("Digital", AH64TSDOptionData.EMapType.Digital));
            ddl_TSD_type.Items.Add(new ComboItem<AH64TSDOptionData.EMapType>("Stick", AH64TSDOptionData.EMapType.Stick));

            ddl_TSD_colorBand.ValueMember = "Value";
            ddl_TSD_colorBand.DisplayMember = "Text";
            ddl_TSD_colorBand.Items.Add(new ComboItem<AH64TSDOptionData.EColorBand>("No Change", AH64TSDOptionData.EColorBand.No_Change));
            ddl_TSD_colorBand.Items.Add(new ComboItem<AH64TSDOptionData.EColorBand>("None", AH64TSDOptionData.EColorBand.None));
            ddl_TSD_colorBand.Items.Add(new ComboItem<AH64TSDOptionData.EColorBand>("A/C", AH64TSDOptionData.EColorBand.AC));
            ddl_TSD_colorBand.Items.Add(new ComboItem<AH64TSDOptionData.EColorBand>("Elevation", AH64TSDOptionData.EColorBand.Elevation));

            ddl_TSD_contours.ValueMember = "Value";
            ddl_TSD_contours.DisplayMember = "Text";
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("No Change", AH64TSDOptionData.EContours.No_Change));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("None", AH64TSDOptionData.EContours.Contours_None));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("50", AH64TSDOptionData.EContours.Contours_50));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("100", AH64TSDOptionData.EContours.Contours_100));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("200", AH64TSDOptionData.EContours.Contours_200));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("500", AH64TSDOptionData.EContours.Contours_500));
            ddl_TSD_contours.Items.Add(new ComboItem<AH64TSDOptionData.EContours>("1000", AH64TSDOptionData.EContours.Contours_1000));

            ddl_TSD_ffd.ValueMember = "Value";
            ddl_TSD_ffd.DisplayMember = "Text";
            ddl_TSD_ffd.Items.Add(new ComboItem<AH64TSDOptionData.EFfd>("No Change", AH64TSDOptionData.EFfd.No_Change));
            ddl_TSD_ffd.Items.Add(new ComboItem<AH64TSDOptionData.EFfd>("None", AH64TSDOptionData.EFfd.FFD_None));
            ddl_TSD_ffd.Items.Add(new ComboItem<AH64TSDOptionData.EFfd>("Both", AH64TSDOptionData.EFfd.FFD_Both));
            ddl_TSD_ffd.Items.Add(new ComboItem<AH64TSDOptionData.EFfd>("Areas", AH64TSDOptionData.EFfd.FFD_Areas));
            ddl_TSD_ffd.Items.Add(new ComboItem<AH64TSDOptionData.EFfd>("Lines", AH64TSDOptionData.EFfd.FFD_Lines));

            ddl_TSD_gray.ValueMember = "Value";
            ddl_TSD_gray.DisplayMember = "Text";
            ddl_TSD_gray.Items.Add(new ComboItem<AH64TSDOptionData.EGray>("No Change", AH64TSDOptionData.EGray.No_Change));
            ddl_TSD_gray.Items.Add(new ComboItem<AH64TSDOptionData.EGray>("Gray", AH64TSDOptionData.EGray.Gray));
            ddl_TSD_gray.Items.Add(new ComboItem<AH64TSDOptionData.EGray>("Green/Gray", AH64TSDOptionData.EGray.Green_and_gray));

            ddl_TSD_satLevel.ValueMember = "Value";
            ddl_TSD_satLevel.DisplayMember = "Text";
            ddl_TSD_satLevel.Items.Add(new ComboItem<AH64TSDOptionData.ESatLevel>("No Change", AH64TSDOptionData.ESatLevel.No_Change));
            ddl_TSD_satLevel.Items.Add(new ComboItem<AH64TSDOptionData.ESatLevel>("5m", AH64TSDOptionData.ESatLevel.L5));
            ddl_TSD_satLevel.Items.Add(new ComboItem<AH64TSDOptionData.ESatLevel>("10m", AH64TSDOptionData.ESatLevel.L10));

            ddl_TSD_center.ValueMember = "Value";
            ddl_TSD_center .DisplayMember = "Text";
            ddl_TSD_center.Items.Add(new ComboItem<AH64TSDOptionData.ECenter>("No Change", AH64TSDOptionData.ECenter.No_Change));
            ddl_TSD_center.Items.Add(new ComboItem<AH64TSDOptionData.ECenter>("Depressed", AH64TSDOptionData.ECenter.Depressed));
            ddl_TSD_center.Items.Add(new ComboItem<AH64TSDOptionData.ECenter>("Centered", AH64TSDOptionData.ECenter.Center));

            ddl_TSD_orient.ValueMember = "Value";
            ddl_TSD_orient.DisplayMember = "Text";
            ddl_TSD_orient.Items.Add(new ComboItem<AH64TSDOptionData.EOrientation>("No Change", AH64TSDOptionData.EOrientation.No_Change));
            ddl_TSD_orient.Items.Add(new ComboItem<AH64TSDOptionData.EOrientation>("Heading Up", AH64TSDOptionData.EOrientation.HeadingUp));
            ddl_TSD_orient.Items.Add(new ComboItem<AH64TSDOptionData.EOrientation>("Track Up", AH64TSDOptionData.EOrientation.TrackUp));
            ddl_TSD_orient.Items.Add(new ComboItem<AH64TSDOptionData.EOrientation>("North Up", AH64TSDOptionData.EOrientation.NorthUp));

            ddl_TSD_phase.ValueMember = "Value";
            ddl_TSD_phase.DisplayMember = "Text";
            ddl_TSD_phase.Items.Add(new ComboItem<AH64TSDOptionData.EPhase>("No Change", AH64TSDOptionData.EPhase.No_Change));
            ddl_TSD_phase.Items.Add(new ComboItem<AH64TSDOptionData.EPhase>("Navigation", AH64TSDOptionData.EPhase.Navigation));
            ddl_TSD_phase.Items.Add(new ComboItem<AH64TSDOptionData.EPhase>("Attack", AH64TSDOptionData.EPhase.Attack));

            ddl_TSD_grid.ValueMember = "Value";
            ddl_TSD_grid.DisplayMember = "Text";
            ddl_TSD_grid.Items.Add(new ComboItem<AH64TSDOptionData.EGrid>("No Change", AH64TSDOptionData.EGrid.No_Change));
            ddl_TSD_grid.Items.Add(new ComboItem<AH64TSDOptionData.EGrid>("Show Grid", AH64TSDOptionData.EGrid.Grid_Normal));
            ddl_TSD_grid.Items.Add(new ComboItem<AH64TSDOptionData.EGrid>("Hide Grid", AH64TSDOptionData.EGrid.Grid_None));

            ddl_TSD_showThreatVis.ValueMember = "Value";
            ddl_TSD_showThreatVis.DisplayMember = "Text";
            ddl_TSD_showThreatVis.Items.Add(new ComboItem<AH64TSDOptionData.EVis>("No Change", AH64TSDOptionData.EVis.No_Change));
            ddl_TSD_showThreatVis.Items.Add(new ComboItem<AH64TSDOptionData.EVis>("Threat", AH64TSDOptionData.EVis.Threat));
            ddl_TSD_showThreatVis.Items.Add(new ComboItem<AH64TSDOptionData.EVis>("Own", AH64TSDOptionData.EVis.Own));

            foreach (ComboBox ddl in new List<ComboBox>()
            { ddl_TSD_showAtkAreas, ddl_TSD_showAtkCtrlMeasures, ddl_TSD_showAtkCurrentRoute, ddl_TSD_showAtkCursor, ddl_TSD_showAtkCursorInfo
            , ddl_TSD_showAtkEndurance, ddl_TSD_showAtkEnemyUnits, ddl_TSD_showAtkFriendlyUnits, ddl_TSD_showAtkHSI, ddl_TSD_showAtkInactiveZones, ddl_TSD_showAtkLines
            , ddl_TSD_showAtkObstacles, ddl_TSD_showAtkShotAt, ddl_TSD_showAtkTargets, ddl_TSD_showAtkWind, ddl_TSD_showNavAreas, ddl_TSD_showNavCtrlMeasures
            , ddl_TSD_showNavCursor, ddl_TSD_showNavCursorInfo, ddl_TSD_showNavEndurance, ddl_TSD_showNavEnemyUnits, ddl_TSD_showNavFriendlyUnits, ddl_TSD_showNavHSI
            , ddl_TSD_showNavInactiveZones, ddl_TSD_showNavLines, ddl_TSD_showNavObstacles, ddl_TSD_showNavTargets, ddl_TSD_showNavWind, ddl_TSD_showNavWPData, ddl_TSD_showOwnGhost
            , ddl_TSD_showOwnOwn, ddl_TSD_showOwnRings, ddl_TSD_showOwnTrnPt, ddl_TSD_showThreatACQ, ddl_TSD_showThreatASE, ddl_TSD_showThreatFCR, ddl_TSD_showThreatTargets
            , ddl_TSD_showThreatThreats, ddl_TSD_showThreatTrnPt, ddl_TSD_showThreatVisShade})
            {
                ddl.ValueMember = "Value";
                ddl.DisplayMember = "Text";

                foreach (AH64TSDOptionData.EFilter filter in Enum.GetValues(typeof(AH64TSDOptionData.EFilter)))
                {
                    string filterName = filter == AH64TSDOptionData.EFilter.No_Change ? "No Change" : filter.ToString();
                    ddl.Items.Add(new ComboItem<AH64TSDOptionData.EFilter>(filterName, filter));
                }
            }

            // Call reset
            btnReset_Click(null, null);
        }

        /// <summary>
        /// Gets the number of commands sent.
        /// </summary>
        /// <value>
        /// The number of commands sent.
        /// </value>
        public int CommandsSentCount { get; private set; } = 0;

        private void btnXfer_Click(object sender, EventArgs e)
        {
            CommandsSentCount = data.SendToDCS(null);
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
            ddlXPNDRAntenna.SelectedIndex = ComboItem<AH64DTCData.EIFFAntenna>.FindValue(ddlXPNDRAntenna, data.IFFAntenna) ?? 0;
            // Reset ASE to values in data
            ddlASEAutopage.SelectedIndex = ComboItem<AH64DTCData.EASEAutopage>.FindValue(ddlASEAutopage, data.ASEAutopage) ?? 0;
            ddlASEBurstCount.SelectedIndex = ComboItem<AH64DTCData.EASEBurstCount>.FindValue(ddlASEBurstCount, data.ASEBurstCount) ?? 0;
            ddlASEBurstInterval.SelectedIndex = ComboItem<AH64DTCData.EASEBurstInterval>.FindValue(ddlASEBurstInterval, data.ASEBurstInterval) ?? 0;
            ddlASESalvoCount.SelectedIndex = ComboItem<AH64DTCData.EASESalvoCount>.FindValue(ddlASESalvoCount, data.ASESalvoCount) ?? 0;
            ddlASESalvoInterval.SelectedIndex = ComboItem<AH64DTCData.EASESalvoInterval>.FindValue(ddlASESalvoInterval, data.ASESalvoInterval) ?? 0;
            // Reset WPN to values in data
            ddlMslChannel1.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlMslChannel1, data.GetMissileChannel(AH64DTCData.EMissileChannel.CH_1)) ?? 0;
            ddlMslChannel2.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlMslChannel2, data.GetMissileChannel(AH64DTCData.EMissileChannel.CH_2)) ?? 0;
            ddlMslChannel3.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlMslChannel3, data.GetMissileChannel(AH64DTCData.EMissileChannel.CH_3)) ?? 0;
            ddlMslChannel4.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlMslChannel4, data.GetMissileChannel(AH64DTCData.EMissileChannel.CH_4)) ?? 0;

            ddlMslChannelPriority.SelectedIndex = ComboItem<AH64DTCData.EMissileChannel>.FindValue(ddlMslChannelPriority, data.MissilePriorityChannel) ?? 0;
            ddlMslChannelAlternate.SelectedIndex = ComboItem<AH64DTCData.EMissileChannel>.FindValue(ddlMslChannelAlternate, data.MissileAlternateChannel) ?? 0;
            ddlMslChannelPriority_SelectedIndexChanged(ddlMslChannelPriority, null);

            ddlGunBurstLength.SelectedIndex = ComboItem<AH64DTCData.EGunBurstLength>.FindValue(ddlGunBurstLength, data.GunBurstLength) ?? 0;
            ddlRktQty.SelectedIndex = ComboItem<AH64DTCData.ERocketQuantity>.FindValue(ddlRktQty, data.RocketQuantity) ?? 0;

            ddlLaserLRFD.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlLaserLRFD, data.LrfdLaserChannel) ?? 0;
            ddlLaserLST.SelectedIndex = ComboItem<AH64DTCData.ELaserChannel>.FindValue(ddlLaserLST, data.LstLaserChannel) ?? 0;

            cbManRange.CheckedChanged -= cbManRange_CheckedChanged;
            if (data.ManRange.HasValue)
            {
                cbManRange.Checked = true;
                nudManRange.Enabled = true;
                nudManRange.Value = data.ManRange.Value;
            }
            else
            {
                cbManRange.Checked = false;
                nudManRange.Enabled = false;
            }
            cbManRange.CheckedChanged += cbManRange_CheckedChanged;
            
            foreach (TextBox tb in new List<TextBox>()
            {
                tb_laserCodeA,
                tb_laserCodeB,
                tb_laserCodeC,
                tb_laserCodeD,
                tb_laserCodeE,
                tb_laserCodeF,
                tb_laserCodeG,
                tb_laserCodeH,
                tb_laserCodeJ,
                tb_laserCodeK,
                tb_laserCodeL,
                tb_laserCodeM,
                tb_laserCodeN,
                tb_laserCodeP,
                tb_laserCodeQ,
                tb_laserCodeR
            })
            {
                var laserChannel = (AH64DTCData.ELaserChannel)Enum.Parse(typeof(AH64DTCData.ELaserChannel), tb.Name.Last().ToString());
                AH64LaserCode laserCode = data.GetLaserCodeFrequency(laserChannel);
                tb.Text = (laserCode != null) ? laserCode.LaserCode.ToString() : string.Empty;
            }
            // TODO: Reset ADF to values in data

            // Radio Tune
            // VHF
            nudTuneVHFManualFreq.Value = data.VHFManualFrequency;
            ddlTuneVHFPreset.SelectedIndex = ComboItem<AH64DTCData.EPreset>.FindValue(ddlTuneVHFPreset, data.VHFTunePreset) ?? 0;
            rbTuneVHFNoChange.Checked = data.VHFTuneSetting == AH64DTCData.ETuneSetting.No_Change;
            rbTuneVHFPreset.Checked = data.VHFTuneSetting == AH64DTCData.ETuneSetting.Preset;
            rbTuneVHFMan.Checked = data.VHFTuneSetting == AH64DTCData.ETuneSetting.Manual;
            // UHF
            nudTuneUHFManualFreq.Value = data.UHFManualFrequency;
            ddlTuneUHFPreset.SelectedIndex = ComboItem<AH64DTCData.EPreset>.FindValue(ddlTuneUHFPreset, data.UHFTunePreset) ?? 0;
            rbTuneUHFNoChange.Checked = data.UHFTuneSetting == AH64DTCData.ETuneSetting.No_Change;
            rbTuneUHFPreset.Checked = data.UHFTuneSetting == AH64DTCData.ETuneSetting.Preset;
            rbTuneUHFMan.Checked = data.UHFTuneSetting == AH64DTCData.ETuneSetting.Manual;

            ddlTuneUHFGuardReciever.SelectedIndex = ComboItem<AH64DTCData.EUHFGuardReceiver>.FindValue(ddlTuneUHFGuardReciever, data.UHFGuardReceiver) ?? 0;
            // FM1
            nudTuneFM1ManualFreq.Value = data.FM1ManualFrequency;
            ddlTuneFM1Preset.SelectedIndex = ComboItem<AH64DTCData.EPreset>.FindValue(ddlTuneFM1Preset, data.FM1TunePreset) ?? 0;
            rbTuneFM1NoChange.Checked = data.FM1TuneSetting == AH64DTCData.ETuneSetting.No_Change;
            rbTuneFM1Preset.Checked = data.FM1TuneSetting == AH64DTCData.ETuneSetting.Preset;
            rbTuneFM1Man.Checked = data.FM1TuneSetting == AH64DTCData.ETuneSetting.Manual;
            // FM2
            nudTuneFM2ManualFreq.Value = data.FM2ManualFrequency;
            ddlTuneFM2Preset.SelectedIndex = ComboItem<AH64DTCData.EPreset>.FindValue(ddlTuneFM2Preset, data.FM2TunePreset) ?? 0;
            rbTuneFM2NoChange.Checked = data.FM2TuneSetting == AH64DTCData.ETuneSetting.No_Change;
            rbTuneFM2Preset.Checked = data.FM2TuneSetting == AH64DTCData.ETuneSetting.Preset;
            rbTuneFM2Man.Checked = data.FM2TuneSetting == AH64DTCData.ETuneSetting.Manual;
            // HF
            nudTuneHFRXManualFreq.Value = data.HFRXManualFrequency;
            ddlTuneHFPreset.SelectedIndex = ComboItem<AH64DTCData.EPreset>.FindValue(ddlTuneHFPreset, data.HFTunePreset) ?? 0;
            rbTuneHFNoChange.Checked = data.HFTuneSetting == AH64DTCData.ETuneSetting.No_Change;
            rbTuneHFPreset.Checked = data.HFTuneSetting == AH64DTCData.ETuneSetting.Preset;
            rbTuneHFMan.Checked = data.HFTuneSetting == AH64DTCData.ETuneSetting.Manual;

            // TSD
            ddl_TSD_type.SelectedIndex = ComboItem<AH64TSDOptionData.EMapType>.FindValue(ddl_TSD_type, data.TSDData.MapType) ?? 0;
            ddl_TSD_colorBand.SelectedIndex = ComboItem<AH64TSDOptionData.EColorBand>.FindValue(ddl_TSD_colorBand, data.TSDData.ColorBand) ?? 0;
            ddl_TSD_contours.SelectedIndex = ComboItem<AH64TSDOptionData.EContours>.FindValue(ddl_TSD_contours, data.TSDData.Contours) ?? 0;
            ddl_TSD_ffd.SelectedIndex = ComboItem<AH64TSDOptionData.EFfd>.FindValue(ddl_TSD_ffd, data.TSDData.ffd) ?? 0;
            ddl_TSD_gray.SelectedIndex = ComboItem<AH64TSDOptionData.EGray>.FindValue(ddl_TSD_gray, data.TSDData.Gray) ?? 0;
            ddl_TSD_satLevel.SelectedIndex = ComboItem<AH64TSDOptionData.ESatLevel>.FindValue(ddl_TSD_satLevel,data.TSDData.SatLevel) ?? 0;
            ddl_TSD_center.SelectedIndex = ComboItem<AH64TSDOptionData.ECenter>.FindValue(ddl_TSD_center, data.TSDData.Center) ?? 0;
            ddl_TSD_orient.SelectedIndex = ComboItem<AH64TSDOptionData.EOrientation>.FindValue(ddl_TSD_orient, data.TSDData.Orientation) ?? 0;
            ddl_TSD_phase.SelectedIndex = ComboItem<AH64TSDOptionData.EPhase>.FindValue(ddl_TSD_phase, data.TSDData.Phase) ?? 0;
            ddl_TSD_grid.SelectedIndex = ComboItem<AH64TSDOptionData.EGrid>.FindValue(ddl_TSD_grid, data.TSDData.Grid) ?? 0;
            ddl_TSD_showThreatVis.SelectedIndex = ComboItem<AH64TSDOptionData.EVis>.FindValue(ddl_TSD_showThreatVis, data.TSDData.ThreatVis) ?? 0;

            ddl_TSD_showAtkAreas.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkAreas, data.TSDData.AtkAreas) ?? 0;
            ddl_TSD_showAtkCtrlMeasures.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkCtrlMeasures, data.TSDData.AtkCtrlMeasures) ?? 0;
            ddl_TSD_showAtkCurrentRoute.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkCurrentRoute, data.TSDData.AtkCurrentRoute) ?? 0;
            ddl_TSD_showAtkCursor.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkCursor, data.TSDData.AtkCursor) ?? 0;
            ddl_TSD_showAtkCursorInfo.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkCursorInfo, data.TSDData.AtkCursorInfo) ?? 0;
            ddl_TSD_showAtkEndurance.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkEndurance, data.TSDData.AtkEndurance) ?? 0;
            ddl_TSD_showAtkEnemyUnits.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkEnemyUnits, data.TSDData.AtkEnemyUnits) ?? 0;
            ddl_TSD_showAtkFriendlyUnits.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkFriendlyUnits, data.TSDData.AtkFriendlyUnits) ?? 0;
            ddl_TSD_showAtkHSI.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkHSI, data.TSDData.AtkHSI) ?? 0;
            ddl_TSD_showAtkInactiveZones.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkInactiveZones, data.TSDData.AtkInactiveZones) ?? 0;
            ddl_TSD_showAtkLines.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkLines, data.TSDData.AtkLines) ?? 0;
            ddl_TSD_showAtkObstacles.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkObstacles, data.TSDData.AtkFCRObstacles) ?? 0;
            ddl_TSD_showAtkShotAt.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkShotAt, data.TSDData.AtkShotAt) ?? 0;
            ddl_TSD_showAtkTargets.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkTargets, data.TSDData.AtkTargets) ?? 0;
            ddl_TSD_showAtkWind.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showAtkWind, data.TSDData.AtkWind) ?? 0;
            ddl_TSD_showNavAreas.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavAreas, data.TSDData.NavAreas) ?? 0;
            ddl_TSD_showNavCtrlMeasures.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavCtrlMeasures, data.TSDData.NavCtrlMeasures) ?? 0;
            ddl_TSD_showNavCursor.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavCursor, data.TSDData.NavCursor) ?? 0;
            ddl_TSD_showNavCursorInfo.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavCursorInfo, data.TSDData.NavCursorInfo) ?? 0;
            ddl_TSD_showNavEndurance.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavEndurance, data.TSDData.NavEndurance) ?? 0;
            ddl_TSD_showNavEnemyUnits.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavEnemyUnits, data.TSDData.NavEnemyUnits) ?? 0;
            ddl_TSD_showNavFriendlyUnits.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavFriendlyUnits, data.TSDData.NavFriendlyUnits) ?? 0;
            ddl_TSD_showNavHSI.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavHSI, data.TSDData.NavHSI) ?? 0;
            ddl_TSD_showNavInactiveZones.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavInactiveZones, data.TSDData.NavInactiveZones) ?? 0;
            ddl_TSD_showNavLines.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavLines, data.TSDData.NavLines) ?? 0;
            ddl_TSD_showNavObstacles.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavObstacles, data.TSDData.NavObstacles) ?? 0;
            ddl_TSD_showNavTargets.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavTargets, data.TSDData.NavTargets) ?? 0;
            ddl_TSD_showNavWind.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavWind, data.TSDData.NavWind) ?? 0;
            ddl_TSD_showNavWPData.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showNavWPData, data.TSDData.NavWpData) ?? 0;
            ddl_TSD_showOwnGhost.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showOwnGhost, data.TSDData.VisOwnGhost) ?? 0;
            ddl_TSD_showOwnOwn.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showOwnOwn, data.TSDData.VisOwnOwn) ?? 0;
            ddl_TSD_showOwnRings.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showOwnRings, data.TSDData.VisOwnRings) ?? 0;
            ddl_TSD_showOwnTrnPt.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showOwnTrnPt, data.TSDData.VisOwnTrnPt) ?? 0;
            ddl_TSD_showThreatACQ.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatACQ, data.TSDData.VisThreatACQ) ?? 0;
            ddl_TSD_showThreatASE.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatASE, data.TSDData.AseThreats) ?? 0;
            ddl_TSD_showThreatFCR.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatFCR, data.TSDData.VisThreatFCR) ?? 0;
            ddl_TSD_showThreatTargets.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatTargets, data.TSDData.visThreatTargets) ?? 0;
            ddl_TSD_showThreatThreats.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatThreats, data.TSDData.VisThreatThreats) ?? 0;
            ddl_TSD_showThreatTrnPt.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatTrnPt, data.TSDData.VisThreatTrnPt) ?? 0;
            ddl_TSD_showThreatVisShade.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl_TSD_showThreatVisShade, data.TSDData.VisShade) ?? 0;
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

            // TSD
            ddl_TSD_type.SelectedIndex = ComboItem<AH64TSDOptionData.EMapType>.FindValue(ddl_TSD_type, AH64TSDOptionData.EMapType.No_Change) ?? 0;
            ddl_TSD_colorBand.SelectedIndex = ComboItem<AH64TSDOptionData.EColorBand>.FindValue(ddl_TSD_colorBand, AH64TSDOptionData.EColorBand.No_Change) ?? 0;
            ddl_TSD_contours.SelectedIndex = ComboItem<AH64TSDOptionData.EContours>.FindValue(ddl_TSD_contours, AH64TSDOptionData.EContours.No_Change) ?? 0;            
            ddl_TSD_ffd.SelectedIndex = ComboItem<AH64TSDOptionData.EFfd>.FindValue(ddl_TSD_ffd, AH64TSDOptionData.EFfd.No_Change) ?? 0;
            ddl_TSD_gray.SelectedIndex = ComboItem<AH64TSDOptionData.EGray>.FindValue(ddl_TSD_gray, AH64TSDOptionData.EGray.No_Change) ?? 0;
            ddl_TSD_satLevel.SelectedIndex = ComboItem<AH64TSDOptionData.ESatLevel>.FindValue(ddl_TSD_satLevel, AH64TSDOptionData.ESatLevel.No_Change) ?? 0;
            ddl_TSD_center.SelectedIndex = ComboItem<AH64TSDOptionData.ECenter>.FindValue(ddl_TSD_center, AH64TSDOptionData.ECenter.No_Change) ?? 0;
            ddl_TSD_orient.SelectedIndex = ComboItem<AH64TSDOptionData.EOrientation>.FindValue(ddl_TSD_orient, AH64TSDOptionData.EOrientation.No_Change) ?? 0;
            ddl_TSD_phase.SelectedIndex = ComboItem<AH64TSDOptionData.EPhase>.FindValue(ddl_TSD_phase, AH64TSDOptionData.EPhase.No_Change) ?? 0;
            ddl_TSD_grid.SelectedIndex = ComboItem<AH64TSDOptionData.EGrid>.FindValue(ddl_TSD_grid, AH64TSDOptionData.EGrid.No_Change) ?? 0;
            ddl_TSD_showThreatVis.SelectedIndex = ComboItem<AH64TSDOptionData.EVis>.FindValue(ddl_TSD_showThreatVis, AH64TSDOptionData.EVis.No_Change) ?? 0;

            foreach (ComboBox ddl in new List<ComboBox>()
                { ddl_TSD_showAtkAreas, ddl_TSD_showAtkCtrlMeasures, ddl_TSD_showAtkCurrentRoute, ddl_TSD_showAtkCursor, ddl_TSD_showAtkCursorInfo
                , ddl_TSD_showAtkEndurance, ddl_TSD_showAtkEnemyUnits, ddl_TSD_showAtkFriendlyUnits, ddl_TSD_showAtkHSI, ddl_TSD_showAtkInactiveZones, ddl_TSD_showAtkLines
                , ddl_TSD_showAtkObstacles, ddl_TSD_showAtkShotAt, ddl_TSD_showAtkTargets, ddl_TSD_showAtkWind, ddl_TSD_showNavAreas, ddl_TSD_showNavCtrlMeasures
                , ddl_TSD_showNavCursor, ddl_TSD_showNavCursorInfo, ddl_TSD_showNavEndurance, ddl_TSD_showNavEnemyUnits, ddl_TSD_showNavFriendlyUnits, ddl_TSD_showNavHSI
                , ddl_TSD_showNavInactiveZones, ddl_TSD_showNavLines, ddl_TSD_showNavObstacles, ddl_TSD_showNavTargets, ddl_TSD_showNavWind, ddl_TSD_showNavWPData, ddl_TSD_showOwnGhost
                , ddl_TSD_showOwnOwn, ddl_TSD_showOwnRings, ddl_TSD_showOwnTrnPt, ddl_TSD_showThreatACQ, ddl_TSD_showThreatASE, ddl_TSD_showThreatFCR, ddl_TSD_showThreatTargets
                , ddl_TSD_showThreatThreats, ddl_TSD_showThreatTrnPt, ddl_TSD_showThreatVisShade})
            {
                ddl.SelectedIndex = ComboItem<AH64TSDOptionData.EFilter>.FindValue(ddl, AH64TSDOptionData.EFilter.No_Change) ?? 0;
            }
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

            UpdateTuneDDLText(preset);
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

        #region Preset
        #region PresetVHF
        private void cbPresetVHF_Enable_CheckedChanged(object sender, EventArgs e)
        {
            bool status = (sender as CheckBox).Checked;
            GetSelectedPreset().ContainsVHFData = status;

            nudVHFFreq.Enabled = status;
            cbPresetVHFAllowRecvOnlyRange.Enabled = status;

            UpdateTuneDDLText(GetSelectedPresetIdent());
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

            UpdateTuneDDLText(GetSelectedPresetIdent());
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

            UpdateTuneDDLText(GetSelectedPresetIdent());
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
            GetSelectedPreset().ContainsFM2Data = status;

            nudFM2Freq.Enabled = status;
            ddlFM2CNV.Enabled = status;
            nudFM2Hopset.Enabled = status;

            UpdateTuneDDLText(GetSelectedPresetIdent());
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

            UpdateTuneDDLText(GetSelectedPresetIdent());
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
            bool primary = cbPresetNetPrimary.Checked && cbPresetNetPrimary.Enabled;
            bool team = cbPresetNetTeam.Checked;

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
            int primaryMemberCount = 0;
            bool containsCurrentID = false;
            bool currentIDIsPrimaryMember = false;
            foreach (ListBoxItem<AH64DataLinkMember> comboItem in lbPresetNetMembers.Items)
            {
                if (comboItem.Value.Primary)
                {
                    primaryMemberCount++;
                }
                if (comboItem.Value.SubscriberID == tbPresetNetID.Text)
                {
                    containsCurrentID = true;
                    currentIDIsPrimaryMember = comboItem.Value.Primary;
                }
            }
            cbPresetNetPrimary.Enabled = cbPresetNet_Enable.Checked
                && (primaryMemberCount < 7 || currentIDIsPrimaryMember);

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
        #endregion

        #region Tune

        private void UpdateTuneDDLText(AH64DTCData.EPreset preset)
        {
            foreach (var ddl in new List<ComboBox>()
    {
        ddlTuneVHFPreset, ddlTuneUHFPreset, ddlTuneFM1Preset, ddlTuneFM2Preset, ddlTuneHFPreset
    })
            {
                int? idx = ComboItem<AH64DTCData.EPreset>.FindValue(ddl, preset);
                if (!idx.HasValue)
                {
                    continue;
                }

                string newText = GetPresetName(preset) + " - ";
                AH64RadioPresetData presetData = data.GetAH64RadioPreset(preset);

                if (ddl.Name == ddlTuneVHFPreset.Name)
                {
                    newText += presetData.ContainsVHFData ? presetData.VHFFrequency.ToString() : "Current Frequency";
                }
                else if (ddl.Name == ddlTuneUHFPreset.Name)
                {
                    newText += presetData.ContainsUHFData ? presetData.UHFFrequency.ToString() : "Current Frequency";
                }
                else if (ddl.Name == ddlTuneFM1Preset.Name)
                {
                    newText += presetData.ContainsFM1Data ? presetData.FM1Frequency.ToString() : "Current Frequency";
                }
                else if (ddl.Name == ddlTuneFM2Preset.Name)
                {
                    newText += presetData.ContainsFM2Data ? presetData.FM2Frequency.ToString() : "Current Frequency";
                }
                else if (ddl.Name == ddlTuneHFPreset.Name)
                {
                    newText += presetData.ContainsHFData ? presetData.HFRXFrequency.ToString() : "Current Frequency";
                }

                (ddl.Items[idx.Value] as ComboItem<AH64DTCData.EPreset>).Text = newText;
                ddl.DisplayMember = "Value";
                ddl.DisplayMember = "Text";
            }
        }

        #region TuneVHF
        private void rbTuneVHFNoChange_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.VHFTuneSetting = AH64DTCData.ETuneSetting.No_Change;
                ddlTuneVHFPreset.Enabled = false;
                nudTuneVHFManualFreq.Enabled = false;
            }
        }

        private void rbTuneVHFPreset_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.VHFTuneSetting = AH64DTCData.ETuneSetting.Preset;
                ddlTuneVHFPreset.Enabled = true;
                data.VHFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneVHFPreset);
                nudTuneVHFManualFreq.Enabled = false;
            }
        }

        private void rbTuneVHFMan_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.VHFTuneSetting = AH64DTCData.ETuneSetting.Manual;
                ddlTuneVHFPreset.Enabled = false;
                nudTuneVHFManualFreq.Enabled = true;
                data.VHFManualFrequency = nudTuneVHFManualFreq.Value;
            }
        }

        private void ddlTuneVHFPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.VHFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneVHFPreset);
        }

        private void nudTuneVHFManualFreq_ValueChanged(object sender, EventArgs e)
        {
            data.VHFManualFrequency = nudTuneVHFManualFreq.Value;
        }
        #endregion

        #region TuneUHF
        private void rbTuneUHFNoChange_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.UHFTuneSetting = AH64DTCData.ETuneSetting.No_Change;
                ddlTuneUHFPreset.Enabled = false;
                nudTuneUHFManualFreq.Enabled = false;
            }
        }

        private void rbTuneUHFPreset_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.UHFTuneSetting = AH64DTCData.ETuneSetting.Preset;
                ddlTuneUHFPreset.Enabled = true;
                data.UHFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneUHFPreset);
                nudTuneUHFManualFreq.Enabled = false;
            }
        }

        private void rbTuneUHFMan_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.UHFTuneSetting = AH64DTCData.ETuneSetting.Manual;
                ddlTuneUHFPreset.Enabled = false;
                nudTuneUHFManualFreq.Enabled = true;
                data.UHFManualFrequency = nudTuneUHFManualFreq.Value;
            }
        }

        private void ddlTuneUHFPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.UHFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneUHFPreset);
        }

        private void nudTuneUHFManualFreq_ValueChanged(object sender, EventArgs e)
        {
            data.UHFManualFrequency = nudTuneUHFManualFreq.Value;
        }

        private void ddlTuneUHFGuardReciever_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.UHFGuardReceiver = ComboItem<AH64DTCData.EUHFGuardReceiver>.GetSelectedValue(ddlTuneUHFGuardReciever);
        }
        #endregion

        #region TuneFM1
        private void rbTuneFM1NoChange_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM1TuneSetting = AH64DTCData.ETuneSetting.No_Change;
                ddlTuneFM1Preset.Enabled = false;
                nudTuneFM1ManualFreq.Enabled = false;
            }
        }

        private void rbTuneFM1Preset_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM1TuneSetting = AH64DTCData.ETuneSetting.Preset;
                ddlTuneFM1Preset.Enabled = true;
                data.FM1TunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneFM1Preset);
                nudTuneFM1ManualFreq.Enabled = false;
            }
        }

        private void rbTuneFM1Man_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM1TuneSetting = AH64DTCData.ETuneSetting.Manual;
                ddlTuneFM1Preset.Enabled = false;
                nudTuneFM1ManualFreq.Enabled = true;
                data.FM1ManualFrequency = nudTuneFM1ManualFreq.Value;
            }
        }

        private void ddlTuneFM1Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.FM1TunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneFM1Preset);
        }

        private void nudTuneFM1ManualFreq_ValueChanged(object sender, EventArgs e)
        {
            data.FM1ManualFrequency = nudTuneFM1ManualFreq.Value;
        }
        #endregion

        #region TuneFM2
        private void rbTuneFM2NoChange_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM2TuneSetting = AH64DTCData.ETuneSetting.No_Change;
                ddlTuneFM2Preset.Enabled = false;
                nudTuneFM2ManualFreq.Enabled = false;
            }
        }

        private void rbTuneFM2Preset_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM2TuneSetting = AH64DTCData.ETuneSetting.Preset;
                ddlTuneFM2Preset.Enabled = true;
                data.FM2TunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneFM2Preset);
                nudTuneFM2ManualFreq.Enabled = false;
            }
        }

        private void rbTuneFM2Man_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.FM2TuneSetting = AH64DTCData.ETuneSetting.Manual;
                ddlTuneFM2Preset.Enabled = false;
                nudTuneFM2ManualFreq.Enabled = true;
                data.FM2ManualFrequency = nudTuneFM2ManualFreq.Value;
            }
        }

        private void ddlTuneFM2Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.FM2TunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneFM2Preset);
        }

        private void nudTuneFM2ManualFreq_ValueChanged(object sender, EventArgs e)
        {
            data.FM2ManualFrequency = nudTuneFM2ManualFreq.Value;
        }
        #endregion

        #region TuneHF
        private void rbTuneHFNoChange_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.HFTuneSetting = AH64DTCData.ETuneSetting.No_Change;
                ddlTuneHFPreset.Enabled = false;
                nudTuneHFRXManualFreq.Enabled = false;
            }
        }

        private void rbTuneHFPreset_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.HFTuneSetting = AH64DTCData.ETuneSetting.Preset;
                ddlTuneHFPreset.Enabled = true;
                data.HFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneHFPreset);
                nudTuneHFRXManualFreq.Enabled = false;
            }
        }

        private void rbTuneHFMan_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                data.HFTuneSetting = AH64DTCData.ETuneSetting.Manual;
                ddlTuneHFPreset.Enabled = false;
                nudTuneHFRXManualFreq.Enabled = true;
                data.HFRXManualFrequency = nudTuneHFRXManualFreq.Value;
            }
        }

        private void ddlTuneHFPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.HFTunePreset = ComboItem<AH64DTCData.EPreset>.GetSelectedValue(ddlTuneHFPreset);
        }

        private void nudTuneHFRXManualFreq_ValueChanged(object sender, EventArgs e)
        {
            data.HFRXManualFrequency = nudTuneHFRXManualFreq.Value;
        }
        #endregion

        #endregion

        #region OwnshipDL
        private void tbOwnshipCallsign_TextChanged(object objsender, EventArgs e)
        {
            TextBox sender = objsender as TextBox;
            string text = sender.Text.ToUpper();
            toolTip.Hide(this);
            sender.BackColor = default;

            if (string.IsNullOrEmpty(text))
            {
                data.OwnshipCallsign = null;
                return;
            }

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
            sender.BackColor = default;

            if (string.IsNullOrEmpty(text))
            {
                data.OwnshipSubscriberID = null;
                return;
            }

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

        private void ddlXPNDRAntenna_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.IFFAntenna = ComboItem<AH64DTCData.EIFFAntenna>.GetSelectedValue(sender as ComboBox);
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

        #region WPN
        private void ddlMslChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = sender as ComboBox;
            var laserChannel = ComboItem<AH64DTCData.ELaserChannel>.GetSelectedValue(ddl);
            AH64DTCData.EMissileChannel missileChannel =
                ddl.Name == ddlMslChannel1.Name ? AH64DTCData.EMissileChannel.CH_1
                : ddl.Name == ddlMslChannel2.Name ? AH64DTCData.EMissileChannel.CH_2
                : ddl.Name == ddlMslChannel3.Name ? AH64DTCData.EMissileChannel.CH_3
                : ddl.Name == ddlMslChannel4.Name ? AH64DTCData.EMissileChannel.CH_4
                : throw new ArgumentException("Sender must be a missile channel combo box", nameof(sender));
            data.SetMissileChannel(missileChannel, laserChannel);
        }

        private void ddlMslChannelPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            AH64DTCData.EMissileChannel value = ComboItem<AH64DTCData.EMissileChannel>.GetSelectedValue(sender as ComboBox);
            data.MissilePriorityChannel = value;
            if (value == AH64DTCData.EMissileChannel.None)
            {
                ddlMslChannelAlternate.SelectedIndex = ComboItem<AH64DTCData.EMissileChannel>.FindValue(ddlMslChannelAlternate, AH64DTCData.EMissileChannel.None) ?? 0;
                ddlMslChannelAlternate.Enabled = false;
            }
            else
            {
                ddlMslChannelAlternate.Enabled = true;
            }
            
            
        }

        private void ddlMslChannelAlternate_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.MissileAlternateChannel = ComboItem<AH64DTCData.EMissileChannel>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlGunBurstLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.GunBurstLength = ComboItem<AH64DTCData.EGunBurstLength>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlRktQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.RocketQuantity = ComboItem<AH64DTCData.ERocketQuantity>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlLaserLRST_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.LrfdLaserChannel = ComboItem<AH64DTCData.ELaserChannel>.GetSelectedValue(sender as ComboBox);
        }

        private void ddlLaserLST_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.LstLaserChannel = ComboItem<AH64DTCData.ELaserChannel>.GetSelectedValue(sender as ComboBox);
        }

        private void tb_laserCode_KeyPress(object objsender, KeyPressEventArgs e)
        {
            var sender = objsender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // If the pressed key is not a digit or backspace, suppress it
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '9' || e.KeyChar == '0') // 0 and 9 are never a valid digit for a laser code
            {
                e.Handled = true;
                return;
            }
            if (sender.Text.Length == 0)
            {
                if (e.KeyChar >= '6' || e.KeyChar == '3') // first digit must be 1,2,4 or 5
                {
                    e.Handled = true;
                    return;
                }
            }
            if (sender.Text.Length == 1 && sender.Text[0] == 1)
            {
                if (e.KeyChar == '8') // 8 is invalid if the first character is a 1
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void tb_laserCode_TextChanged(object objsender, EventArgs e)
        {
            var sender = objsender as TextBox;
            toolTip.Hide(this);
            sender.BackColor = default;
            int? laserCode = null;

            if (!string.IsNullOrEmpty(sender.Text))
            {
                try
                {
                    laserCode = int.Parse(sender.Text);
                }
                catch (Exception ex)
                {
                    sender.BackColor = MainForm.ERROR_COLOR;
                    toolTip.Show(ex.Message, sender, 0, sender.Width, 5000);
                }

                if (!AH64LaserCode.IsLaserCodeValid(laserCode.Value))
                {
                    sender.BackColor = MainForm.ERROR_COLOR;
                    toolTip.Show(AH64LaserCode.LaserCodeError, sender, 0, sender.Width, 5000);
                    return;
                }
            }
            
            string enumValueAsString = (sender as TextBox).Name.Last().ToString();
            var laserChannel = (AH64DTCData.ELaserChannel)Enum.Parse(typeof(AH64DTCData.ELaserChannel), enumValueAsString);
            data.SetLaserCodeFrequency(laserChannel, laserCode.HasValue ? new AH64LaserCode(laserCode.Value) : null);
        }

        private void cbManRange_CheckedChanged(object objsender, EventArgs e)
        {
            var sender = objsender as CheckBox;
            nudManRange.Enabled = sender.Checked;
            data.ManRange = sender.Checked ? (int?)nudManRange.Value : null;
        }

        private void nudManRange_ValueChanged(object sender, EventArgs e)
        {
            data.ManRange = cbManRange.Checked ? (int?)(sender as NumericUpDown).Value : null;
        }
        #endregion

        #region TSD
        private void btn_getMapSettings_Click(object sender, EventArgs e)
        {
            var newTsdData = AH64TSDOptionData.ReadFromAC(IsPilot);
            if (newTsdData != null)
            {
                data.TSDData = newTsdData;
            }
            RefreshControls();
        }

        private void ddl_TSD_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.MapType = ComboItem<AH64TSDOptionData.EMapType>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_colorBand_SelectedIndexChanged(object sender, EventArgs e)
        {

            data.TSDData.ColorBand = ComboItem<AH64TSDOptionData.EColorBand>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_contours_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Contours = ComboItem<AH64TSDOptionData.EContours>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_ffd_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.ffd = ComboItem<AH64TSDOptionData.EFfd>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_gray_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Gray = ComboItem<AH64TSDOptionData.EGray>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_satLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.SatLevel = ComboItem<AH64TSDOptionData.ESatLevel>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_center_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Center = ComboItem<AH64TSDOptionData.ECenter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_orient_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Orientation = ComboItem<AH64TSDOptionData.EOrientation>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Grid = ComboItem<AH64TSDOptionData.EGrid>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_phase_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.Phase = ComboItem<AH64TSDOptionData.EPhase>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavWPData_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavWpData = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavInactiveZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavInactiveZones = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavObstacles_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavObstacles = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavCursor_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavCursor = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavCursorInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavCursorInfo = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavHSI_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavHSI = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavEndurance_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavEndurance = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavWind_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavWind = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavCtrlMeasures_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavCtrlMeasures = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavFriendlyUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavFriendlyUnits = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavEnemyUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavEnemyUnits = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavTargets = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavLines = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showNavAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.NavAreas = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkCurrentRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkCurrentRoute = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkInactiveZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkInactiveZones = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkObstacles_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkFCRObstacles = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkCursor_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkCursor = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkCursorInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkCursorInfo = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkHSI_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkHSI = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkEndurance_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkEndurance = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkWind_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkWind = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkCtrlMeasures_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkCtrlMeasures = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkFriendlyUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkFriendlyUnits = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkEnemyUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkEnemyUnits = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkTargets = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkLines = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkAreas = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showAtkShotAt_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AtkShotAt = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatVis_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.ThreatVis = ComboItem<AH64TSDOptionData.EVis>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatVisShade_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisShade = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatASE_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.AseThreats = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatACQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisThreatACQ = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatTrnPt_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisThreatTrnPt = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatFCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisThreatFCR = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatThreats_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisThreatThreats = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showThreatTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.visThreatTargets = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showOwnOwn_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisOwnOwn = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showOwnTrnPt_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisOwnTrnPt = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showOwnGhost_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisOwnGhost = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }

        private void ddl_TSD_showOwnRings_SelectedIndexChanged(object sender, EventArgs e)
        {
            data.TSDData.VisOwnRings = ComboItem<AH64TSDOptionData.EFilter>.GetSelectedValue(sender as ComboBox);
        }
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
            ShowReadOnly = true,
            CheckFileExists = true,
        };

        private readonly SaveFileDialog sfd = new SaveFileDialog()
        {
            Title = "Open AH64 DTC Data File",
            AddExtension = true,
            DefaultExt = "json",
            Filter = "JSON files (*.json)|*.json|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            FileName = "AH64_DTC.json",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            CheckFileExists = true,
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
            sfd.FileName = fi.Name;
            sfd.InitialDirectory = fi.DirectoryName;
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

            DialogResult result = sfd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            string filePath = sfd.FileName;
            FileInfo fi = new FileInfo(filePath);
            ofd.FileName = fi.Name;
            ofd.InitialDirectory = fi.DirectoryName;
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
