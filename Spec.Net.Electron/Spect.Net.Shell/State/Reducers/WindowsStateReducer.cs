using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;
using System;

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
            if (!(action is WindowActionBase windowAction)) return state;
            switch (windowAction)
            {
                case MaximizeWindowAction _:
                    if (state.WindowState != WindowState.Maximized)
                    {
                        s_Worker?.Invoke(WindowState.Maximized);
                        return state.Assign(s => s.WindowState = WindowState.Maximized);
                    }
                    return state;
                case MinimizeWindowAction _:
                    if (state.WindowState != WindowState.Minimized)
                    {
                        s_Worker?.Invoke(WindowState.Minimized);
                        return state.Assign(s => s.WindowState = WindowState.Minimized);
                    }
                    return state;
                case RestoreWindowAction _:
                    if (state.WindowState != WindowState.Normal)
                    {
                        s_Worker?.Invoke(WindowState.Normal);
                        return state.Assign(s => s.WindowState = WindowState.Normal);
                    }
                    return state;
                case AppGotFocusAction _:
                    return state.Assign(s => s.HasFocus = true);
                case AppLostFocusAction _:
                    return state.Assign(s => s.HasFocus = false);
                case CloseWindowAction _:
                    s_Worker?.Invoke(WindowState.ToClose);
                    return state.Assign(s => s.WindowState = WindowState.ToClose);
                default:
                    return state;
            }
        }
    }
}