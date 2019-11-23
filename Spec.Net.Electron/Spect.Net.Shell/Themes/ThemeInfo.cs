namespace Spect.Net.Shell.Themes
{
    /// <summary>
    /// This structure represents a theme with its properties
    /// </summary>
    /// <typeparam name="TPropSet">A type holding the property set of the theme</typeparam>
    public struct ThemeInfo<TPropSet>
    {
        /// <summary>
        /// Theme identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Name to display for this theme
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// The properties of the theme
        /// </summary>
        public TPropSet Properties { get; }

        public ThemeInfo(string id, string displayName, TPropSet properties)
        {
            Id = id;
            DisplayName = displayName;
            Properties = properties;
        }
    }
}
