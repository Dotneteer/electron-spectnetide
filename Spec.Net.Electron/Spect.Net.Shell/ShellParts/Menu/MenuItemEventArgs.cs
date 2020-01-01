namespace Spect.Net.Shell.ShellParts.Menu
{
    /// <summary>
    /// Represents event arguments related to menu items
    /// </summary>
    public class MenuItemEventArgs
    {
        public MenuPane Pane { get; set; }
        public int ItemIndex { get; set; }
    }
}
