namespace GameBalance.Styles.Themes.Core
{
    public sealed class Theme(string name, string displayName, string resourcePath)
    {
        public string Name { get; } = name;
        public string DisplayName { get; } = displayName;
        public string ResourcePath { get; } = resourcePath;

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
