using System;

namespace Spect.Net.Shell.Shared.State.Redux
{
    /// <summary>
    /// Represents a store that encapsulates a state tree and is used to dispatch actions to update the
    /// state tree.
    /// </summary>
    /// <typeparam name="TState">
    /// The state tree type.
    /// </typeparam>
    public interface IStore<out TState>
    {
        /// <summary>
        /// Dispatches an action to the store.
        /// </summary>
        /// <param name="action">The action to dispatch.</param>
        /// <returns>
        /// The new state of the store after dispatch
        /// </returns>
        TState Dispatch(IReducerAction action);

        /// <summary>
        /// Gets the current state tree.
        /// </summary>
        /// <returns>
        /// The current state tree.
        /// </returns>
        TState GetState();

        /// <summary>
        /// This event is raised when the state of the store has been changed.
        /// </summary>
        event Action<TState> StateChanged;
    }
}