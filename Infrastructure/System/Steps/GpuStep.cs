using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    public class GpuStep : ISystemInfoStep
    {
        public string Name => "Reading GPU";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.GPU = GpuInfoProvider.FetchGpuInformation();

            return Task.CompletedTask;
        }
    }
}
