using GameBalance.Diagnostics;

namespace GameBalance.Core.Results
{
    public sealed class ModuleResult
    {

        public bool Success { get; init; } = true;
        public string Message { get; init; } = string.Empty;
        public Exception? Exception { get; init; }
        public int ExitCode { get; init; } = 0;
        public ResultType Status { get; init; } = ResultType.Unknown;


        public static ModuleResult Successful(string message = "Successfully completed") =>
            new() { Success = true, Status = ResultType.Successful, Message = message, ExitCode = 0 };

        public static ModuleResult Failed(string message = "Failed to complete", Exception? exception = null, ResultType status = ResultType.Failed, int exitCode = 1)
        {
            if (exception != null)
                GameBalanceDebug.Error(message, exception);

            return new() { Success = false, Status = status, Message = message, Exception = exception, ExitCode = exitCode };
        }

        public static ModuleResult Cancel(string message = "Operation cancelled") =>
            new() { Success = false, Status = ResultType.Cancelled, Message = message, ExitCode = 404 };
    }
}
