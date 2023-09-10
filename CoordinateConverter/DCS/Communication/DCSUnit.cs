using CoordinateConverter.DCS.Aircraft;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Communication
{
    /// <summary>
    /// Represents a unit in DCS as returned by <c>LoGetWorldObjects()</c> or <c>LoGetObjectById(id)</c>
    /// </summary>
    public class DCSUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DCSUnit"/> class.
        /// </summary>
        [JsonConstructor]
        public DCSUnit() { /* Empty */ }

        /// <summary>
        /// Initializes a new instance of the <see cref="DCSUnit"/> class.
        /// </summary>
        /// <param name="other">The unit to clone the information from</param>
        public DCSUnit(DCSUnit other)
        {
            TypeName = other.TypeName;
            Type = other.Type;
            Country = other.Country;
            Coalition = other.Coalition;
            Coordinate = other.Coordinate;
            Heading = other.Heading;
            Pitch = other.Pitch;
            Bank = other.Bank;
            WorldPosition = other.WorldPosition == null ? null : new Dictionary<string, double>(other.WorldPosition);
            UnitName = other.UnitName;
            GroupName = other.GroupName;
            Flags = other.Flags;
            ObjectId = other.ObjectId;
        }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>
        /// The object identifier.
        /// </value>
        [JsonProperty("ObjectId")]
        public int ObjectId { get; set; }
        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>
        /// The name of the type.
        /// </value>
        [JsonProperty("Name")]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the type information on the unit.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("Type")]
        public DCSUnitTypeInformation Type { get; set; }

        /// <summary>
        /// Gets or sets the country. See <c>Scripts/database/db_countries.lua</c>
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [JsonProperty("Country")]
        public int? Country { get; set; }

        public enum ECoalition
        {
            Neutral = 0,
            Red = 1,
            Blue = 2
        }

        /// <summary>
        /// Gets or sets the coalition identifier.
        /// </summary>
        /// <value>
        /// The coalition identifier.
        /// </value>
        [JsonProperty("CoalitionID")]
        public ECoalition Coalition { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the unit.
        /// </summary>
        /// <value>
        /// The coordinate.
        /// </value>
        [JsonProperty("LatLongAlt")]
        public DCSCoordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the heading (radians).
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        [JsonProperty("Heading")]
        public double? Heading { get; set; }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        [JsonProperty("Pitch")]
        public double? Pitch { get; set; }

        /// <summary>
        /// Gets or sets the bank.
        /// </summary>
        /// <value>
        /// The bank.
        /// </value>
        [JsonProperty("Bank")]
        public double? Bank { get; set; }

        /// <summary>
        /// Gets or sets the world position.
        /// </summary>
        /// <value>
        /// The world position.
        /// </value>
        [JsonProperty("Position")]
        public Dictionary<string, double> WorldPosition { get; set; }

        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        [JsonProperty("UnitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        [JsonProperty("GroupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        /// <value>
        /// The flags.
        /// </value>
        [JsonProperty("Flags")]
        public DCSUnitFlags Flags { get; set; }
    }

    /// <summary>
    /// Type information on a DCS world object as seen in <c>Scripts/database/wsTypes.lua</c>
    /// </summary>
    public class DCSUnitTypeInformation
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public enum ELevel1Type
        {
            Air = 1,
            Ground = 2,
            Navy = 3,
            Weapon = 4,
            Static = 5,
            Destroyed = 6,
            Test1 = 200,
            Point = 201
        }

        public enum ELevel2Type
        {
            // Air objects (wType_Air)
            Airplane = 1,
            Helicopter = 2,
            Free_Fall = 3,
            // Weapon (Weapon)
            Missile = 4,
            Bomb = 5,
            Shell = 6,
            NURS = 7,
            Torpedo = 8,
            // Ground objects(Ground)
            Moving = 8,
            Standing = 9,
            Tank = 17,
            SAM = 16,
            //  Navy objects(Navy)
            Ship = 12,
            //  object (Static)
            Airdrome = 13,
            Explosion = 14,

            GContainer = 15,
            AirdromePart = 18,
            WingPart = 19
        }

        public enum ELevel3Type
        {
            // Air objects, Airplane
            Fighter = 1,
            F_Bomber = 2,
            Intercepter = 3,
            Intruder = 4,
            Cruiser = 5,
            Battleplane = 6,
            // Free-fall air objects
            Snars = 31,
            Parts = 35,
            FuelTank = 43,
            // Missile
            AA_Missile = 7,
            AS_Missile = 8,
            SA_Missile = 34,
            SS_Missile = 11,

            A_Torpedo = 10,
            S_Torpedo = 11,

            AA_TRAIN_Missile = 100,
            AS_TRAIN_Missile = 101,

            // Bomb
            Bomb_A = 9,
            Bomb_Guided = 36,
            Bomb_BetAB = 37,
            Bomb_Cluster = 38,
            Bomb_Antisubmarine = 39,
            Bomb_ODAB = 40,
            Bomb_Fire = 41,
            Bomb_Nuclear = 42,
            Bomb_Lighter = 49,
            // Shell
            Shell_A = 10,

            // Navy objects (Navy, Cruiser)
            AirCarrier = 12,
            HCarrier = 13,
            ArmedShip = 14,
            CivilShip = 15,
            Submarine = 16,
            // Airdrome
            RW1 = 20,
            RW2 = 30,
            Heliport = 40,
            // Explosion
            GroundExp = 29,
            //           NURS
            Container = 32,
            Rocket = 33,
            //           GContainer
            Control_Cont = 44,
            Jam_Cont = 45,
            Cannon_Cont = 46,
            Support = 47,
            Snare_Cont = 48,
            Smoke_Cont = 50,

            //  Ground object (Moving || Standing )
            NoWeapon = 25,
            Gun = 26,
            Miss = 27,
            ChildMiss = 28,
            MissGun = 104,
            Civil = 100,
            //***************************************************
            Radar = 101,
            Radar_Miss = 102,
            Radar_MissGun = 103,
            Radar_Gun = 105
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        /// <summary>
        /// Broad classification of the object.
        /// </summary>
        /// <value>
        /// The level1.
        /// </value>
        [JsonProperty("level1")]
        public ELevel1Type Level1 { get; set; }

        /// <summary>
        /// The general type of the object.
        /// </summary>
        /// <value>
        /// The level2.
        /// </value>
        [JsonProperty("level2")]
        public ELevel2Type Level2 { get; set; }

        /// <summary>
        /// The specific type of the object
        /// </summary>
        /// <value>
        /// The level3.
        /// </value>
        [JsonProperty("level3")]
        public ELevel3Type Level3 { get; set; }

        /// <summary>
        /// Specific Model type of the object. Due to growth concerns, there is no enum value backing this up.
        /// </summary>
        /// <value>
        /// The level4 type information.
        /// </value>
        [JsonProperty("level4")]
        public int Level4 { get; set; }
    }

    /// <summary>
    /// Flags set for a DCS Unit
    /// </summary>
    public class DCSUnitFlags
    {
        /// <summary>
        /// Gets or sets a value indicating whether the unit has radar active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is radar active; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("RadarActive")]
        public bool? IsRadarActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is human.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is human; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("Human")]
        public bool? IsHuman { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is jamming.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is jamming; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("Jamming")]
        public bool? IsJamming { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is ir jamming.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is ir jamming; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("IRJamming")]
        public bool? IsIRJamming { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is activated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is activated; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("Born")]
        public bool? IsActivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit has ai enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit has ai enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("AI_ON")]
        public bool? HasAIEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is invisible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is invisible; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("Invisible")]
        public bool? IsInvisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit is a static object.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the unit is a static object; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("Static")]
        public bool? IsStaticObject { get; set; }
    }
}
