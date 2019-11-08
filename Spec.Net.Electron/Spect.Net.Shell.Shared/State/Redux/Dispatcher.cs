namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// Represents a method that dispatches an action.
    /// </summary>
    /// <typeparam name="TState">Type of state</typeparam>
    /// <param name="action">
    /// The action to dispatch.
    /// </param>
    /// <returns>
    /// The new state after dispatch
    /// </returns>
    public delegate TState Dispatcher<out TState>(IReducerAction action);
}