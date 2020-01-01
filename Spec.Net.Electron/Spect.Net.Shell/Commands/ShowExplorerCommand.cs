using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class ShowExplorerCommand : UiMenuItem
    {
        public ShowExplorerCommand() :
            base("show-explorer", PlatformHelper.IsDarwin
                    ? "Show Explorer"
                    : "Show explorer")
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
