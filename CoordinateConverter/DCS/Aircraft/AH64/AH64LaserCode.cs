using Newtonsoft.Json;
using System;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents a laser code in the AH64
    /// </summary>
    public class AH64LaserCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64LaserCode"/> class.
        /// </summary>
        /// <param name="laserCode">The laser code.</param>
        [JsonConstructor]
        public AH64LaserCode(int laserCode)
        {
            LaserCode = laserCode;
        }

        private int laserCode = 1688;
        /// <summary>
        /// Gets or sets the laser code.
        /// </summary>
        /// <value>
        /// The laser code.
        /// </value>
        /// <exception cref="System.ArgumentException">Laser code invalid</exception>
        public int LaserCode
        {
            get
            {
                return laserCode;
            }
            set
            {
                if (!IsLaserCodeValid(value))
                {
                    throw new ArgumentException(LaserCodeError, nameof(LaserCode));
                }
                laserCode = value;
            }
        }

        /// <summary>
        /// The laser code error
        /// </summary>
        [JsonIgnore]
        public const string LaserCodeError = "Laser code must be in Range 1111-1788, 2111-2888 or 4111-5888 and must not contain the digit 9.";

        /// <summary>
        /// Determines whether the provided laser code is valid.
        /// </summary>
        /// <param name="laserCode">The laser code.</param>
        /// <returns>
        ///   <c>true</c> if laser code is valid; otherwise, <c>false</c>.
        /// </returns>
        static public bool IsLaserCodeValid(int laserCode)
        {
            if (laserCode.ToString().Length != 4)
            {
                return false;
            }
            int[] digits = new int[4];
            digits[0] = laserCode / 1000;          // Thousands place
            digits[1] = (laserCode / 100) % 10;    // Hundreds place
            digits[2] = (laserCode / 10) % 10;     // Tens place
            digits[3] = laserCode % 10;            // Ones place

            if (digits[0] == 1)
            {
                return digits[1] >= 1 && digits[1] <= 7 && digits[2] >= 1 && digits[2] <= 8 && digits[3] >= 1 && digits[3] <= 8;
            }
            if (digits[0] == 3 || digits[0] > 5)
            {
                return false;
            }
            return digits[2] >= 1 && digits[2] <= 8 && digits[3] >= 1 && digits[3] <= 8;
        }
    }
}
