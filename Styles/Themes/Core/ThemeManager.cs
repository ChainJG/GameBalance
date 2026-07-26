using System.Windows;

namespace GameBalance.Styles.Themes.Core
{
    public sealed class ThemeManager
    {
        private static ResourceDictionary? _currentTheme;
        public static Theme? CurrentTheme { get; private set;  }
        public static event EventHandler<Theme>? ThemeChanged;

        public static void ApplyTheme(Theme theme)
        {
            if (Application.Current == null)
                throw new InvalidOperationException($"WPF Application not initialised");

            ResourceDictionary newTheme = new()
            {
                Source = new Uri(theme.ResourcePath, UriKind.Relative)
            };

            RemoveCurrentTheme();

            Application.Current.Resources.MergedDictionaries.Insert(0, newTheme);

            _currentTheme = newTheme;
            CurrentTheme = theme;

            ThemeChanged?.Invoke(null, theme);
        }

        private static void RemoveCurrentTheme()
        {
            if (_currentTheme == null)
                return;


            Application.Current.Resources
                .MergedDictionaries
                .Remove(_currentTheme);


            _currentTheme = null;
        }
    }
}
