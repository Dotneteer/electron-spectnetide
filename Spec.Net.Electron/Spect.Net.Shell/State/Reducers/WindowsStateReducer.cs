using System;
using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;

namespace Spect.Net.Shell.State.Reducers
{
    /// <summary>
    /// This class represents the window state reducer. 
    /// </summary>
    public static class WindowStateReducer
    {
        private static Action<WindowState> s_Worker;

        /// <summary>
        /// Creates the reducer
        /// </summary>
        /// <param name="worker">Optional worker</param>
        /// <returns>Reducer function</returns>
        public static Reducer<AppState> Create(Action<WindowState> worker = null)
        {
            s_Worker = worker;
            return ReduceWindowState;
        }

        /// <summary>
        /// Implements the reducer function
        /// </summary>
        /// <param name="state">Current state</param>
        /// <param name="action">Action</param>
        /// <returns></returns>
        private static AppState ReduceWindowState(AppState state, IReducerAction action)
        {
            switch (action)
            {
                case MaximizeWindowAction _:
                    s_Worker?.Invoke(WindowState.Maximized);
                    return state.Assign(s => s.WindowState = WindowState.Maximized);
                case MinimizeWindowAction _:
                    s_Worker?.Invoke(WindowState.Minimized);
                    return state.Assign(s => s.WindowState = WindowState.Minimized);
                case RestoreWindowAction _:
                    s_Worker?.Invoke(WindowState.Normal);
                    return state.Assign(s => s.WindowState = WindowState.Normal);
                case AppGotFocusAction _:
                    return state.Assign(s => s.HasFocus = true);
                case AppLostFocusAction _:
                    return state.Assign(s => s.HasFocus = false);
                default:
                    return state;
            }
        }
    }
}