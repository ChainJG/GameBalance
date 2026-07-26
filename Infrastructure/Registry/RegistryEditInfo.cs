using Microsoft.Win32;

namespace GameBalance.Infrastructure.Registry
{
    public sealed class RegistryEditInfo
    {
        public DebugMode DebugMode { get; init; } = DebugMode.Disabled;
        public required RegistryHive Hive { get; set; }
        public required string Path { get; init; } = string.Empty;
        public string Key { get; init; } = string.Empty;
        public RegistryValueKind? Kind { get; init; }
        public object? EnabledValue { get; init; }
        public object? DisabledValue { get; init; }
        public RegistryValueAction EnabledAction { get; init; } = RegistryValueAction.Set;
        public RegistryValueAction DisabledAction { get; init; } = RegistryValueAction.Set;

    }
    public enum RegistryValueAction
    {
        Set,
        Delete,
        DeleteKeyTree,
        Ignore
    }
}
