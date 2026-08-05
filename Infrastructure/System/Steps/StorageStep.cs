using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    public class StorageStep : ISystemInfoStep
    {
        public string Name => "Reading Storage";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.Storage = StorageInfoProvider.FetchStorageInformation();

            return Task.CompletedTask;
        }
    }
}
