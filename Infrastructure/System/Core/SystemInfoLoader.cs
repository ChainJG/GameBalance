using GameBalance.Core.Interfaces;
using GameBalance.Core.Results;
using GameBalance.Diagnostics;
using GameBalance.Helper;
using GameBalance.Infrastructure.System.Steps;

namespace GameBalance.Infrastructure.System.Core
{
    public class SystemInfoLoader
    {
        private readonly List<ISystemInfoStep> _steps;

        public SystemInfoLoader()
        {
            _steps =
            [
                new CpuStep(),
                new GpuStep(),
                new MemoryStep(),
                new MotherboardStep(),
                new OSStep(),
            ];
        }

        public async Task<SystemInfo?> LoadAsync(IProgress<ProgressResult> progress, CancellationToken token)
        {
            try
            {
                var systemInfo = new SystemInfo();

                for (var i = 0; i < _steps.Count; i++)
                {
                    token.ThrowIfCancellationRequested();

                    var step = _steps[i];

                    progress.Report(
                        new ProgressResult(
                            step.Name,
                            MathHelper.ToPercentageInt(i + 1, _steps.Count)
                            ));

                    await step.ExecuteAsync(systemInfo, token);

                }

                return systemInfo;
            }
            catch (Exception ex)
            {
                GameBalanceDebug.Error("Faile loading system information", ex);
                return null;
            }
        }
    }
}