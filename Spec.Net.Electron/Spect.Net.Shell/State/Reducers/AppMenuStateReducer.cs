using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;
using System;

namespace Spect.Net.Shell.State.Reducers
{
    public static class AppMenuStateReducer
    {
        private static Action<AppMenuState> s_Worker;

        /// <summary>
        /// Creates the reducer
        /// </summary>
        /// <param name="worker">Optional worker</param>
        /// <returns>Reducer function</returns>
        public static Reducer<AppState> Create(Action<AppMenuState> worker = null)
        {
            s_Worker = worker;
            return ReduceAppMenuState;
        }

        /// <summary>
        /// Implements the reducer function
        /// </summary>
        /// <param name="state">Current state</param>
        /// <param name="action">Action</param>
        /// <returns></returns>
        private static AppState ReduceAppMenuState(AppState state, IReducerAction action)
        {
            if (!(action is MenuActionBase menuAction)) return state;
            switch (menuAction)
            {
                case SetAppMenuAction setAppMenuAction:
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a => a.AppMenu = setAppMenuAction.AppMenu));
                default:
                    return state;
            }
        }
    }
}
