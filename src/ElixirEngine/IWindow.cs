namespace ElixirEngine
{
    using System;
    using System.Drawing;
    using System.Numerics;

    /// <summary>
    ///     Represents the view that makes up an application's user interface.
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        ///     Occurs when the window is closing.
        /// </summary>
        event Action Closing;

        /// <summary>
        ///     Occurs when the window's focus changes.
        /// </summary>
        event Action<bool> FocusChanged;

        /// <summary>
        ///     Occurs when the window enters or exits full screen mode.
        /// </summary>
        event Action<bool> FullscreenChanged;

        /// <summary>
        ///     Occurs when the mouse enters the window.
        /// </summary>
        event Action MouseEntered;

        /// <summary>
        ///     Occurs when the mouse leaves the window.
        /// </summary>
        event Action MouseLeft;

        /// <summary>
        ///     Occurs when the window's orientation changes.
        /// </summary>
        event Action<WindowOrientation> OrientationChanged;
        
        /// <summary>
        ///     Occurs when the window's size changes.
        /// </summary>
        event Action<Size> SizeChanged;

        /// <summary>
        ///     Occurs when the window's visibility changes.
        /// </summary>
        event Action<bool> VisibilityChanged;

        /// <summary>
        ///     Gets or sets the window's background color.
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        ///     Gets the window's handle.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        ///     Gets whether the window is closing.
        /// </summary>
        bool IsClosing { get; }

        /// <summary>
        ///     Gets whether the window is focused.
        /// </summary>
        bool IsFocused { get; }

        /// <summary>
        ///     Gets whether the window is in full screen mode.
        /// </summary>
        bool IsFullscreen { get; }

        /// <summary>
        ///     Gets whether the window is visible.
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        ///     Gets the window's orientation.
        /// </summary>
        WindowOrientation Orientation { get; }

        /// <summary>
        ///     Gets or sets the window's position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        ///     Gets the window's viewport size.
        /// </summary>
        Size Size { get; }

        /// <summary>
        ///     Gets or sets the window's title.
        /// </summary>
        string Title { get; set; }
        
        /// <summary>
        ///     Queues the window to close after the current frame.
        /// </summary>
        void CloseAfterFrame();

        /// <summary>
        ///     Sets the window to full screen mode.
        /// </summary>
        /// <param name="displaySize">
        ///     The size the window's display should be.
        /// </param>
        void SetFullscreen(Size displaySize);

        /// <summary>
        ///     Sets the window to windowed mode.
        /// </summary>
        void SetWindowed();
    }
}