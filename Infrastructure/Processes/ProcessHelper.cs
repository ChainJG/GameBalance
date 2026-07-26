using System.Diagnostics;
using System.IO;

namespace GameBalance.Infrastructure.Processes
{
    public static class ProcessHelper
    {
        public static long GetTotalMemoryUsageForProcess(string processName)
        {
            try
            {
                string normalizedName = NormaliseProcessName(processName);

                if (string.IsNullOrWhiteSpace(normalizedName))
                    return 0;

                Process[] processes = Process.GetProcessesByName(normalizedName);

                long totalMemoryUsage = 0;

                foreach (Process process in processes)
                {
                    try
                    {
                        totalMemoryUsage += process.WorkingSet64;
                    }
                    catch
                    {
                    }
                }
                return totalMemoryUsage;

            }
            catch
            {
                return 0;
            }
        }


        #region Helper Methods
        private static string NormaliseProcessName(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
                return string.Empty;

            string trimmedName = processName.Trim();

            if (trimmedName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                trimmedName = Path.GetFileNameWithoutExtension(trimmedName);

            return trimmedName;
        } 
        #endregion
    }
}
