using Spect.Net.Shell.Menus;
using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;
using System;
using System.Collections.Generic;
using System.Linq;

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

                case MenuAltPressedAction _:
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a => {
                                a.HighlightAccessKeys = s.MenuState.SelectedIndex < 0;
                                a.OpenPanes = new List<MenuPaneInfo>();
                                a.KeyboardAction = true;
                            }));

                case MenuAltReleasedAction _:
                    var itemIndex = state.MenuState.SelectedIndex < 0 ? 0 : -1;
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a => {
                                a.SelectedIndex = itemIndex;
                                a.HighlightAccessKeys = itemIndex >= 0;
                                a.KeyboardAction = true;
                            }));

                case MenuPaneClosedAction _:
                    var (ParentPanes, LastPane) = GetOpenMenuPanes(state.MenuState);
                    if (LastPane != null)
                    {
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a => {
                                    a.OpenPanes = ParentPanes;
                                    a.KeyboardAction = true;
                                }));
                    }
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a => {
                                a.SelectedIndex = -1;
                                a.HighlightAccessKeys = false;
                                a.KeyboardAction = true;
                            }));

                case MenuButtonSetAction menuButtonSetAction:
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a => {
                                a.SelectedIndex = menuButtonSetAction.ItemIndex;
                                a.OpenPanes = menuButtonSetAction.Pane != null
                                    ? new List<MenuPaneInfo> { menuButtonSetAction.Pane }
                                    : new List<MenuPaneInfo>();
                                a.KeyboardAction = menuButtonSetAction.KeyboardAction;
                            }));

                default:
                    return state;
            }

            // --- Gets the information about open panes
            (List<MenuPaneInfo> ParentPanes, MenuPaneInfo LastPane) GetOpenMenuPanes(AppMenuState state)
            {
                if (state.OpenPanes.Count == 0)
                {
                    return (new List<MenuPaneInfo>(), null);
                }
                else if (state.OpenPanes.Count == 1)
                {
                    return (new List<MenuPaneInfo>(), state.OpenPanes[0]);
                }
                else
                {
                    return (new List<MenuPaneInfo>(state.OpenPanes.Take(state.OpenPanes.Count - 1)), 
                        state.OpenPanes.Last());
                }
            }

        }
    }
}
