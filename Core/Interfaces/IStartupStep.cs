using GameBalance.Core.Results;

namespace GameBalance.Core.Interfaces
{
    public interface IStartupStep
    {
        string Name { get; }
        Task<ModuleResult> ExecuteAsync(IProgress<ProgressResult> progress, CancellationToken cancellationToken);
    }
}
