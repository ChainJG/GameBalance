using GameBalance.Core.Interfaces;
using GameBalance.Core.Results;
using GameBalance.Initialisation.Update;

namespace GameBalance.Initialisation.Steps
{
    public sealed class CheckForUpdateStartStep : IStartupStep
    {
        public string Name => "Check for update";

        public async Task<ModuleResult> ExecuteAsync(IProgress<ProgressResult> progress, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                var update = await GitHubReleaseService.CheckForUpdatesAsync(progress, token);

                return ModuleResult.Successful($"Successfully checked for GameBalance update");
            }
            catch (Exception ex)
            {
                return ModuleResult.Failed($"Failed checking for update", ex);
            }
        }
    }
}
