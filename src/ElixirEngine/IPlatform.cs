namespace ElixirEngine
{
    /// <summary>
    ///     Represents the application's native platform implementation.
    /// </summary>
    public interface IPlatform
    {
        bool ShowCursor { get; set; }

        void CopyToClipboard(string text);

        void SetCursorIcon(string iconFilePath);

        string ShowMessageBox(string title, string message, string[] buttons);
    }
}