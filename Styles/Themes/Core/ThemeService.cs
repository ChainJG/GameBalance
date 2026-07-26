using GameBalance.Diagnostics;
using GameBalance.Framework.Core;
using GameBalance.Framework.Settings;

namespace GameBalance.Styles.Themes.Core
{
    public sealed class ThemeService
    {
        public Theme CurrentTheme => ThemeManager.CurrentTheme ?? BuiltInThemes.Dark;
        public IReadOnlyList<Theme> AvailableThemes => BuiltInThemes.All;

        public void Initialise()
        {
            Theme theme = LoadTheme();

            ThemeManager.ApplyTheme(theme);
        }

        public void SetTheme(Theme theme)
        {
            ThemeManager.ApplyTheme(theme);

            SaveTheme(theme);
        }

        private Theme LoadTheme()
        {
            if (String.IsNullOrWhiteSpace(GameBalanceContext.Settings.ThemeType))
            {
                GameBalanceDebug.Warning("Theme type is not set in settings. Defaulting to Dark theme");
                return BuiltInThemes.Dark;
            }

            Theme? theme =
                BuiltInThemes.All
                .FirstOrDefault(x =>
                    x.Name == GameBalanceContext.Settings.ThemeType);

            GameBalanceDebug.Info($"Theme type '{GameBalanceContext.Settings.ThemeType}' loaded from settings");

            return theme
                ?? BuiltInThemes.Dark;
        }

        private void SaveTheme(Theme theme)
        {
            string themeType = theme.Name;

            GameBalanceContext.Settings.ThemeType = themeType;
            GameBalanceSettingsService.Save(GameBalanceContext.Settings);

            GameBalanceDebug.Info($"Theme type '{themeType}' saved to settings");
        }
    }
}
