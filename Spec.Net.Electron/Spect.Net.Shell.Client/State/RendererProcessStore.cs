using System;
using System.Collections.Generic;
using Spect.Net.Shell.Client.Messaging;
using Spect.Net.Shell.Shared.State;
using Spect.Net.Shell.Shared.State.Reducers;
using Spect.Net.Shell.Shared.State.Redux;

namespace Spect.Net.Shell.Client.State
{
    /// <summary>
    /// This class implements the state store of the renderer process.
    /// </summary>
    internal class RendererProcessStore
    {
        private static Store<AppState> s_Store;
        static RendererProcessStore()
        {
            Reset();
        }

        /// <summary>
        /// Resets the store (useful for testing)
        /// </summary>
        public static void Reset()
        {
            var initial = new AppState
            {
                WindowState = WindowState.Normal,
                HasFocus = true
            };
            s_Store = new Store<AppState>(
                new List<Reducer<AppState>>
                {
                    WindowStateReducer.Create()
                },
                initial,
                ForwardToMain
            );
        }

        public static AppState Dispatch(IReducerAction action)
            => s_Store.Dispatch(action);

        public static AppState GetState() => s_Store.GetState();

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