using System.Diagnostics;

namespace GameBalance.Diagnostics
{
    public sealed class DebugTimer : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        private readonly string _operation;

        private readonly string _caller;

        private bool _disposed;


        public DebugTimer(
            string operation,
            string caller)
        {
            _operation = operation;
            _caller = caller;

            _stopwatch = Stopwatch.StartNew();


#if DEBUG
            Debug.WriteLine("");
            Debug.WriteLine(
                "▶ STARTING OPERATION");

            Debug.WriteLine(
                $"Operation: {_operation}");

            Debug.WriteLine(
                $"Source: {_caller}");

            Debug.WriteLine(
                "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
#endif
        }


        public void Dispose()
        {
            if (_disposed)
                return;


            _disposed = true;


            _stopwatch.Stop();


            GameBalanceDebug.Performance(
                _operation,
                _stopwatch.Elapsed,
                _caller);
        }
    }
}