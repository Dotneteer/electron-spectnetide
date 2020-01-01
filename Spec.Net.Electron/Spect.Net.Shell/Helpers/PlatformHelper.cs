using System.Runtime.InteropServices;

namespace Spect.Net.Shell.Helpers
{
    /// <summary>
    /// This class provides helper methods to test platforms
    /// </summary>
    public static class PlatformHelper
    {
        /// <summary>
        /// tests if the current platform is Windows
        /// </summary>
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// tests if the current platform is Linux
        /// </summary>
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// tests if the current platform is Darwin
        /// </summary>
        public static bool IsDarwin => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}
