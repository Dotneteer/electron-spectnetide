using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowSpectrumEmulatorCommand : UiMenuItem
    {
        public ShowSpectrumEmulatorCommand() :
            base("show-spectrum-emulator", PlatformHelper.IsDarwin
                    ? "ZX Spectrum Emulator"
                    : "ZX Spectrum emulator")
        { }

        /// <summary>
        /// Shows/hides the Explorer pane
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
