using ElectronNET.API.Entities;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// This class defines a menu item handled by the Electron Shell.
    /// </summary>
    public class ElectronShellMenuItem : UiMenuItem
    {
        public ElectronShellMenuItem(MenuRole? role) : base(null, null, role) { }
    }
}
