namespace ElixirEngine
{
    /// <summary>
    ///     Represents the real global time since application initialization.
    /// </summary>
    public interface IGlobalTime
    {
        /// <summary>
        ///     Gets the frames per second.
        /// </summary>
        int FramesPerSecond { get; }

        /// <summary>
        ///     Gets the milliseconds of the current frame.
        /// </summary>
        public long Milliseconds { get; }
    }
}