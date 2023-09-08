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
        /// Clones the specified other.
        /// </summary>
        /// <returns>A clone of the data</returns>
        public abstract AircraftSpecificData Clone();
    }
}
