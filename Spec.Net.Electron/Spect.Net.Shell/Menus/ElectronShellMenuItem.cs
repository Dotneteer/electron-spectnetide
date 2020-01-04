using ElectronNET.API.Entities;

namespace Spect.Net.Shell.Menus
{
    /// <summary>
    /// This class defines a menu item handled by the Electron Shell.
    /// </summary>
    public class ElectronShellMenuItem : UiMenuItem
    {
        public ElectronShellMenuItem(MenuRole? role, string label = null, string accelerator = null) 
            : base(null, label, role) 
        { 
            if (!string.IsNullOrWhiteSpace(accelerator))
            {
                WithAccelerator(accelerator);
            }
        }
    }
}
