namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// Represents the information we store about a menu
    /// item
    /// </summary>
    public interface IMenuItem : IMenuItemState
    {
        /// <summary>
        /// Uique identifier of the menu item
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// The accelerator key of the menu item
        /// </summary>
        string Accelerator { get; }

        /// <summary>
        /// The access key of the menu item
        /// </summary>
        string AccessKey { get; }
    }
}