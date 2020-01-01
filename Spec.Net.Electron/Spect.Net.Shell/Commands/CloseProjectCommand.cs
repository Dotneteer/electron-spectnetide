using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class CloseProjectCommand : UiMenuItem
    {
        public CloseProjectCommand() :
            base("close-project", PlatformHelper.IsDarwin
                ? "Close Project"
                : "Close project")
        { }

        /// <summary>
        /// Closes the open project
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
