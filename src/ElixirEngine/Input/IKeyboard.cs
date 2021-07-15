namespace ElixirEngine.Input
{
    /// <summary>
    ///     Represents the application's keyboard device.
    /// </summary>
    public interface IKeyboard : IInputDevice
    {
        /// <summary>
        ///     Gets the <see cref="KeyboardKeyState" /> for the specified <see cref="KeyboardKey" />.
        /// </summary>
        /// <param name="keyboardKey">
        ///     The <see cref="KeyboardKey" /> to get the <see cref="KeyboardKeyState" /> for.
        /// </param>
        /// <returns>
        ///     The <see cref="KeyboardKeyState" /> for the specified <see cref="KeyboardKey" />.
        /// </returns>
        KeyboardKeyState GetKeyboardKeyState(KeyboardKey keyboardKey);
    }
}