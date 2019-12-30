namespace Spect.Net.Shell.State
{
    /// <summary>
    /// The Transit method of this class carries out the side effects of
    /// the window state changes.
    /// </summary>
    public static class WindowStateWorker
    {
        /// <summary>
        /// Carries out the action associated with transition
        /// </summary>
        /// <param name="state">New window state</param>
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
                case WindowState.ToClose:
                    browserWindow.Close();
                    break;
            }
        }
    }
}