using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    public class CpuStep : ISystemInfoStep
    {
        public string Name => "Reading CPU";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.CPU = CpuInfoProvider.FetchCpuInformation();

            return Task.CompletedTask;
        }
    }
}
