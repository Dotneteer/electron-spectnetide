namespace Spect.Net.Shell.State.Redux
{
    /// <summary>
    /// This class is intended to be the base class of all reducer actions that should be
    /// executed both in the main process and in the renderer process.
    /// </summary>
    public abstract class ReducerAction : IReducerAction
    {
        /// <summary>
        /// Instantiates the action.
        /// </summary>
        /// <param name="isLocal">
        /// Sets the IsLocal property (default: false)
        /// </param>
        protected ReducerAction(bool isLocal = false)
        {
            IsLocal = isLocal;
        }

        /// <summary>
        /// This action should be executed only in the renderer process, and must not
        /// be forwarded to the main process.
        /// </summary>
        public bool IsLocal { get; set; }
    }
}