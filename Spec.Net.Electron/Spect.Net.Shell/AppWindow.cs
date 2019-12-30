using ElectronNET.API;
using ElectronNET.API.Entities;
using Spect.Net.Shell.State;
using Spect.Net.Shell.State.Actions;
using System.Threading.Tasks;

namespace Spect.Net.Shell
{
    /// <summary>
    /// This class represents the main application window
    /// </summary>
    public class AppWindow
    {
        /// <summary>
        /// Gets the singleton instance of this AppWindow
        /// </summary>
        public static AppWindow Instance { get; private set; }

        /// <summary>
        /// Factory method to create the singleton instance
        /// </summary>
        /// <returns></returns>
        public static async Task Create()
        {
            if (Instance != null) return;
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Title = "ZX Spectrum IDE",
                Width = 1152,
                Height = 864,
                Show = true,
                Frame = false
            });
            Instance = new AppWindow(browserWindow);
            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.OnFocus += () => StateStore.Dispatch(new AppGotFocusAction());
            browserWindow.OnBlur += () => StateStore.Dispatch(new AppLostFocusAction());
            browserWindow.OnRestore += () => StateStore.Dispatch(new RestoreWindowAction());
            browserWindow.OnMaximize += () => StateStore.Dispatch(new MaximizeWindowAction());
            browserWindow.OnMinimize += () => StateStore.Dispatch(new MinimizeWindowAction());
            browserWindow.OnUnmaximize += () => StateStore.Dispatch(new RestoreWindowAction());
        }

        /// <summary>
        /// Gets the BrowserWindows that displays this app
        /// </summary>
        public BrowserWindow Window { get; }

        /// <summary>
        /// The private constructor that initializes the singleton
        /// class instance
        /// </summary>
        /// <param name="window">BrowserWindow instance wrapped in</param>
        private AppWindow(BrowserWindow window)
        {
            Window = window;
        }
    }
}