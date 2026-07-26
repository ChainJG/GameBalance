using GameBalance.Initialisation.RestorePoint;
using GameBalance.Core.Interfaces;
using GameBalance.Core.Results;

namespace GameBalance.Initialisation.Steps
{
    public sealed class CheckForRestorePointStartStep : IStartupStep
    {
        public string Name => "Check for restore point";

        public async Task<ModuleResult> ExecuteAsync(IProgress<ProgressResult> progress, CancellationToken token)
        {
            var hasRestorePoint = await RestorePointManager.HasActiveRestorePointAsync(token);

            if (hasRestorePoint)
                return ModuleResult.Successful($"{Name} successful");

            return await RestorePointManager.ShowRestorePointDialog(progress, token);
        }
    }
}
