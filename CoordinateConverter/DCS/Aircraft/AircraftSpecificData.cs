namespace CoordinateConverter.DCS.Aircraft
{
    /// <summary>
    /// Data specific to an aircraft type
    /// </summary>
    public abstract class AircraftSpecificData
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public abstract override string ToString();

        /// <summary>
        /// Clones the data.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public abstract AircraftSpecificData Clone();
    }
}
