using GameBalance.Core.Results;
using System.ComponentModel;
using System.Diagnostics;

namespace GameBalance.Infrastructure.Shell
{
    public static class AdminShellService
    {
        private const int UacCancelledErrorCode = 1223;

        public static async Task<ModuleResult> RunAsync(ShellType shell, string command, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(command))
                    throw new ArgumentException("Command cannot be empty", nameof(command));

                string fileName = shell switch
                {
                    ShellType.Cmd => "cmd.exe",
                    ShellType.PowerShell => "powershell",
                    _ => throw new NotSupportedException()
                };

                var arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"";

                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                using var process = Process.Start(psi) 
                    ?? throw new InvalidOperationException("Failed to start the process");

                await process.WaitForExitAsync(token);

                if (process.ExitCode == 0)
                {
                    return ModuleResult.Successful("Command executed successfully");
                }
                else
                {
                    throw new InvalidOperationException($"Command failed with exit code {process.ExitCode}");
                }
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == UacCancelledErrorCode)
            {
                return ModuleResult.Failed("Administrator permission was declined", ex, ResultType.Administrator);
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed("An error occurred while running the command", ex);
            }
        }
    }
}
