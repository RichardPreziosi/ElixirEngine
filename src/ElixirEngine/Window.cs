using System;
using System.Drawing;
using System.Numerics;
using ElixirEngine.Exceptions;

namespace ElixirEngine
{
    /// <inheritdoc cref="IWindow" />
    internal class Window : IWindow, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Window" /> class.
        /// </summary>
        /// <exception cref="InitializationException">
        ///     Thrown when SDL2 cannot be initialized.
        /// </exception>
        public Window()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_JOYSTICK | SDL.SDL_INIT_GAMECONTROLLER |
                             SDL.SDL_INIT_HAPTIC) <
                0)
            {
                throw new InitializationException($"Unable to initialize SDL. Error: {SDL.SDL_GetError()}");
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            SDL.SDL_DestroyWindow(Handle);
            SDL.SDL_Quit();
        }

        /// <inheritdoc />
        public event Action Closing;

        /// <inheritdoc />
        public event Action<bool> FocusChanged;

        /// <inheritdoc />
        public event Action<bool> FullscreenChanged;

        /// <inheritdoc />
        public event Action MouseEntered;

        /// <inheritdoc />
        public event Action MouseLeft;

        /// <inheritdoc />
        public event Action<WindowOrientation> OrientationChanged;

        /// <inheritdoc />
        public event Action<Size> SizeChanged;

        /// <inheritdoc />
        public event Action<bool> VisibilityChanged;

        /// <inheritdoc />
        public Color BackgroundColor { get; set; }

        /// <inheritdoc />
        public IntPtr Handle { get; private set; }

        /// <inheritdoc />
        public bool IsClosing { get; private set; }

        /// <inheritdoc />
        public bool IsFocused { get; private set; }

        /// <inheritdoc />
        public bool IsFullscreen { get; }

        /// <inheritdoc />
        public bool IsVisible { get; private set; }

        /// <inheritdoc />
        public WindowOrientation Orientation { get; private set; }

        /// <inheritdoc />
        public Vector2 Position { get; set; }

        /// <inheritdoc />
        public Size Size { get; private set; }

        /// <inheritdoc />
        public string Title
        {
            get => SDL.SDL_GetWindowTitle(Handle);
            set => SDL.SDL_SetWindowTitle(Handle, value);
        }

        /// <inheritdoc />
        public void CloseAfterFrame()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetFullscreen(Size displaySize)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SetWindowed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Opens the window.
        /// </summary>
        public void Open()
        {
            Handle = SDL.SDL_CreateWindow(
                "ElixirEngine Game",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                1020,
                800,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (Handle == IntPtr.Zero)
            {
                Console.WriteLine("Unable to create a window. Error: {0}", SDL.SDL_GetError());
            }
        }

        /// <summary>
        ///     Processes <see cref="SDL.SDL_WindowEvent" /> type events.
        /// </summary>
        /// <param name="windowEvent">
        ///     The <see cref="SDL.SDL_WindowEvent" /> to process.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if the provided <see cref="SDL.SDL_EventType" /> cannot be handled.
        /// </exception>
        public void ProcessEvent(SDL.SDL_WindowEvent windowEvent)
        {
            switch (windowEvent.windowEvent)
            {
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                    OnVisibilityChanged(true);
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                    OnVisibilityChanged(false);
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                    Position = new Vector2(windowEvent.data1, windowEvent.data2);
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                    OnSizeChanged(new Size(windowEvent.data1, windowEvent.data2));
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                    OnMouseEntered();
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                    OnMouseLeft();
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                    OnFocusChanged(true);
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                    OnFocusChanged(false);
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                    OnClosing();
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_NONE:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIT_TEST:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Called when the window is closing.
        /// </summary>
        private void OnClosing()
        {
            IsClosing = true;
            Closing?.Invoke();
        }

        /// <summary>
        ///     Called when the window's focus changes.
        /// </summary>
        /// <param name="isFocused">
        ///     Whether the window is focused or not.
        /// </param>
        private void OnFocusChanged(bool isFocused)
        {
            FocusChanged?.Invoke(IsFocused = isFocused);
        }

        /// <summary>
        ///     Called when the mouse enters the window.
        /// </summary>
        private void OnMouseEntered()
        {
            MouseEntered?.Invoke();
        }

        /// <summary>
        ///     Called when the mouse leaves the window.
        /// </summary>
        private void OnMouseLeft()
        {
            MouseLeft?.Invoke();
        }

        /// <summary>
        ///     Called when the window's size changes.
        /// </summary>
        /// <param name="size">
        ///     The new size of the window.
        /// </param>
        private void OnSizeChanged(Size size)
        {
            Size = size;

            WindowOrientation newOrientation = size.Width > size.Height
                ? WindowOrientation.Landscape
                : WindowOrientation.Portrait;

            SizeChanged?.Invoke(size);

            if (newOrientation == Orientation)
            {
                return;
            }

            OrientationChanged?.Invoke(Orientation = newOrientation);
        }

        /// <summary>
        ///     Called when the window's visibility changes.
        /// </summary>
        /// <param name="isVisible">
        ///     Whether the window is visible or not.
        /// </param>
        private void OnVisibilityChanged(bool isVisible)
        {
            VisibilityChanged?.Invoke(IsVisible = isVisible);
        }
    }
}