namespace GameBalance.Styles.Themes.Core
{
    public static class BuiltInThemes
    {
        public static readonly Theme Dark =
            new(
                "Dark",
                "Dark Theme",
                "Styles/Themes/Resources/Colours/Dark.xaml"
            );


        public static readonly Theme Light =
            new(
                "Light",
                "Light Theme",
                "Styles/Themes/Resources/Colours/Light.xaml"
            );

        public static IReadOnlyList<Theme> All =>
            new[]
            {
            Dark,
            Light,
            };
    }
}
