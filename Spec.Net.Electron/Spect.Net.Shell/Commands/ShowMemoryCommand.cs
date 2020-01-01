using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowMemoryCommand : UiMenuItem
    {
        public ShowMemoryCommand() :
            base("show-memory", PlatformHelper.IsDarwin
                    ? "ZX Spectrum Memory"
                    : "ZX Spectrum memory")
        { }

        /// <summary>
        /// Shows the Memory tool window
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
