using GameBalance.Diagnostics;
using GameBalance.Helper;
using GameBalance.Infrastructure.System.Componets;
using System.IO;

namespace GameBalance.Infrastructure.System.Providers
{
    public static class StorageInfoProvider
    {
        public static StorageInfo FetchStorageInformation()
        {
            var storage = new StorageInfo();

            try
            {
                var drives = GetFixedReadyDrives();

                storage.FixedReadyDrives = drives;

                GetTotalStorage(storage, drives);
                GetTotalStorageUsaged(storage, drives);
            }
            catch (Exception ex)
            {
                GameBalanceDebug.Error("Failed to fetch storage information", ex);
            }

            return storage;
        }

        private static long GetTotalStorage(StorageInfo storage, IReadOnlyList<DriveInfo> drives)
        {
            var totalBytes = drives.Sum(drive => drive.TotalSize);
            storage.TotalStorage = MathHelper.FormatBytes(totalBytes);

            return Math.Abs(totalBytes);
        }

        private static long GetTotalStorageUsaged(StorageInfo storage, IReadOnlyList<DriveInfo> drives)
        {
            var totalBytesUsed = drives.Sum(drive => drive.TotalSize - drive.AvailableFreeSpace);
            storage.TotalUsage = MathHelper.FormatBytes(totalBytesUsed);

            return Math.Abs(totalBytesUsed);
        }
        private static IReadOnlyList<DriveInfo> GetFixedReadyDrives() =>
             [.. DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Fixed)];
    }
}
