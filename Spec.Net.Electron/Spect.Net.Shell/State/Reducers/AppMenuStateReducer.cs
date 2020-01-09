using Spect.Net.Shell.Menus;
using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;
using System.Collections.Generic;
using System.Linq;

namespace Spect.Net.Shell.State.Reducers
{
    public static class AppMenuStateReducer
    {
        /// <summary>
        /// Creates the reducer
        /// </summary>
        /// <returns>Reducer function</returns>
        public static Reducer<AppState> Create()
        {
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
                            a =>
                            {
                                a.HighlightAccessKeys = s.MenuState.SelectedIndex < 0;
                                a.OpenPanes = new List<MenuPaneInfo>();
                                a.KeyboardAction = true;
                            }));

                case MenuAltReleasedAction _:
                    var itemIndex = state.MenuState.SelectedIndex < 0 ? 0 : -1;
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a =>
                            {
                                a.SelectedIndex = itemIndex;
                                a.HighlightAccessKeys = itemIndex >= 0;
                                a.KeyboardAction = true;
                            }));

                case MenuPaneClosedAction _:
                    {
                        var (parentPanes, lastPane) = GetOpenMenuPanes(state.MenuState);
                        if (lastPane != null)
                        {
                            return state.Assign(
                                s => s.MenuState = s.MenuState.Assign(
                                    a =>
                                    {
                                        a.OpenPanes = parentPanes;
                                        a.KeyboardAction = true;
                                    }));
                        }
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.SelectedIndex = -1;
                                    a.HighlightAccessKeys = false;
                                    a.KeyboardAction = true;
                                }));

                    }

                case MenuButtonSetAction menuButtonSetAction:
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a =>
                            {
                                a.SelectedIndex = menuButtonSetAction.ItemIndex;
                                a.OpenPanes = menuButtonSetAction.Pane != null
                                    ? new List<MenuPaneInfo> { menuButtonSetAction.Pane }
                                    : new List<MenuPaneInfo>();
                                a.KeyboardAction = menuButtonSetAction.KeyboardAction;
                            }));

                case MenuItemDownAction _:
                    {
                        var (parentPanes, lastPane) = GetOpenMenuPanes(state.MenuState);
                        if (lastPane == null) return state;
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = parentPanes.Concat(
                                        new List<MenuPaneInfo> {
                                            lastPane.Assign(lp => lp.SelectedIndex = GetNextMenuItemIndex(lp, 1))
                                        }).ToList();
                                }));
                    }

                case MenuItemUpAction _:
                    {
                        var (parentPanes, lastPane) = GetOpenMenuPanes(state.MenuState);
                        if (lastPane == null) return state;
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = parentPanes.Concat(
                                        new List<MenuPaneInfo> {
                                            lastPane.Assign(lp => lp.SelectedIndex = GetNextMenuItemIndex(lp, -1))
                                        }).ToList();
                                }));
                    }

                case MenuPaneOpenAction menuPaneOpenAction:
                    {
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = a.OpenPanes.Concat(
                                        new List<MenuPaneInfo> { menuPaneOpenAction.Pane }).ToList();
                                    a.KeyboardAction = menuPaneOpenAction.KeyboardAction;
                                }));
                    }

                case MenuCloseAllPanesAction _:
                    return state.Assign(
                        s => s.MenuState = s.MenuState.Assign(
                            a =>
                            {
                                a.SelectedIndex = -1;
                                a.HighlightAccessKeys = false;
                                a.OpenPanes = new List<MenuPaneInfo>();
                                a.KeyboardAction = false;
                            }));

                case MenuItemSelectAction menuItemSelectAction:
                    {
                        var (parentPanes, lastPane) = GetOpenMenuPanes(state.MenuState);
                        if (lastPane == null) return state;
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = parentPanes.Concat(
                                        new List<MenuPaneInfo>
                                        {
                                            lastPane.Assign(lp => lp.SelectedIndex = menuItemSelectAction.Index)
                                        }).ToList();
                                    a.KeyboardAction = true;
                                }));
                    }

                case MenuButtonClickAction menuButtonClickAction:
                    {
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.SelectedIndex = menuButtonClickAction.Index;
                                    a.OpenPanes = new List<MenuPaneInfo>
                                        {
                                      menuButtonClickAction.Pane
                                        };
                                    a.KeyboardAction = false;
                                }));
                    }

                case MenuKeepPaneAction menuKeepPaneAction:
                    {
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = a.OpenPanes.Take(menuKeepPaneAction.PaneIndex + 1).ToList();
                                    a.KeyboardAction = false;
                                }));
                    }

                case MenuItemPointAction menuItemPointAction:
                    {
                        var panes = state.MenuState.OpenPanes.ToList();
                        var paneIndex = menuItemPointAction.PaneIndex;
                        if (paneIndex < 0 || paneIndex >= panes.Count)
                        {
                            return state;
                        }
                        var pane = panes[menuItemPointAction.PaneIndex];
                        var selectedIndex = menuItemPointAction.ItemIndex >= 0
                            ? pane.Items.Flatten().ToArray()[menuItemPointAction.ItemIndex].Enabled
                                ? menuItemPointAction.ItemIndex
                                : -1
                            : -1;
                        panes[menuItemPointAction.PaneIndex] = new MenuPaneInfo
                        {
                            Items = pane.Items,
                            ParentIndex = pane.ParentIndex,
                            LeftPos = pane.LeftPos,
                            TopPos = pane.TopPos,
                            SelectedIndex = selectedIndex
                        };
                        return state.Assign(
                            s => s.MenuState = s.MenuState.Assign(
                                a =>
                                {
                                    a.OpenPanes = panes;
                                    a.KeyboardAction = false;
                                }));
                    }

                default:
                    return state;
            }

            // --- Gets the information about open panes
            static (List<MenuPaneInfo> ParentPanes, MenuPaneInfo LastPane) GetOpenMenuPanes(AppMenuState state)
            {
                return state.OpenPanes.Count switch
                {
                    0 => (new List<MenuPaneInfo>(), null),
                    1 => (new List<MenuPaneInfo>(), state.OpenPanes[0]),
                    _ => (new List<MenuPaneInfo>(state.OpenPanes.Take(state.OpenPanes.Count - 1)),
                        state.OpenPanes.Last())
                };
            }

            // --- Gets the next menu item index according to the specified step direction
            static int GetNextMenuItemIndex(MenuPaneInfo pane, int step)
            {
                var items = pane.Items.Flatten().ToArray();
                var count = items.Length;
                var selectedIndex = pane.SelectedIndex;
                for (var i = 1; i < count; i++)
                {
                    var nextItemIndex = (selectedIndex + step * i + count) % count;
                    var nextItem = items[nextItemIndex];
                    if (!nextItem.Visible || !nextItem.Enabled) continue;

                    selectedIndex = nextItemIndex;
                    break;
                }
                return selectedIndex;
            }

        }
    }
}
