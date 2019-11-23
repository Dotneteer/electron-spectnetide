using System.Collections.Generic;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// Stores the attributes of a menu
    /// </summary>
    public class Menu: IMenu
    {
        public Menu(string id = null)
        {
            Id = id;
        }

        /// <summary>
        /// Menu identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The items of the menu.
        /// </summary>
        public IList<IMenuItem> Items { get; set; }

        /// <summary>
        /// The selected item of the menu, or null, if no item is selected.
        /// </summary>
        public IMenuItem SelectedItem { get; set; }
    }
}