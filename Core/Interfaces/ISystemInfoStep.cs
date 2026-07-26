using GameBalance.Infrastructure.System.Core;

namespace GameBalance.Core.Interfaces
{
    public interface ISystemInfoStep
    {
        string Name { get; }
        Task ExecuteAsync(SystemInfo info, CancellationToken token);
    }
}
