
using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    public class MemoryStep : ISystemInfoStep
    {
        public string Name => "Reading Memory";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.Memory = MemoryInfoProvider.FetchMemoryInformation();

            return Task.CompletedTask;
        }
    }
}
