using ElectronNET.API;
using Spect.Net.Shell.Helpers;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class OptionsCommand : UiMenuItem
    {
        public OptionsCommand() :
            base("show-preferences", PlatformHelper.IsDarwin
                    ? "Preferences..."
                    : "&Options...")
        { }

        /// <summary>
        /// Displays the Options dialog
        /// </summary>
        /// <param name="window">Host browser window</param>
        public override void OnExecute(BrowserWindow window)
        {
        }
    }
}
