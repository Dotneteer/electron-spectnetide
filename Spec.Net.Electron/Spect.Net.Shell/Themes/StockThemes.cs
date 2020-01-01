namespace Spect.Net.Shell.Themes
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
            ActivityBarBackgroundColor = "#303030",
            SidebarBackgroundColor = "#252526",

            IconDefaultSize = 12,
            IconDefaultFill = "#ffffff",

            TitleBarActiveBackgroundColor = "#3c3c3c",
            TitleBarInactiveBackgroundColor = "#303030",
            TitleBarActiveColor = "#ffffff",
            TitleBarInactiveColor = "#cccccc",

            MenuActiveBackgroundColor = "#505050",
            MenuSelectedBackgroundColor = "#094771",
            MenuTextColor = "#ffffff",
            MenuPaneBackgroundColor = "#252526",
            MenuPaneShadow = "rgb(0, 0, 0) 0px 2px 4px",
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

            TitleBarActiveBackgroundColor = "#3c3c3c",
            TitleBarInactiveBackgroundColor = "#3ee3e3e",
            TitleBarActiveColor = "#ffffff",
            TitleBarInactiveColor = "#dddddd",

            MenuActiveBackgroundColor = "#505050",
            MenuSelectedBackgroundColor = "#094771",
            MenuTextColor = "#ffffff",
            MenuPaneBackgroundColor = "#252526",
            MenuPaneShadow = "rgb(0, 0, 0) 0px 2px 4px",
        };
    }
}
