using ElectronNET.API;
using Spect.Net.Shell.Menus;

namespace Spect.Net.Shell.Commands
{
    public class AboutCommand: UiMenuItem
    {
        public AboutCommand() :
            base("show-about", "About ZX Spectrum IDE...")
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
