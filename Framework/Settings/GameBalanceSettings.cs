namespace GameBalance.Framework.Settings
{
    public class GameBalanceSettings
    {
        public string ThemeType { get; set; } = "Dark";
        public OperationState RestorePoint { get; set; } = new();
    }

    public class OperationState
    {
        public DateTime? LastCreated { get; set; }

        public ResultType LastStatus { get; set; } = ResultType.Unknown;
    }
}
