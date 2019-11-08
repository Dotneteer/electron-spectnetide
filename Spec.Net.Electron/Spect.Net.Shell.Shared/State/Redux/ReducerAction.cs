namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// This class is intended to be the base class of all reducer actions that should be
    /// executed both in the main process and in the renderer process.
    /// </summary>
    public abstract class ReducerAction : IReducerAction
    {
        protected ReducerAction(bool isLocal = false)
        {
            IsLocal = isLocal;
        }

        /// <summary>
        /// This action should be executed only in the renderer process.
        /// </summary>
        public bool IsLocal { get; set; }
    }
}