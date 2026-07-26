
using GameBalance.Initialisation.Steps;
using GameBalance.Core.Interfaces;
using GameBalance.Core.Results;

namespace GameBalance.Initialisation.Core
{
    public sealed class StartupCoordinator
    {
        private readonly List<IStartupStep> _steps;

        public StartupCoordinator()
        {
            _steps =
            [
                new CheckForUpdateStartStep(),
                new CheckForRestorePointStartStep(),
                new LoadSystemInfoStartupStep(),
            ];
        }

        public async Task<ModuleResult> InitialiseAsync(IProgress<ProgressResult> progress, CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                for (var index = 0; index < _steps.Count; index++)
                {
                    token.ThrowIfCancellationRequested();

                    var step = _steps[index];

                    var (StartPercent, EndPercent) = StartupProgressMapper.GetRange(index, _steps.Count);

                    progress.Report(new ProgressResult(step.Name, StartPercent));

                    var stepProgress = StartupProgressMapper.CreateRange(progress, StartPercent, EndPercent, step.Name);

                    var result = await step.ExecuteAsync(stepProgress, token);
                }

                await Task.Yield();

                return InitialiseComplete(progress);
            }
            catch (OperationCanceledException ex)
            {
                progress.Report(new ProgressResult("Initialisation cancelled", 100));
                return ModuleResult.Failed("Initialisation cancelled", ex);
            }
            catch (Exception ex)
            {
                progress.Report(new ProgressResult(ex.Message, 100));
                return ModuleResult.Failed("Failed to initialise GameBalance", ex);
            }
        }

        private static ModuleResult InitialiseComplete(IProgress<ProgressResult> progress)
        {
            progress.Report(new ProgressResult("Initialisation complete", 100));
            return ModuleResult.Successful("Initialisation complete");
        }
    }
}
