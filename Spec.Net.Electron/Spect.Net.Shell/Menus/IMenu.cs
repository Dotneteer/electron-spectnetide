using System.Collections.Generic;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// This class represents a menu.
    /// </summary>
    /// <remarks>
    /// For the root menu the ID will be undefined. For all other menus it
    /// will be the same as the id of the submenu item which owns this menu.
    ///
    /// +---------------------------+
    /// | Root menu(id: undefined) |
    /// +---------------------------+  +--------------------------+
    /// |  File(id File)           +--> File menu(id: File)     |
    /// +---------------------------+  +--------------------------+
    /// |  Edit(id Edit)           |  |  Open(id File.Open)     |
    /// +---------------------------+  +--------------------------+
    ///                                |  Close(id File.Close)   |
    ///                                +--------------------------+
    /// </remarks>
    public interface IMenu
    {
        /// <summary>
        /// Menu identifier
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The items of the menu.
        /// </summary>
        IList<IMenuItem> Items { get; }

        /// <summary>
        /// The selected item of the menu, or null, if no item is selected.
        /// </summary>
        IMenuItem SelectedItem { get; }
    }
}