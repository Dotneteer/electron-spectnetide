using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Spect.Net.Shell.Client.State;
using Spect.Net.Shell.Server.Messaging;
using Spect.Net.Shell.Server.State;
using Spect.Net.Shell.Shared.State.Actions;

namespace Spect.Net.Shell.Server
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

        public static async Task Create()
        {
            if (Instance != null) return;
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Title = "ZX Spectrum IDE",
                Width = 1152,
                Height = 864,
                Show = true,
            });
            Instance = new AppWindow(browserWindow);
            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.OnFocus += () => MainProcessStore.Dispatch(new AppGotFocusAction());
            browserWindow.OnBlur += () => MainProcessStore.Dispatch(new AppLostFocusAction());
        }

        public SampleMessageProcessor SampleMessageProcessor { get; }

        public AppActionMessageProcessor AppActionMessageProcessor { get; }

        /// <summary>
        /// Gets the BrowserWindows that displays this app
        /// </summary>
        public BrowserWindow Window { get; }

        private AppWindow(BrowserWindow window)
        {
            Window = window;
            SampleMessageProcessor = new SampleMessageProcessor(window);
            AppActionMessageProcessor = new AppActionMessageProcessor(window);
        }
    }
}