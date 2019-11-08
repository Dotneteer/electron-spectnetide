namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// This markup interface represents an action.
    /// </summary>
    public interface IReducerAction
    {
        /// <summary>
        /// This action should be executed only in the renderer process.
        /// </summary>
        bool IsLocal { get; set; }
    }
}