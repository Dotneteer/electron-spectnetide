using System.Collections.Generic;
using Spect.Net.Shell.Menus;

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
        /// Indicates if the main window has the focus.
        /// </summary>
        public bool HasFocus { get; set; }

        /// <summary>
        /// The current menu to display
        /// </summary>
        public Menu Menu { get; set; }

        /// <summary>
        /// The intial app state to use when intializing the stores
        /// </summary>
        public static AppState InitialState = new AppState
        {
            WindowState = WindowState.Normal,
            HasFocus = true,
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
            }
        };
    }
}