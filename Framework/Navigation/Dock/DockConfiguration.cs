using GameBalance.Framework.Navigation.Core;

namespace GameBalance.Framework.Navigation.Dock
{
    public class DockConfiguration
    {
        public IList<ActionItem> Items { get; init; } = [];
        public bool ShowGlobalButton { get; init; } = false;
    }
}
