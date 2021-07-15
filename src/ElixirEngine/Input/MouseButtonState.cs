namespace ElixirEngine.Input
{
    /// <summary>
    ///     Represents the various states of a <see cref="MouseButtonState" />.
    /// </summary>
    public enum MouseButtonState
    {
        /// <summary>
        ///     The button is released.
        /// </summary>
        Released,

        /// <summary>
        ///     The button is being released.
        /// </summary>
        Releasing,

        /// <summary>
        ///     The button is being pressed.
        /// </summary>
        Pressing,

        /// <summary>
        ///     The button is pressed/held.
        /// </summary>
        Pressed
    }
}