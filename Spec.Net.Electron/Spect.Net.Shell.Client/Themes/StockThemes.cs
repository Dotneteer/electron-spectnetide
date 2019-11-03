namespace Spect.Net.Shell.Client.Themes
{
    /// <summary>
    /// This class contains default stock themes
    /// </summary>
    public class StockThemes
    {
        /// <summary>
        /// Dark theme properties
        /// </summary>
        public static ThemeProps DarkTheme = new ThemeProps
        {
            AppBackgroundColor = "#202020",
            MenuBarBackgroundColor = "#3c3c3c",
            MenuBarColor = "#f89406",
            StatusBarBackgroundColor = "#007acc",
            StatusBarColor = "#ffffff",
            ActivityBarBackgroundColor = "#3c3c3c",
            SidebarBackgroundColor = "#252526",

            IconDefaultSize = 12,
            IconDefaultFill = "#ffffff",

            MenuActiveBackgroundColor = "#505050",
        };

        /// <summary>
        /// Light theme properties
        /// </summary>
        public static ThemeProps LightTheme = new ThemeProps
        {
            AppBackgroundColor = "#e0e0e0",
            MenuBarBackgroundColor = "c8c8c8",
            MenuBarColor = "#f89406",
            StatusBarBackgroundColor = "#007acc",
            StatusBarColor = "#000000",
            ActivityBarBackgroundColor = "#c8c8c8",
            SidebarBackgroundColor = "#c0c0c0",

            IconDefaultSize = 12,
            IconDefaultFill = "#202020",

            MenuActiveBackgroundColor = "#505050",
        };
    }
}
