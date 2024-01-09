using System;
using System.Collections.Generic;

namespace CoordinateConverter.DCS.Aircraft.AH64
{
    /// <summary>
    /// Represents ADF Data in the AH64
    /// </summary>
    public class AH64ADFData
    {
        private RadioFrequency frequency = new RadioFrequency(100.0m, 100.0m, 2199.5m, 0.25m);
        /// <summary>
        /// Gets or sets the frequency.<br></br>
        /// Valid: 100.0 kHz to 2199.5 kHz, Interval 0.25 khZ steps
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public decimal Frequency {
            get
            {
                return frequency.Frequency;
            }
            set
            {
                frequency.Frequency = value;
            }
        }

        private string identifier = null;
        /// <summary>
        /// Gets or sets the 1-3 letter identifier.
        /// </summary>
        /// <value>
        /// The identifier or null.
        /// </value>
        /// <exception cref="System.ArgumentException">Must be 1-3 Letters - Identifier</exception>
        public string Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    identifier = null;
                }
                List<char> valid = new List<char>();
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    valid.Add(c);
                }
                if (!AH64.GetIsValidTextForKU(value, 1, 3, valid))
                {
                    throw new ArgumentException("Must be 1-3 Letters", nameof(Identifier));
                }
            }
        }

    }
}
