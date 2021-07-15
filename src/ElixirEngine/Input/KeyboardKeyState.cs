namespace ElixirEngine.Input
{
    /// <summary>
    ///     Represents the various states of a <see cref="KeyboardKey" />.
    /// </summary>
    public enum KeyboardKeyState
    {
        /// <summary>
        ///     The key is released.
        /// </summary>
        Released,

        /// <summary>
        ///     The key is being released.
        /// </summary>
        Releasing,

        /// <summary>
        ///     The key is being pressed.
        /// </summary>
        Pressing,

        /// <summary>
        ///     The key is pressed/held.
        /// </summary>
        Pressed
    }
}