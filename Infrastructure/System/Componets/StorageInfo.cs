using System.IO;

namespace GameBalance.Infrastructure.System.Componets
{
    public sealed class StorageInfo
    {
        public IReadOnlyList<DriveInfo> FixedReadyDrives;
        public string TotalStorage { get; set; } = "Unknown";
        public string TotalUsage { get; set; } = "Unknown";
    }
}
