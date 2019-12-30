using System;
using System.Collections.Generic;
using Spect.Net.Shell.State.Reducers;
using Spect.Net.Shell.State.Redux;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This static class implements the state store of the app.
    /// </summary>
    /// <remarks>
    /// This object holds the single truth about application state.
    /// </remarks>
    public static class StateStore
    {
        private static Store<AppState> s_Store;

        /// <summary>
        /// Instantiates the singleton instance of the class using the
        /// Reset method.
        /// </summary>
        static StateStore()
        {
            Reset();
        }

        /// <summary>
        /// Resets the store (useful for testing)
        /// </summary>
        public static void Reset()
        {
            s_Store = new Store<AppState>(
                // --- Action reducers
                new List<Reducer<AppState>>
                {
                    WindowStateReducer.Create(WindowStateWorker.Transit)
                },

                AppState.InitialState
            );
        }

        /// <summary>
        /// Retrieves the current state of the store.
        /// </summary>
        public static AppState GetState() => s_Store.GetState();

        /// <summary>
        /// Dispatches the specified actions, and sets
        /// the new state of the store accordingly.
        /// </summary>
        /// <param name="action">Action to dispatch</param>
        /// <returns>
        /// The new state of the store.
        /// </returns>
        public static AppState Dispatch(IReducerAction action)
            => s_Store.Dispatch(action);

        /// <summary>
        /// This event is raised when the store's state has changed.
        /// The first argument of the event method is the previous app state;
        /// the second argument is the new state.
        /// </summary>
        public static event Action<AppState,AppState> StateChange
        {
            add => s_Store.StateChanged += value;
            remove => s_Store.StateChanged -= value;
        }
    }
}