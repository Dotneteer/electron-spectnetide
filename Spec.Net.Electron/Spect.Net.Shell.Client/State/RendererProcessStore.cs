using System;
using System.Collections.Generic;
using Spect.Net.Shell.Client.Messaging;
using Spect.Net.Shell.Shared.State;
using Spect.Net.Shell.Shared.State.Reducers;
using Spect.Net.Shell.Shared.State.Redux;

namespace Spect.Net.Shell.Client.State
{
    /// <summary>
    /// This static class implements the state store of the renderer process.
    /// </summary>
    /// <remarks>
    /// This object holds a replica of the application state stored in the main
    /// process. Local actions are dispatched only in this store. Nonetheless, other
    /// actions are send to the main process, and after the store dispatch there,
    /// they are sent back to this store.
    /// </remarks>
    internal class RendererProcessStore
    {
        private static Store<AppState> s_Store;

        /// <summary>
        /// Instantiates the singleton instance of the class using the
        /// Reset method.
        /// </summary>
        static RendererProcessStore()
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
                    WindowStateReducer.Create()
                },

                AppState.InitialState,

                // --- Forwards non-local messages to the main process
                ForwardToMain
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
        public static event Action<AppState, AppState> StateChange
        {
            add => s_Store.StateChanged += value;
            remove => s_Store.StateChanged -= value;
        }

        /// <summary>
        /// This method implements the middleware that forwards the main process store
        /// state change action to the store of the renderer process
        /// </summary>
        /// <param name="store">
        /// The IStore this middleware is to be used on.
        /// </param>
        /// <param name="action">Action to be handled</param>
        /// <returns>
        /// A boolean that represents if the middleware chain should be processed (true),
        /// or abandoned (false)
        /// </returns>
        private static bool ForwardToMain<TState>(IStore<TState> store, IReducerAction action)
        {
            if (action.IsLocal) return true;
            Messenger.SendAppAction(action);
            return false;
        }
    }
}