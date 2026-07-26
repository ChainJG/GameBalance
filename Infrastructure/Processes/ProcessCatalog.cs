namespace GameBalance.Infrastructure.Processes
{
    public class ProcessCatalog
    {
        private static readonly HashSet<string> ProtectedProcessesList = new(StringComparer.OrdinalIgnoreCase)
        {
            "system",
            "idle",
            "registry",
            "smss",
            "csrss",
            "wininit",
            "services",
            "lsass",
            "winlogon",
            "svchost",
            "dwm",
            "explorer",
            "sihost",
            "fontdrvhost",
            "conhost",
            "ctfmon",
            "runtimebroker",
            "searchhost",
            "startmenuexperiencehost",
            "shellexperiencehost",
            "securityhealthservice",
            "securityhealthsystray",
            "msmpeng",
            "smartscreen",
            "audiodg",
            "nvcontainer",
            "amdow",
            "atiesrxx",
            "atieclxx",
        };

        public static bool IsProtectedProcess(string processName) => ProtectedProcessesList.Contains(processName);
    }
}
