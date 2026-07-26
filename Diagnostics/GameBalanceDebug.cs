using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace GameBalance.Diagnostics
{
    public static class GameBalanceDebug
    {
        private static string? _lastCaller;


        [Conditional("DEBUG")]
        public static void Info(
            string message,
            [CallerMemberName] string caller = "",
            [CallerFilePath] string filePath = "")
        {
            Write(
                "INFO",
                "ℹ️",
                message,
                null,
                caller,
                filePath);
        }


        [Conditional("DEBUG")]
        public static void Success(
            string message,
            [CallerMemberName] string caller = "",
            [CallerFilePath] string filePath = "")
        {
            Write(
                "SUCCESS",
                "✅",
                message,
                null,
                caller,
                filePath);
        }


        [Conditional("DEBUG")]
        public static void Warning(
            string message,
            Exception? exception = null,
            [CallerMemberName] string caller = "",
            [CallerFilePath] string filePath = "")
        {
            Write(
                "WARNING",
                "⚠️",
                message,
                exception,
                caller,
                filePath);
        }


        [Conditional("DEBUG")]
        public static void Error(
            string message,
            Exception? exception = null,
            [CallerMemberName] string caller = "",
            [CallerFilePath] string filePath = "")
        {
            Write(
                "ERROR",
                "❌",
                message,
                exception,
                caller,
                filePath);
        }


        public static DebugTimer Measure(
            string operation,
            [CallerMemberName] string caller = "")
        {
            return new DebugTimer(operation, caller);
        }


        [Conditional("DEBUG")]
        private static void Write(
            string level,
            string icon,
            string message,
            Exception? exception,
            string caller,
            string filePath)
        {
#if DEBUG

            string fileName = Path.GetFileName(filePath);
            string time = DateTime.Now.ToString("HH:mm:ss.fff");

            if (_lastCaller != caller)
            {
                Debug.WriteLine("");
                Debug.WriteLine(
                    "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            }


            Debug.WriteLine(
                $"{icon} [{level}] [{time}]");

            Debug.WriteLine(
                $"Source: {fileName}::{caller}");

            Debug.WriteLine(
                $"Message: {message}");


            if (exception != null)
            {
                Debug.WriteLine(
                    $"Exception: {exception.GetType().Name}");

                Debug.WriteLine(
                    $"Details: {exception.Message}");
            }


            Debug.WriteLine(
                "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");


            _lastCaller = caller;

#endif
        }


        internal static void Performance(
            string operation,
            TimeSpan elapsed,
            string caller)
        {
#if DEBUG

            Debug.WriteLine("");

            Debug.WriteLine(
                "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            Debug.WriteLine("▶ END OPERATION");

            Debug.WriteLine(
                $"Operation: {operation}");

            Debug.WriteLine(
                $"Duration: {elapsed.TotalMilliseconds:N0}ms");

            Debug.WriteLine(
                $"Source: {caller}");

            Debug.WriteLine(
                "Status: Completed Successfully");


            Debug.WriteLine(
                "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

#endif
        }
    }
}