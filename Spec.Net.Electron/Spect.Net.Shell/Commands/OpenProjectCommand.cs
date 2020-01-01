using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class OpenProjectCommand : UiMenuItem
    {
        public OpenProjectCommand() :
            base("open-project", PlatformHelper.IsDarwin
                ? "Open Project..."
                : "&Open project...")
        { }

        /// <summary>
        /// Opens an existing project
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
