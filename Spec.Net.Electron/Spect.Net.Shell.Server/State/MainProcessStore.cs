using Spect.Net.Shell.Shared.State;
using Spect.Net.Shell.Shared.State.Reducers;
using Spect.Net.Shell.Shared.State.Redux;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ElectronNET.API;
using Spect.Net.Shell.Client.State;
using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Server.State
{
    /// <summary>
    /// This class implements the state store of the main process.
    /// </summary>
    public static class MainProcessStore
    {
        private static Store<AppState> s_Store;
        static MainProcessStore()
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
                WindowState = WindowState.Normal
            };
            s_Store = new Store<AppState>(
                new List<Reducer<AppState>>
                {
                    WindowStateReducer.Create(WindowStateWorker.Transit)
                },
                initial,
                ForwardToRenderer
            );
        }

        public static AppState Dispatch(IReducerAction action)
            => s_Store.Dispatch(action);

        public static AppState GetState() => s_Store.GetState();

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
                ChannelNames.APP_STATE_FORWARD, 
                message);
            File.WriteAllText("C:\\Temp\\appmessages.txt", $"Message forwarded: {JsonSerializer.Serialize(message)}");
            return true;
        }
    }
}