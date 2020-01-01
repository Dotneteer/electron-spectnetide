using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowRegistersCommand : UiMenuItem
    {
        public ShowRegistersCommand() :
            base("show-registers", PlatformHelper.IsDarwin
                    ? "Z80 Registers"
                    : "Z80 registers")
        { }

        /// <summary>
        /// Shows the Z80 Registers window
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
