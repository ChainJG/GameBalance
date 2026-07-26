using GameBalance.Core.Interfaces;
using GameBalance.Core.Results;
using GameBalance.Framework.Core;
using GameBalance.Infrastructure.System.Core;

namespace GameBalance.Initialisation.Steps
{
    public class LoadSystemInfoStartupStep : IStartupStep
    {
        public string Name => "Loading system information";

        public async Task<ModuleResult> ExecuteAsync(IProgress<ProgressResult> progress, CancellationToken token)
        {
            var systemLoader = new SystemInfoLoader();

            var systemInfo = await systemLoader.LoadAsync(progress, token);

            GameBalanceContext.SystemInfo = systemInfo;

            return systemInfo != null ? ModuleResult.Successful() : ModuleResult.Failed();
        }
    }
}
