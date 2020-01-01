using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowDevToolsCommand : UiMenuItem
    {
        public ShowDevToolsCommand() :
            base("show-dev-tools", PlatformHelper.IsDarwin
                    ? "Toggle Developer Tools"
                    : "&Toggle developer tools")
        { }

        /// <summary>
        /// Shows/hides the developer tools panel
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
