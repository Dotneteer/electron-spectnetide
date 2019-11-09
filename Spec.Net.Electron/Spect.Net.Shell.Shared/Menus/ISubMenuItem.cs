namespace Spect.Net.Shell.Shared.Menus
{
    /// <summary>
    /// Represents a submenu item
    /// </summary>
    public interface ISubMenuItem : IMenuItem
    {
        /// <summary>
        /// The host menu of this item
        /// </summary>
        IMenu Menu { get; }
    }
}