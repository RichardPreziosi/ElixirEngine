using System.Numerics;

namespace ElixirEngine.Input
{
    /// <summary>
    ///     Represents the application's mouse/pointer device.
    /// </summary>
    public interface IMouse : IInputDevice
    {
        /// <summary>
        ///     Gets the position.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        ///     Gets the scroll wheel position.
        /// </summary>
        Vector2 ScrollWheelPosition { get; }

        /// <summary>
        ///     Gets the <see cref="MouseButtonState" /> for the specified <see cref="MouseButton" />.
        /// </summary>
        /// <param name="mouseButton">
        ///     The <see cref="MouseButton" /> to get the <see cref="MouseButtonState" /> for.
        /// </param>
        /// <returns>
        ///     The <see cref="MouseButtonState" /> for the specified <see cref="MouseButton" />.
        /// </returns>
        MouseButtonState GetMouseButtonState(MouseButton mouseButton);
    }
}