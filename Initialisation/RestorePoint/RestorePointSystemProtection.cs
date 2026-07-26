using GameBalance.Core.Results;
using GameBalance.Infrastructure.Registry;
using GameBalance.Infrastructure.Shell;
using Microsoft.Win32;
using System.Windows;

namespace GameBalance.Initialisation.RestorePoint
{
    public static class RestorePointSystemProtection
    {
        public static async Task<ModuleResult> EnableSystemProtectionAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var result = await AdminShellService.RunAsync(ShellType.PowerShell, "Enable-ComputerRestore -Drive 'C:\\'", token);

            if (!result.Success)
            {
                return ModuleResult.Failed("Failed to enable system protection", result.Exception, result.Status, result.ExitCode);
            }

            return ModuleResult.Successful("System protection enabled successfully");
        }
        public static bool IsSystemProtectionEnabled()
        {
            RegistryEditor.TryGetValue<int>(
                new RegistryEditInfo
                {
                    Hive = RegistryHive.LocalMachine,
                    Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore",
                    Key = "RPSessionInterval"
                }, out int value);

            return value > 0;
        }

        public static async Task<ModuleResult> EnsureSystemProtectionEnabled(IProgress<ProgressResult>? progress, CancellationToken token)
        {
            if (IsSystemProtectionEnabled())
                return ModuleResult.Successful("System protection is enabled");

            progress?.Report(new ProgressResult("System protection is disabled", 30));

            var wantsToEnableProtection = MessageBox.Show(
                $"System protection is required to create a restore point\n" +
                $"Do you want to enable it now?",
                "Restore Point",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes;

            if (wantsToEnableProtection)
            {
                progress?.Report(new ProgressResult("Enabling system protection", 50));

                return await EnableSystemProtectionAsync(token);
            }

            return ModuleResult.Failed("System protection was not enabled");
        }
    }
}
