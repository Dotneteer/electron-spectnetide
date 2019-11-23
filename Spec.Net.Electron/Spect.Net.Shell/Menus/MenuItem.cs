namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// Stores the attributes of a menu item.
    /// </summary>
    public class MenuItem: IMenuItem
    {
        /// <summary>
        /// Is the item enabled?
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Is the item visible?
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Is the item checked?
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// The menu item's label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Uique identifier of the menu item
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The accelerator key of the menu item
        /// </summary>
        public string Accelerator { get; set; }

        /// <summary>
        /// The access key of the menu item
        /// </summary>
        public string AccessKey { get; set; }

        public MenuItem(string id)
        {
            Id = id;
            Enabled = true;
            Visible = true;
            Checked = false;
        }
    }
}