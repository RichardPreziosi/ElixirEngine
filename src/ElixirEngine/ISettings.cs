namespace ElixirEngine
{
    /// <summary>
    ///     Represents the settings for the application.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     Gets or sets the target frame rate.
        /// </summary>
        float TargetFrameRate { get; set; }
    }
}