using System;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents a CNV setting for an AH64 radio.
    /// </summary>
    public class AH64RadioCNVSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AH64RadioCNVSetting"/> class.
        /// </summary>
        /// <param name="val">The value.</param>
        public AH64RadioCNVSetting(int? val)
        {
            this.val = val;
        }
        private int? val = 1;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value needs to be 1..6</exception>
        public int? Value
        {
            get { return val; }
            set
            {
                if (value == null)
                {
                    this.val = null;
                    return;
                }
                if (value < 1 || value > 6)
                {
                    throw new ArgumentOutOfRangeException("value needs to be 1..6");
                }
                val = value;
            }
        }
    }
}
