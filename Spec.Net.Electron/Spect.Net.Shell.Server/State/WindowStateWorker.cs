using Spect.Net.Shell.Shared.State;

namespace Spect.Net.Shell.Server.State
{
    public static class WindowStateWorker
    {
        /// <summary>
        /// Carries out the action associated with transition
        /// </summary>
        /// <param name="state"></param>
        public static void Transit(WindowState state)
        {
            var browserWindow = AppWindow.Instance.Window;
            switch (state)
            {
                case WindowState.Normal:
                    browserWindow.Restore();
                    break;
                case WindowState.Maximized:
                    browserWindow.Maximize();
                    break;
                case WindowState.Minimized:
                    browserWindow.Minimize();
                    break;
            }
        }
    }
}