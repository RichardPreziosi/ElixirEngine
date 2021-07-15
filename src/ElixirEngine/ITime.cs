namespace ElixirEngine
{
    /// <summary>
    ///     Represents the frame time.
    /// </summary>
    public interface ITime
    {
        /// <summary>
        ///     Gets the delta time of the current frame.
        /// </summary>
        float Delta { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether the application is paused.
        /// </summary>
        bool IsPaused { get; set; }

        /// <summary>
        ///     Gets the rapid update delta time of the current frame.
        /// </summary>
        float RapidUpdateDelta { get; }

        /// <summary>
        ///     Gets or sets the rate at which the application should run.
        /// </summary>
        float SpeedFactor { get; set; }

        float Total { get; }

        /// <summary>
        ///     Checks to see if a provided time interval has passed.
        /// </summary>
        /// <param name="interval">
        ///     The time interval to check if has passed.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the time interval has passed; otherwise, <see langword="false" />.
        /// </returns>
        bool CheckEvery(float interval);
    }
}