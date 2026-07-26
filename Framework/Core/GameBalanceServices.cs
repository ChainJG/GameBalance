using GameBalance.Core.Results;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace GameBalance.Framework.Core
{
    public static class GameBalanceServices
    {
        private const int UacCancelledErrorCode = 1223;

        public static void Shutdown() => Application.Current.Shutdown();

        public static bool IsAdministrator()
        {
            using var identity = WindowsIdentity.GetCurrent();

            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        #region Diglog Methods
        public static async Task<ModuleResult> ShowRestartAdministratorDialog()
        {
            try
            {
                var result = MessageBox.Show(
                    "Administrator permission is required.\nRestart as administrator?",
                    "Administrator",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result != MessageBoxResult.Yes)
                    return ModuleResult.Failed("Administrator restart was declined");

                var executablePath = Environment.ProcessPath;

                if (string.IsNullOrWhiteSpace(executablePath))
                {
                    return ModuleResult.Failed("Unable to restart as administrator");
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = executablePath,
                    Verb = "runas",
                    UseShellExecute = true,
                    WorkingDirectory = AppContext.BaseDirectory
                };

                var process = Process.Start(startInfo);

                if (process is null)
                {
                    return ModuleResult.Failed("Failed to start GameBoost as administrator");
                }

                Shutdown();

                return ModuleResult.Successful("GameBoost restarted as administrator");
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == UacCancelledErrorCode)
            {
                return ModuleResult.Failed("Administrator permission was declined", ex);
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed($"Failed to restart as administrator", ex);
            }
        }
        #endregion

    }
}
