using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    public class OSStep : ISystemInfoStep
    {
        public string Name => "Reading OS";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.OS = OSInfoProvider.FetchOSInformation();

            return Task.CompletedTask;
        }
    }
}
