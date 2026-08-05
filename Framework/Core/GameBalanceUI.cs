namespace GameBalance.Framework.Core
{
    public static class GameBalanceUI
    {
        public static event Action? StartupCompleted;

        public static void OnStartupCompleted() => StartupCompleted?.Invoke();
    }
}
