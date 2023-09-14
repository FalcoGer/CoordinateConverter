using Newtonsoft.Json;
using System;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// coordinate data specific to F18C
    /// </summary>
    /// <seealso cref="CoordinateConverter.DCS.Aircraft.AircraftSpecificData" />
    public class F18CSpecificData : AircraftSpecificData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="F18CSpecificData"/> class.
        /// This will be a default waypoint
        /// </summary>
        public F18CSpecificData()
        {
             // Empty
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="F18CSpecificData"/> class.
        /// The data is for a standard waypoint.
        /// </summary>
        public F18CSpecificData(bool isSlamErSTP)
        {
            if (isSlamErSTP)
            {
                WeaponType = F18C.EWeaponType.SLAMER;
                StationSetting = EStationSetting.All;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F18CSpecificData"/> class.
        /// The data is for a pre planned strike target for a GPS guided weapon.
        /// </summary>
        /// <param name="weaponType">Type of the programmable weapon.</param>
        /// <param name="preplanPointIdx">Index of the preplan point. Must be 1 through 6.</param>
        /// <param name="stationSetting">Whether to go to the next station before putting in the data, to not do that or to put the data into all stations with that weapon.</param>
        [JsonConstructor]
        public F18CSpecificData(F18C.EWeaponType? weaponType, int? preplanPointIdx, EStationSetting stationSetting)
        {
            WeaponType = weaponType;
            PreplanPointIdx = preplanPointIdx;
            StationSetting = stationSetting;
        }

        /// <summary>
        /// Gets or sets the type of the weapon. If null the point is not a weapon point.
        /// </summary>
        /// <value>
        /// The type of the weapon.
        /// </value>
        public F18C.EWeaponType? WeaponType { get; private set; } = null;

        private int? preplanPointIdx = null;
        /// <summary>
        /// Gets or sets the preplan point identifier for GPS guided weapons. Must be 1 through 6. null if not a GPS guided weapon point.
        /// </summary>
        /// <value>
        /// The preplan point identifier.
        /// </value>
        public int? PreplanPointIdx
        {
            get
            {
                return preplanPointIdx;
            }
            private set
            {
                if (value.HasValue && (value <= 0 || value > 6))
                {
                    throw new ArgumentException("Value must be in [1 .. 6]");
                }
                preplanPointIdx = value;
            }
        }

        /// <summary>
        /// How to handle weapon stations
        /// </summary>
        public enum EStationSetting
        {
            /// <summary>
            /// Step to the next weapon in the group before putting the point into it.
            /// </summary>
            Step,
            /// <summary>
            /// Next PP/SLAM-ER STP WP is for the same weapon.
            /// </summary>
            Stay,
            /// <summary>
            /// Put this setting into all the weapons of this type.
            /// </summary>
            All
        }

        /// <summary>
        /// Gets or sets the station setting.
        /// </summary>
        /// <value>
        /// Wether to step before the next station, not to do that, or configure all weapons of this type on the aircraft.
        /// </value>
        public EStationSetting StationSetting { get; set; } = EStationSetting.All;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string ToString()
        {
            
            if (!WeaponType.HasValue)
            {
                return F18C.WAYPOINT_STR;
            }
            else
            {
                string ret = String.Empty;
                if (preplanPointIdx.HasValue)
                {
                    ret += WeaponType.Value.ToString() + "/PP " + preplanPointIdx.Value.ToString() + "-" + StationSetting.ToString();
                }
                else
                {
                    ret += F18C.SLAMER_STP_STR;
                }
                return ret;
            }
        }

        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>
        /// A copy of this instance.
        /// </returns>
        public override AircraftSpecificData Clone()
        {
            F18CSpecificData ret = new F18CSpecificData() { preplanPointIdx = this.preplanPointIdx, WeaponType = this.WeaponType, StationSetting = StationSetting };
            return ret;
        }
    }
}
