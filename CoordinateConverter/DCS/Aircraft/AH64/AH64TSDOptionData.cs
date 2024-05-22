using CoordinateConverter.DCS.Communication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents the TSD Option Data for an AH64
    /// </summary>
    public class AH64TSDOptionData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64TSDOptionData"/> class.
        /// </summary>
        [JsonConstructor]
        public AH64TSDOptionData()
        {
        }

        #region Map

        #region General
        /// <summary>
        /// The type of map to use on the TSD
        /// </summary>
        public enum EMapType
        {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Chart,
            Digital,
            Satellite,
            Stick
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the type of the map.
        /// </summary>
        /// <value>
        /// The type of the map.
        /// </value>
        public EMapType MapType { get; set; } = EMapType.No_Change;

        /// <summary>
        /// Whether the map is centered or depressed
        /// </summary>
        public enum ECenter
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Center,
            Depressed
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets whether the map is centered or depressed.
        /// </summary>
        /// <value>
        /// The center setting.
        /// </value>
        public ECenter Center { get; set; } = ECenter.No_Change;

        /// <summary>
        /// The orientation of the TSD map
        /// </summary>
        public enum EOrientation
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            HeadingUp,
            TrackUp,
            NorthUp
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }
        

        /// <summary>
        /// Gets or sets the orientation of the TSD map..
        /// </summary>
        /// <value>
        /// The orientation.
        /// </value>
        public EOrientation Orientation { get; set; } = EOrientation.No_Change;

        /// <summary>
        /// Phase of the TSD
        /// </summary>
        public enum EPhase
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Navigation,
            Attack
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// The phase.
        /// </value>
        public EPhase Phase { get; set; } = EPhase.No_Change;

        /// <summary>
        /// Whether or not to show the grid
        /// </summary>
        public enum EGrid
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Grid_Normal,
            Grid_None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the grid setting.
        /// </summary>
        /// <value>
        /// The grid setting.
        /// </value>
        public EGrid Grid { get; set; } = EGrid.No_Change;

        #endregion

        #region ChartMap
        /// <summary>
        /// The scale of the TSD chart map
        /// </summary>
        public enum EChartScale
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Scale_5M,
            Scale_2M,
            Scale_1M,
            Scale_500k,
            Scale_250k,
            Scale_100k,
            Scale_50k,
            Scale_12k5
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the chart scale for each zoom level.
        /// </summary>
        /// <value>
        /// The chart scale for each zoom level.
        /// </value>
        public Dictionary<int, EChartScale> ChartScale { get; set; } = null;


        #endregion

        #region DigitalMap
        /// <summary>
        /// color band for digital map
        /// </summary>
        public enum EColorBand
        {
            #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            AC,
            Elevation,
            None
            #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }


        /// <summary>
        /// Gets or sets the color band.
        /// </summary>
        /// <value>
        /// The color band.
        /// </value>
        public EColorBand ColorBand { get; set; } = AH64TSDOptionData.EColorBand.No_Change;

        /// <summary>
        /// The contour setting for the digital map
        /// </summary>
        public enum EContours
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Contours_1000,
            Contours_500,
            Contours_200,
            Contours_100,
            Contours_50,
            Contours_None,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the contours.
        /// </summary>
        /// <value>
        /// The contours.
        /// </value>
        public EContours Contours { get; set; } = EContours.No_Change;

        /// <summary>
        /// Feature Foundation Database settings
        /// </summary>
        public enum EFfd
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            FFD_Areas,
            FFD_Lines,
            FFD_Both,
            FFD_None
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the Feature Foundation Database setting.
        /// </summary>
        /// <value>
        /// The FFD setting.
        /// </value>
        public EFfd ffd { get; set; } = EFfd.No_Change;

        /// <summary>
        /// Whether to show the digital map in grayscale or green/grayscale
        /// </summary>
        public enum EGray
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Gray,
            Green_and_gray
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }


        /// <summary>
        /// Gets or sets whether to show the digital map in grayscale or green/grayscale
        /// </summary>
        /// <value>
        /// The grayscale setting.
        /// </value>
        public EGray Gray { get; set; } = EGray.No_Change;

        #endregion

        #region Satellite        
        /// <summary>
        /// Level of detail for the satellite map
        /// </summary>
        public enum ESatLevel
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            L5,
            L10
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the level of detail for the satellite map.
        /// </summary>
        /// <value>
        /// The sat level.
        /// </value>
        public ESatLevel SatLevel { get; set; } = ESatLevel.No_Change;

        #endregion

        #endregion

        #region Show        
        /// <summary>
        /// A filter setting for the TSD show options
        /// </summary>
        public enum EFilter
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Show,
            Hide
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        #region NavPhase        
        /// <summary>
        /// Wheter to show or hide the waypoint data window in the navigation phase
        /// </summary>
        /// <value>
        /// The navigation phase waypoint data window show/hide setting.
        /// </value>
        public EFilter NavWpData { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide inactive zones in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase inactive zones show/hide setting.
        /// </value>
        public EFilter NavInactiveZones { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide obstacles in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase obstacles show/hide setting.
        /// </value>
        public EFilter NavObstacles { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the opposite crewmember cursor in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase opposite crewmember cursor show/hide setting.
        /// </value>
        public EFilter NavCursor { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the cursor information in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase cursor information show/hide setting.
        /// </value>
        public EFilter NavCursorInfo { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the Horizontal Situation Indicator (HSI) in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase HSI show/hide setting.
        /// </value>
        public EFilter NavHSI { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide endurance information in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase endurance information show/hide setting.
        /// </value>
        public EFilter NavEndurance { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide wind information in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase wind information show/hide setting.
        /// </value>
        public EFilter NavWind { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide control measures in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase control measures show/hide setting.
        /// </value>
        public EFilter NavCtrlMeasures { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide friendly units in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase friendly units show/hide setting.
        /// </value>
        public EFilter NavFriendlyUnits { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide enemy units in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase enemy units show/hide setting.
        /// </value>
        public EFilter NavEnemyUnits { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide targets in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase targets show/hide setting.
        /// </value>
        public EFilter NavTargets { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide navigation lines in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase lines show/hide setting.
        /// </value>
        public EFilter NavLines { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide navigation areas in the navigation phase.
        /// </summary>
        /// <value>
        /// The navigation phase areas show/hide setting.
        /// </value>
        public EFilter NavAreas { get; set; } = EFilter.No_Change;
        #endregion

        #region AtkPhase
        /// <summary>
        /// Whether to show or hide the current route in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase current route show/hide setting.
        /// </value>
        public EFilter AtkCurrentRoute { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide inactive zones in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase inactive zones show/hide setting.
        /// </value>
        public EFilter AtkInactiveZones { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide obstacles and secondary Fire Control Radar (FCR) targets in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase FCR/obstacles show/hide setting.
        /// </value>
        public EFilter AtkFCRObstacles { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the opposite crewmember cursor in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase opposite crewmember cursor show/hide setting.
        /// </value>
        public EFilter AtkCursor { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the cursor information in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase cursor information show/hide setting.
        /// </value>
        public EFilter AtkCursorInfo { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide the Horizontal Situation Indicator (HSI) in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase HSI show/hide setting.
        /// </value>
        public EFilter AtkHSI { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide endurance information in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase endurance information show/hide setting.
        /// </value>
        public EFilter AtkEndurance { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide wind information in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase wind information show/hide setting.
        /// </value>
        public EFilter AtkWind { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide control measures in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase control measures show/hide setting.
        /// </value>
        public EFilter AtkCtrlMeasures { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide friendly units in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase friendly units show/hide setting.
        /// </value>
        public EFilter AtkFriendlyUnits { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide enemy units in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase enemy units show/hide setting.
        /// </value>
        public EFilter AtkEnemyUnits { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide targets in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase targets show/hide setting.
        /// </value>
        public EFilter AtkTargets { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide navigation lines in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase lines show/hide setting.
        /// </value>
        public EFilter AtkLines { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide navigation areas in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase areas show/hide setting.
        /// </value>
        public EFilter AtkAreas { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show or hide "shot at" symbols in the attack phase.
        /// </summary>
        /// <value>
        /// The attack phase "shot at" symbols show/hide setting.
        /// </value>
        public EFilter AtkShotAt { get; set; } = EFilter.No_Change;
        #endregion

        #region Threats        
        /// <summary>
        /// Threat show option
        /// </summary>
        public enum EVis
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
            No_Change,
            Own,
            Threat
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        }

        /// <summary>
        /// Gets or sets the threat vis setting.
        /// </summary>
        /// <value>
        /// The threat vis setting.
        /// </value>
        public EVis ThreatVis { get; set; } = EVis.No_Change;

        /// <summary>
        /// Wheter to show ASE threats
        /// </summary>
        /// <value>
        /// The ase threats filter setting.
        /// </value>
        public EFilter AseThreats { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade in areas
        /// </summary>
        /// <value>
        /// The vis shade setting.
        /// </value>
        public EFilter VisShade { get; set; } = EFilter.No_Change;

        #region VisThreat        
        /// <summary>
        /// Whether to shade or show the threat rings for the acquisition source
        /// </summary>
        /// <value>
        /// The ACQ shade/ring filter setting.
        /// </value>
        public EFilter VisThreatACQ { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for terrain points
        /// </summary>
        /// <value>
        /// The TrnPt shade/ring filter setting.
        /// </value>
        public EFilter VisThreatTrnPt { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for the FCR contacts
        /// </summary>
        /// <value>
        /// The FCR shade/ring filter setting.
        /// </value>
        public EFilter VisThreatFCR { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for pre planned threats
        /// </summary>
        /// <value>
        /// The Threats shade/ring filter setting.
        /// </value>
        public EFilter VisThreatThreats { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for generic target points
        /// </summary>
        /// <value>
        /// The Targets shade/ring filter setting.
        /// </value>
        public EFilter visThreatTargets { get; set; } = EFilter.No_Change;
        #endregion

        #region VisOwn
        /// <summary>
        /// Whether to shade or show the threat rings for ownship
        /// </summary>
        /// <value>
        /// The Own shade/ring filter setting.
        /// </value>
        public EFilter VisOwnOwn { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for terrain points in the Own vis setting
        /// </summary>
        /// <value>
        /// The TrnPt shade/ring filter setting for own.
        /// </value>
        public EFilter VisOwnTrnPt { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to shade or show the threat rings for ghost ownship, when panning/freezing
        /// </summary>
        /// <value>
        /// The Ghost shade/ring filter setting.
        /// </value>
        public EFilter VisOwnGhost { get; set; } = EFilter.No_Change;

        /// <summary>
        /// Whether to show rings when own is selected
        /// </summary>
        /// <value>
        /// The ring ring filter setting for own.
        /// </value>
        public EFilter VisOwnRings { get; set; } = EFilter.No_Change;
        #endregion
        #endregion

        #endregion

        #region Info        
        /// <summary>
        /// Gets a value indicating whether this instance has data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data; otherwise, <c>false</c>.
        /// </value>
        public bool HasData
        {
            get
            {
                return HasMapPageData || HasNavPhaseData || HasAtkPhaseData;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has dig map data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has dig map data; otherwise, <c>false</c>.
        /// </value>
        public bool HasDigMapData
        {
            get
            {
                return ColorBand != EColorBand.No_Change
                    || Contours != EContours.No_Change
                    || ffd != EFfd.No_Change
                    || Gray != EGray.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has chart map data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has chart map data; otherwise, <c>false</c>.
        /// </value>
        public bool HasChartMapData
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has sat map data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has sat map data; otherwise, <c>false</c>.
        /// </value>
        public bool HasSatMapData
        {
            get
            {
                return SatLevel != ESatLevel.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has generic map page data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has generic map page data; otherwise, <c>false</c>.
        /// </value>
        public bool HasGenericMapPageData
        {
            get
            {
                return Orientation != EOrientation.No_Change
                    || Grid != EGrid.No_Change
                    || MapType != EMapType.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has map page data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has map page data; otherwise, <c>false</c>.
        /// </value>
        public bool HasMapPageData
        {
            get
            {
                return HasDigMapData || HasChartMapData || HasSatMapData || HasGenericMapPageData;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has nav phase data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has nav phase data; otherwise, <c>false</c>.
        /// </value>
        public bool HasNavPhaseData
        {
            get
            {
                return HasNavCoordData
                    || NavWpData != EFilter.No_Change
                    || NavInactiveZones != EFilter.No_Change
                    || NavObstacles != EFilter.No_Change
                    || NavCursor != EFilter.No_Change
                    || NavCursorInfo != EFilter.No_Change
                    || NavHSI != EFilter.No_Change
                    || NavEndurance != EFilter.No_Change
                    || NavWind != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has nav coord data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has nav coord data; otherwise, <c>false</c>.
        /// </value>
        public bool HasNavCoordData
        {
            get
            {
                return NavCtrlMeasures != EFilter.No_Change
                    || NavFriendlyUnits != EFilter.No_Change
                    || NavEnemyUnits != EFilter.No_Change
                    || NavTargets != EFilter.No_Change
                    || NavLines != EFilter.No_Change
                    || NavAreas != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has atk phase data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has atk phase data; otherwise, <c>false</c>.
        /// </value>
        public bool HasAtkPhaseData
        {
            get
            {
                return HasAtkCoordData
                    || AtkCurrentRoute != EFilter.No_Change
                    || AtkInactiveZones != EFilter.No_Change
                    || AtkFCRObstacles != EFilter.No_Change
                    || AtkCursor != EFilter.No_Change
                    || AtkCursorInfo != EFilter.No_Change
                    || AtkHSI != EFilter.No_Change
                    || AtkEndurance != EFilter.No_Change
                    || AtkWind != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has atk coord data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has atk coord data; otherwise, <c>false</c>.
        /// </value>
        public bool HasAtkCoordData
        {
            get
            {
                return AtkCtrlMeasures != EFilter.No_Change
                    || AtkFriendlyUnits != EFilter.No_Change
                    || AtkEnemyUnits != EFilter.No_Change
                    || AtkTargets != EFilter.No_Change
                    || AtkLines != EFilter.No_Change
                    || AtkAreas != EFilter.No_Change
                    || AtkShotAt != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has generic vis data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has generic vis data; otherwise, <c>false</c>.
        /// </value>
        public bool HasGenericVisData
        {
            get
            {
                return ThreatVis != EVis.No_Change
                    || AseThreats != EFilter.No_Change
                    || VisShade != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has threat vis data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has threat vis data; otherwise, <c>false</c>.
        /// </value>
        public bool HasThreatVisData
        {
            get
            {
                return VisThreatACQ != EFilter.No_Change
                    || VisThreatFCR != EFilter.No_Change
                    || visThreatTargets != EFilter.No_Change
                    || VisThreatThreats != EFilter.No_Change
                    || VisThreatTrnPt != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has own vis data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has own vis data; otherwise, <c>false</c>.
        /// </value>
        public bool HasOwnVisData
        {
            get
            {
                return VisOwnOwn != EFilter.No_Change
                    || VisOwnGhost != EFilter.No_Change
                    || VisOwnRings != EFilter.No_Change
                    || VisOwnTrnPt != EFilter.No_Change;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has vis data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has vis data; otherwise, <c>false</c>.
        /// </value>
        public bool HasVisData
        {
            get
            {
                return HasGenericVisData
                    || HasThreatVisData
                    || HasOwnVisData;
            }
        }
        #endregion

        #region Generation

        private enum EScreens
        {
            TSD_MAP,
            TSD_MAP_DIG,
            TSD_MAP_SAT,
            TSD_MAP_CHART,
            TSD_SHOW_NAV,
            TSD_SHOW_COORD_NAV,
            TSD_SHOW_ATK,
            TSD_SHOW_COORD_ATK,
            TSD_SHOW_THREAT
        }

        static private void SleepForCommands(List<DCSCommand> commands)
        {
            int totalDelay = (int)(commands.Sum(c => c.Delay) * 1.2);
            if (totalDelay > 0)
            {
                Thread.Sleep(totalDelay);
            }
        }

        /// <summary>
        /// Reads the current map settings from DCS.
        /// </summary>
        /// <returns>The current map settings</returns>
        static public AH64TSDOptionData ReadFromAC(bool isPilot)
        {
            int mfd = (int)(isPilot ? AH64.EDeviceCode.PLT_RMFD : AH64.EDeviceCode.CPG_RMFD);
            int displayToRead = (int)(isPilot ? AH64.EDisplayCodes.PLT_MFD_Right : AH64.EDisplayCodes.CPG_MFD_Right);

            List<DCSCommand> commands = null;
            DCSMessage message = null;

            Dictionary<EScreens, List<DCSCommand>> navigations = new Dictionary<EScreens, List<DCSCommand>>()
            {
                { EScreens.TSD_MAP,
                    new List<DCSCommand>() {
                    // TSD -> Map
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_TSD),
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B4),
                } },
                { EScreens.TSD_MAP_DIG, new List<DCSCommand>() {
                    // Type -> Digital
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2),
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2),
                } },
                { EScreens.TSD_MAP_SAT, new List<DCSCommand>()
                {
                    // Type -> Sat
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2),
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4),
                } },
                { EScreens.TSD_MAP_CHART, new List<DCSCommand>()
                {
                    // Type -> Chart
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2),
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3),
                } },
                { EScreens.TSD_SHOW_NAV, new List<DCSCommand>()
                {
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T3)
                } },
                { EScreens.TSD_SHOW_COORD_NAV, new List<DCSCommand>()
                {
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6)
                } },
                { EScreens.TSD_SHOW_ATK, new List<DCSCommand>()
                {
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6),
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2)
                } },
                { EScreens.TSD_SHOW_COORD_ATK, new List<DCSCommand>()
                {
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6)
                } },
                { EScreens.TSD_SHOW_THREAT, new List<DCSCommand>()
                {
                    new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T5)
                } }
            };

            AH64TSDOptionData result = new AH64TSDOptionData();


            // loop over navigations dictionary
            foreach (KeyValuePair<EScreens, List<DCSCommand>> kvp in navigations.OrderBy(x => x.Key))
            {
                // execute the navigation commands
                commands = kvp.Value;
                message = new DCSMessage() { Commands = commands };
                message = DCSConnection.SendRequest(message);

                if (message == null)
                {
                    return null;
                }
                SleepForCommands(commands);

                // Read the display
                message = new DCSMessage()
                {
                    GetCockpitDisplayData = new List<int> { displayToRead }
                };
                message = DCSConnection.SendRequest(message);
                if (message == null)
                {
                    return null;
                }

                Dictionary<string, string> displayData = message.CockpitDisplayData[displayToRead];
                switch (kvp.Key)
                {
                    case EScreens.TSD_MAP:
                        result.Phase = displayData["PB17_31"] == "NAV" ? EPhase.Navigation : EPhase.Attack;
                        result.MapType = displayData["PB23_15"] == "DIG" ? EMapType.Digital
                                        : displayData["PB23_15"] == "CHART" ? EMapType.Chart
                                        : displayData["PB23_15"] == "SAT" ? EMapType.Satellite
                                        : EMapType.Stick;
                        result.Center = displayData.ContainsKey("PB9_1_b") ? ECenter.Center : ECenter.Depressed;
                        result.Orientation = displayData["PB11_7"] == "TRK-UP" ? EOrientation.TrackUp
                                        : (displayData["PB11_7"] == "HDG-UP" ? EOrientation.HeadingUp
                                        : EOrientation.NorthUp);
                        result.Grid = displayData.ContainsKey("PB5_23_b") ? EGrid.Grid_Normal : EGrid.Grid_None;
                        break;
                    case EScreens.TSD_MAP_DIG:
                        result.Gray = displayData.ContainsKey("PB4_21_b") ? EGray.Gray : EGray.Green_and_gray;
                        result.ColorBand = displayData["PB21_31"] == "NONE" ? EColorBand.None
                                        : displayData["PB21_31"] == "ELEV" ? EColorBand.Elevation
                                        : EColorBand.AC;
                        result.Contours = displayData["PB20_35"] == "NONE" ? EContours.Contours_None
                                        : displayData["PB20_35"] == "50" ? EContours.Contours_50
                                        : displayData["PB20_35"] == "100" ? EContours.Contours_100
                                        : displayData["PB20_35"] == "200" ? EContours.Contours_200
                                        : displayData["PB20_35"] == "500" ? EContours.Contours_500
                                        : EContours.Contours_1000;
                        result.ffd = displayData["PB19_39"] == "NONE" ? EFfd.FFD_None
                                        : displayData["PB19_39"] == "BOTH" ? EFfd.FFD_Both
                                        : displayData["PB19_39"] == "LINES" ? EFfd.FFD_Lines
                                        : EFfd.FFD_Areas;
                        break;
                    case EScreens.TSD_MAP_SAT:
                        result.SatLevel = displayData["PB22_27"] == "5M" ? ESatLevel.L5 : ESatLevel.L10;
                        break;
                    case EScreens.TSD_MAP_CHART:
                        // result.ChartScale = displayData["PB22_23"] == "" ...;
                        
                        commands = new List<DCSCommand>();

                        // Switch back to original map type
                        if (result.MapType != EMapType.Chart)
                        {
                            int btnForMapType = (int)(result.MapType == EMapType.Digital ? AH64.EKeyCode.MFD_L2
                                                : result.MapType == EMapType.Satellite ? AH64.EKeyCode.MFD_L4
                                                : AH64.EKeyCode.MFD_L5);
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                            commands.Add(new DCSCommand(mfd, btnForMapType));
                        }

                        // Switch to NAV phase
                        if (result.Phase == EPhase.Attack)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                        }

                        if (commands.Count > 0)
                        {
                            message = DCSConnection.SendRequest(new DCSMessage()
                            {
                                Commands = commands
                            });
                            if (message == null)
                            {
                                return null;
                            }
                            SleepForCommands(commands);
                        }
                        break;
                    case EScreens.TSD_SHOW_NAV:
                        result.NavWpData = displayData.ContainsKey("PB23_9_b") ? EFilter.Show : EFilter.Hide;
                        result.NavInactiveZones = displayData.ContainsKey("PB22_13_b") ? EFilter.Show : EFilter.Hide;
                        result.NavObstacles = displayData.ContainsKey("PB21_29_b") ? EFilter.Show : EFilter.Hide;
                        result.NavCursor = displayData.ContainsKey("PB20_15_b") ? EFilter.Show : EFilter.Hide;
                        result.NavCursorInfo = displayData.ContainsKey("PB19_17_b") ? EFilter.Show : EFilter.Hide;
                        result.NavHSI = displayData.ContainsKey("PB10_23_b") ? EFilter.Show : EFilter.Hide;
                        result.NavEndurance = displayData.ContainsKey("PB11_25_b") ? EFilter.Show : EFilter.Hide;
                        result.NavWind = displayData.ContainsKey("PB12_27_b") ? EFilter.Show : EFilter.Hide;
                        break;
                    case EScreens.TSD_SHOW_COORD_NAV:
                        result.NavCtrlMeasures = displayData.ContainsKey("PB23_11_b") ? EFilter.Show : EFilter.Hide;
                        result.NavFriendlyUnits = displayData.ContainsKey("PB22_13_b") ? EFilter.Show : EFilter.Hide;
                        result.NavEnemyUnits = displayData.ContainsKey("PB21_15_b") ? EFilter.Show : EFilter.Hide;
                        result.NavTargets = displayData.ContainsKey("PB20_17_b") ? EFilter.Show : EFilter.Hide;
                        result.NavLines = displayData.ContainsKey("PB7_19_b") ? EFilter.Show : EFilter.Hide;
                        result.NavAreas = displayData.ContainsKey("PB8_21_b") ? EFilter.Show : EFilter.Hide;
                        break;
                    case EScreens.TSD_SHOW_ATK:
                        result.AtkCurrentRoute = displayData.ContainsKey("PB23_11_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkInactiveZones = displayData.ContainsKey("PB22_13_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkFCRObstacles = displayData.ContainsKey("PB21_31_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkCursor = displayData.ContainsKey("PB20_15_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkCursorInfo = displayData.ContainsKey("PB19_17_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkHSI = displayData.ContainsKey("PB10_23_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkEndurance = displayData.ContainsKey("PB11_25_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkWind = displayData.ContainsKey("PB12_27_b") ? EFilter.Show : EFilter.Hide;
                        break;
                    case EScreens.TSD_SHOW_COORD_ATK:
                        result.AtkCtrlMeasures = displayData.ContainsKey("PB23_11_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkFriendlyUnits = displayData.ContainsKey("PB22_13_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkEnemyUnits = displayData.ContainsKey("PB21_15_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkTargets = displayData.ContainsKey("PB20_17_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkLines = displayData.ContainsKey("PB7_19_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkAreas = displayData.ContainsKey("PB8_21_b") ? EFilter.Show : EFilter.Hide;
                        result.AtkShotAt = displayData.ContainsKey("PB9_23_b") ? EFilter.Show : EFilter.Hide;
                        break;
                    case EScreens.TSD_SHOW_THREAT:
                        result.ThreatVis = displayData["PB7_25"] == "THRT" ? EVis.Threat : EVis.Own;
                        result.AseThreats = displayData.ContainsKey("PB23_15_b") ? EFilter.Show : EFilter.Hide;
                        result.VisShade = displayData.ContainsKey("PB19_21_b") ? EFilter.Show : EFilter.Hide;

                        // switch to OWN
                        if (result.ThreatVis == EVis.Threat)
                        {
                            commands = new List<DCSCommand>()
                            {
                                new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1)
                            };
                            message = DCSConnection.SendRequest(new DCSMessage()
                            {
                                Commands = commands
                            });
                            if (message == null)
                            {
                                return null;
                            }
                            SleepForCommands(commands);

                            message = DCSConnection.SendRequest(new DCSMessage()
                            {
                                GetCockpitDisplayData = new List<int> { displayToRead }
                            });
                            
                            if (message == null
                                || message.CockpitDisplayData == null
                                || !message.CockpitDisplayData.ContainsKey(displayToRead)
                            )
                            {
                                return null;
                            }

                            displayData = message.CockpitDisplayData[displayToRead];
                        }

                        // we are now in OWN, read data
                        result.VisOwnOwn = displayData.ContainsKey("PB8_27_b") ? EFilter.Show : EFilter.Hide;
                        result.VisOwnTrnPt = displayData.ContainsKey("PB9_29_b") ? EFilter.Show : EFilter.Hide;
                        result.VisOwnGhost = displayData.ContainsKey("PB10_31_b") ? EFilter.Show : EFilter.Hide;
                        result.VisOwnRings = displayData.ContainsKey("PB12_33_b") ? EFilter.Show : EFilter.Hide;

                        // switch over to THRT
                        commands = new List<DCSCommand>()
                        {
                            new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1)
                        };
                        message = DCSConnection.SendRequest(new DCSMessage()
                        {
                            Commands = commands
                        });
                        if (message == null)
                        {
                            return null;
                        }
                        SleepForCommands(commands);

                        message = DCSConnection.SendRequest(new DCSMessage()
                        {
                            GetCockpitDisplayData = new List<int> { displayToRead }
                        });

                        if (message == null
                            || message.CockpitDisplayData == null
                            || !message.CockpitDisplayData.ContainsKey(displayToRead)
                        )
                        {
                            return null;
                        }

                        displayData = message.CockpitDisplayData[displayToRead];

                        // read THRT
                        result.VisThreatACQ = displayData.ContainsKey("PB8_37_b") ? EFilter.Show : EFilter.Hide;
                        result.VisThreatTrnPt = displayData.ContainsKey("PB9_39_b") ? EFilter.Show : EFilter.Hide;
                        result.VisThreatFCR = displayData.ContainsKey("PB10_41_b") ? EFilter.Show : EFilter.Hide;
                        result.VisThreatThreats = displayData.ContainsKey("PB11_43_b") ? EFilter.Show : EFilter.Hide;
                        result.visThreatTargets = displayData.ContainsKey("PB12_45_b") ? EFilter.Show : EFilter.Hide;
                        break;
                }
            }

            commands = new List<DCSCommand>();
            // switch to original Threat Vis (OWN) if needed
            if (result.ThreatVis != EVis.Threat)
            {
                commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
            }

            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_TSD));

            // switch back to original phase
            if (result.Phase == EPhase.Navigation)
            {
                commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
            }

            message = DCSConnection.SendRequest(new DCSMessage()
            {
                Commands = commands
            });
            if (message == null)
            {
                return null;
            }
            SleepForCommands(commands);

            return result;
        }

        /// <summary>
        /// Generates the commands to enter the options.
        /// </summary>
        /// <param name="isPilot">if set to <c>true</c> [is pilot].</param>
        /// <returns>The list of commands for DCS</returns>
        public List<DCSCommand> GenerateCommands(bool isPilot)
        {
            AH64TSDOptionData startingCondition = ReadFromAC(isPilot);
            if (startingCondition == null)
            {
                return new List<DCSCommand>();
            }
            EPhase currentPhase = startingCondition.Phase;
            int mfd = isPilot ? (int)AH64.EDeviceCode.PLT_RMFD : (int)AH64.EDeviceCode.CPG_RMFD;

            var commands = new List<DCSCommand>()
            // var commands = new DebugCommandList()
            {
                new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_TSD)
            };

            if (HasMapPageData)
            {
                EMapType currentMapType = startingCondition.MapType;
                commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B4));
                if (Center != ECenter.No_Change && startingCondition.Center != Center)
                {
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T5));
                }
                if (Orientation != EOrientation.No_Change && startingCondition.Orientation != Orientation)
                {
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R5));
                    commands.Add(new DCSCommand(mfd, Orientation == EOrientation.HeadingUp ? (int)AH64.EKeyCode.MFD_R4
                                                                    : Orientation == EOrientation.TrackUp ? (int)AH64.EKeyCode.MFD_R5
                                                                    : (int)AH64.EKeyCode.MFD_R6));
                }
                if (Grid != EGrid.No_Change && startingCondition.Grid != Grid)
                {
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T5));
                }

                if (HasChartMapData)
                {
                    if (currentMapType != EMapType.Chart)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                        currentMapType = EMapType.Chart;
                    }
                    // Nothing here yet
                }

                if (HasDigMapData)
                {
                    if (currentMapType != EMapType.Digital)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        currentMapType = EMapType.Digital;
                    }
                    if (Gray != EGray.No_Change && startingCondition.Gray != Gray)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T4));
                    }
                    if (ColorBand != EColorBand.No_Change && startingCondition.ColorBand != ColorBand)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                        commands.Add(new DCSCommand(mfd, (int)(ColorBand == EColorBand.None ? AH64.EKeyCode.MFD_L5
                                                              : ColorBand == EColorBand.AC ? AH64.EKeyCode.MFD_L3
                                                              : AH64.EKeyCode.MFD_L4))
                        );
                    }
                    if (Contours != EContours.No_Change && startingCondition.Contours != Contours)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L5));
                        commands.Add(new DCSCommand(mfd, (int)(Contours == EContours.Contours_None ? AH64.EKeyCode.MFD_R6
                                                                : Contours == EContours.Contours_50 ? AH64.EKeyCode.MFD_R5
                                                                : Contours == EContours.Contours_100 ? AH64.EKeyCode.MFD_R4
                                                                : Contours == EContours.Contours_200 ? AH64.EKeyCode.MFD_L6
                                                                : Contours == EContours.Contours_500 ? AH64.EKeyCode.MFD_L5
                                                                : AH64.EKeyCode.MFD_L4))
                        );
                    }
                    if (ffd != EFfd.No_Change && startingCondition.ffd != ffd)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L6));
                        commands.Add(new DCSCommand(mfd, (int)(ffd == EFfd.FFD_None ? AH64.EKeyCode.MFD_L6
                                                                : ffd == EFfd.FFD_Both ? AH64.EKeyCode.MFD_L5
                                                                : ffd == EFfd.FFD_Lines ? AH64.EKeyCode.MFD_L4
                                                                : AH64.EKeyCode.MFD_L3))
                        );
                    }
                }

                if (HasSatMapData)
                {
                    if (currentMapType != EMapType.Satellite)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                        currentMapType = EMapType.Satellite;
                    }

                    if (SatLevel != ESatLevel.No_Change && startingCondition.SatLevel != SatLevel)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                    }
                }

                // done setting up all the map types, switch to the desired one
                EMapType desiredMapType = MapType == EMapType.No_Change ? startingCondition.MapType : MapType;
                if (desiredMapType != currentMapType)
                {
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                    commands.Add(new DCSCommand(mfd, (int)(desiredMapType == EMapType.Chart ? AH64.EKeyCode.MFD_L3
                                                            : desiredMapType == EMapType.Digital ? AH64.EKeyCode.MFD_L2
                                                            : desiredMapType == EMapType.Satellite ? AH64.EKeyCode.MFD_L4
                                                            : AH64.EKeyCode.MFD_L5))
                    );
                    // currentMapType = desiredMapType;
                }
            }

            if (HasNavPhaseData || HasAtkPhaseData || HasVisData)
            {
                // Show page
                commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T3));

                if (currentPhase == EPhase.Attack && HasNavPhaseData)
                {
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                    currentPhase = EPhase.Navigation;
                }

                if (HasNavPhaseData)
                {
                    if (NavWpData != EFilter.No_Change && startingCondition.NavWpData != NavWpData)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                    }
                    if (NavInactiveZones != EFilter.No_Change && startingCondition.NavInactiveZones != NavInactiveZones)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                    }
                    if (NavObstacles != EFilter.No_Change && startingCondition.NavObstacles != NavObstacles)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                    }
                    if (NavCursor != EFilter.No_Change && startingCondition.NavCursor != NavCursor)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L5));
                    }
                    if (NavCursorInfo != EFilter.No_Change && startingCondition.NavCursorInfo != NavCursorInfo)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L6));
                    }
                    if (NavHSI != EFilter.No_Change && startingCondition.NavHSI != NavHSI)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R4));
                    }
                    if (NavEndurance != EFilter.No_Change && startingCondition.NavEndurance != NavEndurance)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R5));
                    }
                    if (NavWind != EFilter.No_Change && startingCondition.NavWind != NavWind)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R6));
                    }

                    if (HasNavCoordData)
                    {
                        // coord show page
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6));

                        if (NavCtrlMeasures != EFilter.No_Change && startingCondition.NavCtrlMeasures != NavCtrlMeasures)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        }
                        if (NavFriendlyUnits != EFilter.No_Change && startingCondition.NavFriendlyUnits != NavFriendlyUnits)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                        }
                        if (NavEnemyUnits != EFilter.No_Change && startingCondition.NavEnemyUnits != NavEnemyUnits)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                        }
                        if (NavTargets != EFilter.No_Change && startingCondition.NavTargets != NavTargets)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L5));
                        }
                        if (NavLines != EFilter.No_Change && startingCondition.NavLines != NavLines)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
                        }
                        if (NavAreas != EFilter.No_Change && startingCondition.NavAreas != NavAreas)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R2));
                        }
                        // back to main show page
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6));
                    }
                }

                if (HasAtkPhaseData)
                {
                    if (currentPhase == EPhase.Navigation)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                        currentPhase = EPhase.Attack;
                    }

                    if (AtkCurrentRoute != EFilter.No_Change && startingCondition.AtkCurrentRoute != AtkCurrentRoute)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                    }
                    if (AtkInactiveZones != EFilter.No_Change && startingCondition.AtkInactiveZones != AtkInactiveZones)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                    }
                    if (AtkFCRObstacles != EFilter.No_Change && startingCondition.AtkFCRObstacles != AtkFCRObstacles)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                    }
                    if (AtkCursor != EFilter.No_Change && startingCondition.AtkCursor != AtkCursor)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L5));
                    }
                    if (AtkCursorInfo != EFilter.No_Change && startingCondition.AtkCursorInfo != AtkCursorInfo)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L6));
                    }
                    if (AtkHSI != EFilter.No_Change && startingCondition.AtkHSI != AtkHSI)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R4));
                    }
                    if (AtkEndurance != EFilter.No_Change && startingCondition.AtkEndurance != AtkEndurance)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R5));
                    }
                    if (AtkWind != EFilter.No_Change && startingCondition.AtkWind != AtkWind)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R6));
                    }

                    if (HasAtkCoordData)
                    {
                        // go to coord show page
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T6));

                        if (AtkCtrlMeasures != EFilter.No_Change && startingCondition.AtkCtrlMeasures != AtkCtrlMeasures)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                        }
                        if (AtkFriendlyUnits != EFilter.No_Change && startingCondition.AtkFriendlyUnits != AtkFriendlyUnits)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L3));
                        }
                        if (AtkEnemyUnits != EFilter.No_Change && startingCondition.AtkEnemyUnits != AtkEnemyUnits)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L4));
                        }
                        if (AtkTargets != EFilter.No_Change && startingCondition.AtkTargets != AtkTargets)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L5));
                        }
                        if (AtkLines != EFilter.No_Change && startingCondition.AtkLines != AtkLines)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
                        }
                        if (AtkAreas != EFilter.No_Change && startingCondition.AtkAreas != AtkAreas)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R2));
                        }
                        if (AtkShotAt != EFilter.No_Change && startingCondition.AtkShotAt != AtkShotAt)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R3));
                        }
                    }
                }

                if (HasVisData)
                {
                    // Go to threat show page
                    commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_T5));

                    EVis currentVis = startingCondition.ThreatVis;
                    // enter generic vis data
                    if (AseThreats != EFilter.No_Change && startingCondition.AseThreats != AseThreats)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L2));
                    }
                    if (VisShade != EFilter.No_Change && startingCondition.VisShade != VisShade)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_L6));
                    }

                    // enter own vis data if exists
                    if (HasOwnVisData)
                    {
                        if (currentVis != EVis.Own)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
                            currentVis = EVis.Own;
                        }

                        if (VisOwnOwn != EFilter.No_Change && startingCondition.VisOwnOwn != VisOwnOwn)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R2));
                        }
                        if (VisOwnTrnPt != EFilter.No_Change && startingCondition.VisOwnTrnPt != VisOwnTrnPt)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R3));
                        }
                        if (VisOwnGhost != EFilter.No_Change && startingCondition.VisOwnGhost != VisOwnGhost)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R4));
                        }
                        if (VisOwnRings != EFilter.No_Change && startingCondition.VisOwnRings != VisOwnRings)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R6));
                        }
                    }
                    if (HasThreatVisData)
                    {
                        if (currentVis != EVis.Threat)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
                            currentVis = EVis.Threat;
                        }
                        if (VisThreatACQ != EFilter.No_Change && startingCondition.VisThreatACQ != VisThreatACQ)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R2));
                        }
                        if (VisThreatTrnPt != EFilter.No_Change && startingCondition.VisThreatTrnPt != VisThreatTrnPt)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R3));
                        }
                        if (VisThreatFCR != EFilter.No_Change && startingCondition.VisThreatFCR != VisThreatFCR)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R4));
                        }
                        if (VisThreatThreats != EFilter.No_Change && startingCondition.VisThreatThreats != VisThreatThreats)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R5));
                        }
                        if (visThreatTargets != EFilter.No_Change && startingCondition.visThreatTargets != visThreatTargets)
                        {
                            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R6));
                        }
                    }

                    // enter threat vis data if exists


                    // select correct vis
                    EVis desiredVis = ThreatVis == EVis.No_Change ? startingCondition.ThreatVis : ThreatVis;
                    if (desiredVis != currentVis)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_R1));
                    }
                }
            }

            // TSD main page
            commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_TSD));
            switch (Phase)
            {
                case EPhase.No_Change:
                    // go back to the original phase
                    if (currentPhase != Phase)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                    }
                    break;
                case EPhase.Navigation:
                    // go to nav phase if required
                    if (currentPhase != EPhase.Navigation)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                    }
                    break;
                case EPhase.Attack:
                    // go to atk phase if required
                    if (currentPhase != EPhase.Attack)
                    {
                        commands.Add(new DCSCommand(mfd, (int)AH64.EKeyCode.MFD_B2));
                    }
                    break;
            }

            return commands;
        }

        #endregion
    }
}
