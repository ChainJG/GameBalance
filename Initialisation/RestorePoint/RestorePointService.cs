using GameBalance.Core.Results;
using GameBalance.Diagnostics;
using GameBalance.Framework.Core;
using GameBalance.Framework.Settings;
using GameBalance.Helper;
using System.Management;

namespace GameBalance.Initialisation.RestorePoint
{
    public sealed class RestorePointService
    {
        private static readonly string Description = $"{GameBalanceContext.AppName} Restore Point";
        private static readonly TimeSpan MaxSavedRestorePointAge = TimeSpan.FromDays(30);

        public static ModuleResult CreateRestorePoint(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                var scope = new ManagementScope(@"\\.\root\default");
                scope.Connect();

                using var restoreClass = new ManagementClass(
                    scope,
                    new ManagementPath("SystemRestore"),
                    null);

                using var inParams = restoreClass.GetMethodParameters("CreateRestorePoint");

                inParams["Description"] = Description;
                inParams["RestorePointType"] = 0;
                inParams["EventType"] = 100;

                using var outParams = restoreClass.InvokeMethod(
                    "CreateRestorePoint",
                    inParams,
                    null);

                var returnValue = Convert.ToUInt32(outParams?["ReturnValue"] ?? 1);

                var status = returnValue == 0
                    ? ResultType.Successful
                    : ResultType.Failed;

                SaveRestorePointState(status);

                return returnValue switch
                {
                    0 => ModuleResult.Successful("Successfully created restore point"),
                    _ => ModuleResult.Failed($"Restore point error code {returnValue}")
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                return ModuleResult.Failed("Administrator permission is required to create a restore point", ex, ResultType.Administrator);
            }
            catch (ManagementException ex)
            {
                return ModuleResult.Failed($"WMI Error in creating restore point", ex);
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed("Failed to create restore point", ex);
            }
        }

        public static bool HasActiveRestorePoint(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (!GameBalanceServices.IsAdministrator())
                return HasRecentSavedRestorePoint(token);

            var hasRestorePoint = GetActiveRestorePointList(token).Any(description => description == Description);

            SaveRestorePointState(hasRestorePoint ? ResultType.Successful : ResultType.Failed);

            return hasRestorePoint;
        }
        private static bool HasRecentSavedRestorePoint(CancellationToken token) 
        {
            token.ThrowIfCancellationRequested();

            var restorePoint = GameBalanceContext.Settings.RestorePoint;

            GameBalanceDebug.Info(
                $"Loaded restore point state. Status: {restorePoint.LastStatus}, " +
                $"LastCreated: {(restorePoint.LastCreated.HasValue ? restorePoint.LastCreated.Value.ToString("yyyy-MM-dd HH:mm:ss") : "None")}, " +
                $"MaxAge: {StringHelper.FormatTimeSpan(MaxSavedRestorePointAge)}");

            if (restorePoint.LastStatus != ResultType.Successful)
            {
                GameBalanceDebug.Warning($"Restore point check failed. Last status was {restorePoint.LastStatus}");

                return false;
            }

            if (restorePoint.LastCreated is not { } lastCreated)
            {
                GameBalanceDebug.Warning("LastCreated was empty even though status was successful");

                return false;
            }

            var age = DateTime.Now - lastCreated;

            if (age < TimeSpan.Zero)
            {
                GameBalanceDebug.Warning("LastCreated is in the future, which means the saved state/time is invalid");

                return false;
            }

            if (age > MaxSavedRestorePointAge)
            {
                GameBalanceDebug.Warning(
                    $"Restore point is too old" +
                    $"Age: {StringHelper.FormatTimeSpan(age)}, MaxAllowedAge: {StringHelper.FormatTimeSpan(MaxSavedRestorePointAge)}");

                return false;
            }

            GameBalanceDebug.Success($"Recent restore point found. Age: {StringHelper.FormatTimeSpan(age)}");

            return true;
        }
        public static List<string> GetActiveRestorePointList(CancellationToken token)
        {
            var restorePointList = new List<string>();

            try
            {
                token.ThrowIfCancellationRequested();

                var searcher = new ManagementObjectSearcher(
                    @"root\default",
                    "SELECT * FROM SystemRestore");

                restorePointList.AddRange(from ManagementObject obj in searcher.Get()
                                          select obj["Description"]?.ToString());
            }
            catch (Exception ex)
            {
                GameBalanceDebug.Error($"Failed to get active restore points", ex);
            }

            return restorePointList;
        }

        public static void SaveRestorePointState(ResultType result)
        {
            var setting = GameBalanceContext.Settings;

            setting.RestorePoint.LastCreated = DateTime.Now;
            setting.RestorePoint.LastStatus = result;

            GameBalanceSettingsService.Save(setting);
        }
    }
}
