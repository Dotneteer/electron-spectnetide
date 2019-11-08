namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// Represents a method that is used to update the state tree.
    /// </summary>
    /// <typeparam name="TState">
    /// The state tree type.
    /// </typeparam>
    /// <param name="previousState">
    /// The previous state tree.
    /// </param>
    /// <param name="action">
    /// The action to be applied to the state tree.
    /// </param>
    /// <returns>
    /// The updated state tree.
    /// </returns>
    public delegate TState Reducer<TState>(TState previousState, IReducerAction action);
}