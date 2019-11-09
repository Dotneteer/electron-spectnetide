namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// This interface represents an reducer action that may change the state of the application.
    /// </summary>
    public interface IReducerAction
    {
        /// <summary>
        /// This action should be executed only in the renderer process, and must not
        /// be forwarded to the main process.
        /// </summary>
        bool IsLocal { get; set; }
    }
}