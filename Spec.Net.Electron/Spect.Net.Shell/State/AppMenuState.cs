using Spect.Net.Shell.Menus;
using System.Collections.Generic;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This class represents the state of the menu
    /// </summary>
    public sealed class AppMenuState
    {
        /// <summary>
        /// The collection of menu items
        /// </summary>
        public UiMenuItem AppMenu { get; set; }

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