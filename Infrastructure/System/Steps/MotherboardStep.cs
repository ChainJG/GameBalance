using GameBalance.Core.Interfaces;
using GameBalance.Infrastructure.System.Core;
using GameBalance.Infrastructure.System.Providers;

namespace GameBalance.Infrastructure.System.Steps
{
    internal class MotherboardStep : ISystemInfoStep
    {
        public string Name => "Read Motherboard";

        public Task ExecuteAsync(SystemInfo info, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            info.Motherboard = MotherboardInfoProvider.FetchMotherboardInformation();

            return Task.CompletedTask;
        }
    }
}
