using GameBalance.Framework.Settings;
using GameBalance.Infrastructure.System.Core;

namespace GameBalance.Framework.Core
{
    public static class GameBalanceContext
    {
        static GameBalanceContext()
        {
            Settings = GameBalanceSettingsService.Load();
        }

        public static string AppName { get; } = "GameBalance";
        public static GameBalanceSettings Settings { get; }
        public static SystemInfo? SystemInfo { get; internal set; }
    }
}
