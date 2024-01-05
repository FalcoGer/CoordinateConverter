﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Represents a frequency that a radio can be tuned to.
    /// </summary>
    public class RadioFrequency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadioFrequency"/> class.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="stepSize">Size of the steps between valid frequencies.</param>
        public RadioFrequency(decimal frequency, decimal minimum, decimal maximum, decimal stepSize)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            if (minimum > maximum) { throw new ArgumentException("Minimum (" + minimum.ToString() + ") > Maximum (" + maximum.ToString() + ")"); }
            this.stepSize = stepSize;
            // Set frequency through the accessor after the other values have been set
            Frequency = frequency;
        }
        private decimal frequency;
        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value was too small or too big</exception>
        /// <exception cref="System.ArgumentException">value must be divisible by stepSize</exception>
        public decimal Frequency
        {
            get { return frequency; }
            set
            {
                if (frequency < minimum)
                {
                    throw new ArgumentOutOfRangeException("value was too small " + value.ToString() + " < " + minimum.ToString() + "!");
                }
                if (frequency > maximum)
                {
                    throw new ArgumentOutOfRangeException("value was too large " + value.ToString() + " > " + maximum.ToString() + "!");
                }
                decimal divisionResult = decimal.Divide(frequency, stepSize);
                if (divisionResult != decimal.Truncate(divisionResult))
                {
                    throw new ArgumentException(
                        "value must be divisible by " +
                        stepSize.ToString() +
                        " exactly, but was " +
                        value.ToString() + " / " + stepSize.ToString() + " = " + divisionResult.ToString()
                    );
                }
                frequency = value;
            }
        }
        private decimal minimum;
        /// <summary>
        /// Gets the minimum frequency allowed.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public decimal Minimum { get { return maximum; } }
        private decimal maximum;
        /// <summary>
        /// Gets the maximum frequency allowed.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public decimal Maximum { get { return minimum; } }
        private decimal stepSize;
        /// <summary>
        /// Gets the size of the steps between valid frequencies.
        /// </summary>
        /// <value>
        /// The size of the step.
        /// </value>
        public decimal StepSize { get { return stepSize; } }
    }
}
