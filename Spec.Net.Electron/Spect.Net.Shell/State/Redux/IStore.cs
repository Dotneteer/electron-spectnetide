using System;

namespace Spect.Net.Shell.State.Redux
{
    /// <summary>
    /// Represents a store that encapsulates a state tree and is used to dispatch
    /// actions to update the state tree.
    /// </summary>
    /// <typeparam name="TState">
    /// The state tree type.
    /// </typeparam>
    public interface IStore<out TState>
    {
        /// <summary>
        /// Dispatches the specified actions, and sets
        /// the new state of the store accordingly.
        /// </summary>
        /// <param name="action">Action to dispatch</param>
        /// <returns>
        /// The new state of the store.
        /// </returns>
        TState Dispatch(IReducerAction action);

        /// <summary>
        /// Retrieves the current state of the store.
        /// </summary>
        TState GetState();

        /// <summary>
        /// This event is raised when the state of the store has been changed.
        /// </summary>
        event Action<TState, TState> StateChanged;
    }
}