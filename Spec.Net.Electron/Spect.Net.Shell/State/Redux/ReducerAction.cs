namespace Spect.Net.Shell.State.Redux
{
    /// <summary>
    /// This class is intended to be the base class of all reducer actions that should be
    /// executed both in the main process and in the renderer process.
    /// </summary>
    public abstract class ReducerAction : IReducerAction
    {
    }
}