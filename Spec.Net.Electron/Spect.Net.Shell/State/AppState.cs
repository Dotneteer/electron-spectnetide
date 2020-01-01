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
        public AppMenuState MenuState { get; set; }

        /// <summary>
        /// The intial app state to use when intializing the stores
        /// </summary>
        public static AppState InitialState = new AppState
        {
            WindowState = WindowState.Normal,
            HasFocus = true,
            MenuState = new AppMenuState
            {
                AppMenu = null,
                SelectedIndex = -1,
                HighlightAccessKeys = false,
                OpenPanes = new List<MenuPaneInfo>(),
                KeyboardAction = false
            }
        };
    }
}