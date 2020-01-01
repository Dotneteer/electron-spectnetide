using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowDisassemblyCommand: UiMenuItem
    {
        public ShowDisassemblyCommand() :
            base("show-disassembly", PlatformHelper.IsDarwin
                    ? "Z80 Disassembly"
                    : "Z80 disassembly")
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
