// TODO: Need to handle triggering input on entities.
// TODO: Need to determine how text input will be handled (SDL event).

using System;
using System.Collections.Generic;
using ElixirEngine.Extensions;

namespace ElixirEngine.Input
{
    /// <inheritdoc cref="IKeyboard" />
    internal class Keyboard : IKeyboard, IDisposable
    {
        /// <summary>
        ///     The keyboard key states.
        /// </summary>
        private readonly KeyboardKeyState[] _keyboardKeyStates;

        /// <summary>
        ///     The pressed keyboard keys.
        /// </summary>
        private readonly List<KeyboardKey> _pressedKeyboardKeys;

        /// <summary>
        ///     The released keyboard keys.
        /// </summary>
        private readonly List<KeyboardKey> _releasedKeyboardKeys;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Keyboard" /> class.
        /// </summary>
        public Keyboard()
        {
            _keyboardKeyStates = new KeyboardKeyState[(int) EnumExtensions.GetMaximum<KeyboardKey>()];
            _pressedKeyboardKeys = new List<KeyboardKey>();
            _releasedKeyboardKeys = new List<KeyboardKey>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // TODO: Dispose of things...?
        }

        /// <inheritdoc />
        public KeyboardKeyState GetKeyboardKeyState(KeyboardKey keyboardKey)
        {
            return _keyboardKeyStates[(int) keyboardKey];
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_KeyboardEvent" /> event triggered by <see cref="SDL.SDL_EventType.SDL_KEYDOWN" />.
        /// </summary>
        /// <param name="keyboardEvent">
        ///     The <see cref="SDL.SDL_KeyboardEvent" /> event to be handled.
        /// </param>
        public void ProcessKeyDownEvent(SDL.SDL_KeyboardEvent keyboardEvent)
        {
            _pressedKeyboardKeys.Add((KeyboardKey) keyboardEvent.keysym.scancode);
        }

        /// <summary>
        ///     Processes a <see cref="SDL.SDL_KeyboardEvent" /> event triggered by <see cref="SDL.SDL_EventType.SDL_KEYUP" />.
        /// </summary>
        /// <param name="keyboardEvent">
        ///     The <see cref="SDL.SDL_KeyboardEvent" /> event to be handled.
        /// </param>
        public void ProcessKeyUpEvent(SDL.SDL_KeyboardEvent keyboardEvent)
        {
            _releasedKeyboardKeys.Add((KeyboardKey) keyboardEvent.keysym.scancode);
        }

        /// <summary>
        ///     Updates the state of the <see cref="Keyboard" />.
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < _keyboardKeyStates.Length - 1; i++)
            {
                _keyboardKeyStates[i] = GetUpdatedKeyboardKeyState((KeyboardKey) i);
            }

            _pressedKeyboardKeys.Clear();
            _releasedKeyboardKeys.Clear();
        }

        /// <summary>
        ///     Gets the updated <see cref="KeyboardKeyState" /> for a provided <see cref="KeyboardKey" />.
        /// </summary>
        /// <param name="keyboardKey">
        ///     The <see cref="KeyboardKey" /> to get the updated state for.
        /// </param>
        /// <returns>
        ///     The updated <see cref="KeyboardKeyState" />.
        /// </returns>
        private KeyboardKeyState GetUpdatedKeyboardKeyState(KeyboardKey keyboardKey)
        {
            bool isReleased = _releasedKeyboardKeys.Contains(keyboardKey);
            bool isPressed = _pressedKeyboardKeys.Contains(keyboardKey);
            KeyboardKeyState previousState = _keyboardKeyStates[(int) keyboardKey];

            if (previousState == KeyboardKeyState.Pressing && isReleased == false)
            {
                return KeyboardKeyState.Pressed;
            }

            if (isPressed && isReleased == false)
            {
                return previousState != KeyboardKeyState.Pressed ? KeyboardKeyState.Pressing : previousState;
            }

            if (isReleased)
            {
                return KeyboardKeyState.Releasing;
            }

            return previousState == KeyboardKeyState.Releasing ? KeyboardKeyState.Released : previousState;
        }
    }
}