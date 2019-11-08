namespace Spect.Net.Shell.Shared.State
{
    /// <summary>
    /// This class represents the state of the application
    /// </summary>
    public sealed class AppState
    {
        public WindowState WindowState { get; set; }
        public bool HasFocus { get; set; }
    }
}