namespace Spect.Net.Shell.Messaging
{
    /// <summary>
    /// This class defines channel names used in the application
    /// </summary>
    public class ChannelNames
    {
        /// <summary>
        /// Convers application state messages from renderer to main
        /// </summary>
        public const string APP_STATE_TO_MAIN = "app-state-to-main";

        /// <summary>
        /// Convers application state messages from main to renderer
        /// </summary>
        public const string APP_STATE_TO_RENDERER = "app-state-to-renderer";
    }
}