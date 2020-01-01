using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class NewProjectCommand : UiMenuItem
    {
        public NewProjectCommand() :
            base("create-project", PlatformHelper.IsDarwin
                ? "New Project..."
                : "&New project...")
        { }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
