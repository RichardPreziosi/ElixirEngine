using System;
using System.Collections.Generic;
using System.Numerics;
using ElixirEngine.Extensions;

namespace ElixirEngine.Input
{
    /// <inheritdoc cref="IMouse" />
    internal class Mouse : IMouse, IDisposable
    {
        /// <summary>
        ///     The mouse button states.
        /// </summary>
        private readonly MouseButtonState[] _mouseButtonStates;

        /// <summary>
        ///     The pressed mouse buttons.
        /// </summary>
        private readonly List<MouseButton> _pressedMouseButtons;

        /// <summary>
        ///     The released mouse buttons.
        /// </summary>
        private readonly List<MouseButton> _releasedMouseButtons;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Mouse" /> class.
        /// </summary>
        public Mouse()
        {
            _mouseButtonStates = new MouseButtonState[(int) EnumExtensions.GetMaximum<MouseButton>()];
            _pressedMouseButtons = new List<MouseButton>();
            _releasedMouseButtons = new List<MouseButton>();
        }

        /// <inheritdoc />
        public Vector2 Position { get; private set; }

        /// <inheritdoc />
        public Vector2 ScrollWheelPosition { get; private set; }

        /// <inheritdoc />
        public void Dispose()
        {
            // TODO: Dispose of things...?
        }

        /// <inheritdoc />
        public MouseButtonState GetMouseButtonState(MouseButton mouseButton)
        {
            return _mouseButtonStates[(int) mouseButton];
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_MouseButtonEvent" /> triggered by
        ///     <see cref="SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN" />.
        /// </summary>
        /// <param name="mouseButtonEvent">
        ///     The <see cref="SDL.SDL_MouseButtonEvent" /> event to be handled.
        /// </param>
        public void ProcessKeyDownEvent(SDL.SDL_MouseButtonEvent mouseButtonEvent)
        {
            _pressedMouseButtons.Add((MouseButton) mouseButtonEvent.button);
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_MouseButtonEvent" /> triggered by
        ///     <see cref="SDL.SDL_EventType.SDL_MOUSEBUTTONUP" />.
        /// </summary>
        /// <param name="mouseButtonEvent">
        ///     The <see cref="SDL.SDL_MouseButtonEvent" /> event to be handled.
        /// </param>
        public void ProcessKeyUpEvent(SDL.SDL_MouseButtonEvent mouseButtonEvent)
        {
            _releasedMouseButtons.Add((MouseButton) mouseButtonEvent.button);
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_MouseMotionEvent" />.
        /// </summary>
        /// <param name="mouseMotionEvent">
        ///     The <see cref="SDL.SDL_MouseMotionEvent" /> event to be handled.
        /// </param>
        public void ProcessMouseMotionEvent(SDL.SDL_MouseMotionEvent mouseMotionEvent)
        {
            Position = new Vector2(mouseMotionEvent.x, mouseMotionEvent.y);
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_MouseWheelEvent" />.
        /// </summary>
        /// <param name="mouseWheelEvent">
        ///     The <see cref="SDL.SDL_MouseWheelEvent" /> event to be handled.
        /// </param>
        public void ProcessMouseWheelEvent(SDL.SDL_MouseWheelEvent mouseWheelEvent)
        {
            ScrollWheelPosition = new Vector2(mouseWheelEvent.x, mouseWheelEvent.y);
        }

        /// <summary>
        ///     Updates the state of the <see cref="Mouse" />.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < _mouseButtonStates.Length - 1; i++)
            {
                _mouseButtonStates[i] = GetUpdatedMouseButtonState((MouseButton) i);
            }

            _pressedMouseButtons.Clear();
            _releasedMouseButtons.Clear();
        }

        /// <summary>
        ///     Gets the updated <see cref="KeyboardKey" /> for a provided <see cref="KeyboardKey" />.
        /// </summary>
        /// <param name="keyboardKey">
        ///     The <see cref="KeyboardKeyState" /> to get the updated state for.
        /// </param>
        /// <returns>
        ///     The updated <see cref="KeyboardKeyState" />.
        /// </returns>
        private MouseButtonState GetUpdatedMouseButtonState(MouseButton keyboardKey)
        {
            bool isReleased = _releasedMouseButtons.Contains(keyboardKey);
            bool isPressed = _pressedMouseButtons.Contains(keyboardKey);
            MouseButtonState previousState = _mouseButtonStates[(int) keyboardKey];

            if (previousState == MouseButtonState.Pressing && isReleased == false)
            {
                return MouseButtonState.Pressed;
            }

            if (isPressed && isReleased == false)
            {
                return previousState != MouseButtonState.Pressed ? MouseButtonState.Pressing : previousState;
            }

            if (isReleased)
            {
                return MouseButtonState.Releasing;
            }

            return previousState == MouseButtonState.Releasing ? MouseButtonState.Released : previousState;
        }
    }
}