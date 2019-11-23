using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Spect.Net.Shell.Messaging;
using Spect.Net.Shell.State;
using Spect.Net.Shell.State.Actions;
using Spect.Net.Shell.State.Redux;

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
            });
            Instance = new AppWindow(browserWindow);
            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.OnFocus += () => MainProcessStore.Dispatch(new AppGotFocusAction());
            browserWindow.OnBlur += () => MainProcessStore.Dispatch(new AppLostFocusAction());
            browserWindow.OnRestore += async () =>
            {
                var action = (await browserWindow.IsMaximizedAsync())
                    ? new MaximizeWindowAction() as IReducerAction
                    : new RestoreWindowAction();
                action.IsLocal = true;
                MainProcessStore.Dispatch(action);
            };
            browserWindow.OnMaximize += () => MainProcessStore.Dispatch(new MaximizeWindowAction
            {
                IsLocal = true
            });
            browserWindow.OnMinimize += () => MainProcessStore.Dispatch(new MinimizeWindowAction
            {
                IsLocal = true
            });
        }

        /// <summary>
        /// Processor handling sample messages
        /// </summary>
        public SampleMessageProcessor SampleMessageProcessor { get; }

        /// <summary>
        /// Processor handling application state messages
        /// </summary>
        public AppActionMessageProcessor AppActionMessageProcessor { get; }

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
            SampleMessageProcessor = new SampleMessageProcessor(window);
            AppActionMessageProcessor = new AppActionMessageProcessor(window);
        }
    }
}