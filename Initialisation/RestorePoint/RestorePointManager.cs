using GameBalance.Core.Results;
using GameBalance.Framework.Core;
using System.Windows;

namespace GameBalance.Initialisation.RestorePoint
{
    public sealed class RestorePointManager
    {
        public static async Task<bool> HasActiveRestorePointAsync(CancellationToken token)
        {
            return await Task.Run(() => RestorePointService.HasActiveRestorePoint(token), token);
        }

        public static async Task<ModuleResult> CreateRestorePointAsync(IProgress<ProgressResult>? progress, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                if (!GameBalanceServices.IsAdministrator())
                    return await GameBalanceServices.ShowRestartAdministratorDialog();

                var protectionResult = await RestorePointSystemProtection.EnsureSystemProtectionEnabled(progress, token);

                if (!protectionResult.Success)
                    return ModuleResult.Cancel($"System protection was declined");

                progress?.Report(new ProgressResult("Creating system restore point", 75)); 

                var result = await Task.Run(() => RestorePointService.CreateRestorePoint(token), token);

                return result;
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed($"Error creating restore point", ex);
            }
        }
        public static async Task<ModuleResult> ShowRestorePointDialog(IProgress<ProgressResult>? progress = default, CancellationToken token = default)
        {
            var wantsRestorePoint = MessageBox.Show(
                "Your system does not have an active restore point\n" +
                "Do you want to create one now?",
                "Restore Point",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (wantsRestorePoint != MessageBoxResult.Yes)
                return ModuleResult.Cancel("Restore point creation was declined by the user");

            return await CreateRestorePointAsync(progress, token);
        }
    }
}
