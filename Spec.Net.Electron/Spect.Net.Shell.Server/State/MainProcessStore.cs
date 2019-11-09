using Spect.Net.Shell.Shared.State;
using Spect.Net.Shell.Shared.State.Reducers;
using Spect.Net.Shell.Shared.State.Redux;
using System;
using System.Collections.Generic;
using ElectronNET.API;
using Spect.Net.Shell.Client.State;
using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Server.State
{
    /// <summary>
    /// This static class implements the state store of the main process.
    /// </summary>
    /// <remarks>
    /// This object holds the single truth about application state. Firsts,
    /// (except local actions) this store displatches the actions, and then
    /// actions are conveyed to the store in the renderer process.
    /// </remarks>
    public static class MainProcessStore
    {
        private static Store<AppState> s_Store;

        /// <summary>
        /// Instantiates the singleton instance of the class using the
        /// Reset method.
        /// </summary>
        static MainProcessStore()
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

                AppState.InitialState,

                // --- This middleware forwards actions to the renderer
                ForwardToRenderer
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
        private static bool ForwardToRenderer<TState>(IStore<TState> store, IReducerAction action)
        {
            action.IsLocal = true;
            var message = new AppActionMessage(action.GetType().AssemblyQualifiedName, action);
            Electron.IpcMain.Send(AppWindow.Instance.Window, 
                ChannelNames.APP_STATE_TO_RENDERER, 
                message);
            return true;
        }
    }
}