namespace Spect.Net.Shell.Shared.Menus
{
    /// <summary>
    /// Represents the state of a menu item
    /// </summary>
    public interface IMenuItemState
    {
        /// <summary>
        /// Is the item enabled?
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Is the item visible?
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// Is the item checked?
        /// </summary>
        bool Checked { get; }

        /// <summary>
        /// The menu item's label
        /// </summary>
        string Label { get; }
    }
}