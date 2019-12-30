using Spect.Net.Shell.Menus;
using System.Collections.Generic;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This class represents the state of the application.
    /// </summary>
    public sealed class AppState
    {
        /// <summary>
        /// The state of the main window.
        /// </summary>
        public WindowState WindowState { get; set; }

        /// <summary>
        /// Indicates if a modal dialog is displayed
        /// </summary>
        public bool IsModelDisplayed { get; set; }

        /// <summary>
        /// Indicates if the main window has the focus.
        /// </summary>
        public bool HasFocus { get; set; }

        /// <summary>
        /// The state of the menu
        /// </summary>
        public MenuState MenuState { get; set; }

        /// <summary>
        /// The intial app state to use when intializing the stores
        /// </summary>
        public static AppState InitialState = new AppState
        {
            WindowState = WindowState.Normal,
            HasFocus = true,
            MenuState = new MenuState
            {
                Menu = new Menu()
                {
                    Items = new List<IMenuItem>
                    {
                        new MenuItem("file")
                        {
                            Label = "&File"
                        },
                        new MenuItem("edit")
                        {
                            Label = "&Edit"
                        },
                        new MenuItem("view")
                        {
                            Label = "&View"
                        }
                    }
                },
                SelectedIndex = -1,
                HighlightAccessKeys = false,
                OpenPanes = new List<MenuPaneInfo>(),
                KeyboardAction = false
            }
        };
    }

    /// <summary>
    /// This class represents the state of the menu
    /// </summary>
    public sealed class MenuState
    {
        /// <summary>
        /// The collection of menu items
        /// </summary>
        public IMenu Menu { get; set; }

        /// <summary>
        /// The index of the pointed menu button in the menu bar
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Indicates if access keys should be highlighted
        /// </summary>
        public bool HighlightAccessKeys { get; set; }

        /// <summary>
        /// Open menu panes
        /// </summary>
        public List<MenuPaneInfo> OpenPanes { get; set; }

        /// <summary>
        /// Indicates if the last action was a keyboard action
        /// </summary>
        public bool KeyboardAction { get; set; }
    }
}