using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spect.Net.Shell.Themes
{
    /// <summary>
    /// This class provides an implementation for theming service
    /// </summary>
    /// <typeparam name="TPropSet">A type holding the property set of the theme</typeparam>
    public class ThemingService<TPropSet> : IThemingService<TPropSet>
    {
        private readonly Dictionary<string, ThemeInfo<TPropSet>> _themes =
            new Dictionary<string, ThemeInfo<TPropSet>>();
        private string _activeId;
        private ThemeInfo<TPropSet> _activeTheme;

        public void Reset()
        {
            _themes.Clear();
            _activeId = null;
            _activeTheme = default;
        }

        /// <summary>
        /// Registers the theme
        /// </summary>
        /// <param name="theme">Theme to register</param>
        public void RegisterTheme(ThemeInfo<TPropSet> theme)
        {
            _themes[theme.Id] = theme;

        }

        /// <summary>
        /// Get all registered themes
        /// </summary>
        /// <returns>List of registered themes</returns>
        public List<ThemeInfo<TPropSet>> GetRegisteredThemes()
            => _themes.Values.ToList();

        /// <summary>
        /// Sets the theme to the specified one
        /// </summary>
        /// <param name="id">Theme name</param>
        public void SetTheme(string id)
        {
            if (id == _activeId) return;
            if (!_themes.TryGetValue(id, out var theme)) return;

            _activeId = id;
            _activeTheme = theme;
            OnThemeChanged();
        }

        /// <summary>
        /// This event is raised when the active theme changes.
        /// </summary>
        public event EventHandler ThemeChanged;

        /// <summary>
        /// Gets the icon with the specified name
        /// </summary>
        /// <param name="iconName">Name of the icon</param>
        /// <returns>Icon information</returns>
        public IconInfo GetIcon(string iconName) => IconDefs.GetIcon(iconName);

        /// <summary>
        /// Gets the active theme
        /// </summary>
        /// <returns>Active theme information</returns>
        public ThemeInfo<TPropSet> GetActiveTheme() => _activeTheme;

        /// <summary>
        /// Gets the specified property
        /// </summary>
        /// <typeparam name="TProp">Property type</typeparam>
        /// <param name="propName">Property name</param>
        /// <returns>Property value</returns>
        public TProp GetProperty<TProp>(string propName)
        {
            var props = _activeTheme.Properties;
            var propInfo = props.GetType().GetProperty(propName);
            return propInfo == null
                ? default
                : (TProp)propInfo.GetValue(props);
        }

        /// <summary>
        /// Gets the specified property
        /// </summary>
        /// <typeparam name="TProp">Property type</typeparam>
        /// <param name="selector">Property selector</param>
        /// <returns>Property value</returns>
        public TProp GetProperty<TProp>(Func<TPropSet, TProp> selector)
        {
            return selector(_activeTheme.Properties);
        }

        /// <summary>
        /// Gets the style attribute to be used with the current theme
        /// </summary>
        /// <returns>Value of the style attribute</returns>
        public string ComposeStyleAttributeFromTheme()
        {
            var sb = new StringBuilder(1024);
            var props = _activeTheme.Properties;
            foreach (var propInfo in props.GetType().GetProperties())
            {
                if (propInfo.GetCustomAttributes(typeof(NonCssAttribute), false).Length == 0)
                {
                    sb.Append($"--{ToCssName(propInfo.Name)}:{propInfo.GetValue(props)};");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Invokes the ThemeChanged event
        /// </summary>
        protected virtual void OnThemeChanged()
        {
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }

        public static string ToCssName(string name)
        {
            var sb = new StringBuilder(100);
            var lastCharWasLowerCase = false;
            foreach (var ch in name)
            {
                if (char.IsLower(ch))
                {
                    sb.Append(ch);
                    lastCharWasLowerCase = true;
                }
                else
                {
                    if (lastCharWasLowerCase)
                    {
                        sb.Append("-");
                    }
                    sb.Append(char.ToLower(ch));
                    lastCharWasLowerCase = false;
                }
            }
            return sb.ToString();
        }
    }
}
