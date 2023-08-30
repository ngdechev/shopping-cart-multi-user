
namespace ShoppingCartMultiUser.utils
{
    internal class Logger
    {
        private static LogLevel _logLevel = 0;

        public static void SetLogLevel(LogLevel lvl)
        {
            _logLevel = lvl;
        }

        public static void Error(string message)
        {
            if (_logLevel >= LogLevel.Error)
            {
                Message("ERROR", message);
            }
        }

        public static void Warn(string message)
        {
            if (_logLevel >= LogLevel.Warning)
            {
                Message("WARN", message);
            }
        }

        public static void Info(string message)
        {
            if (_logLevel >= LogLevel.Info)
            {
                Message("INFO", message);
            }
        }

        public static void Debug(string message)
        {
            if (_logLevel >= LogLevel.Debug)
            {
                Message("DEBUG", message);
            }
        }

        private static void Message(string severity, string msg)
        {
            var timestamp = DateTime.Now;

            Console.WriteLine($"[{timestamp}] [{severity}] {msg}");
        }
    }
}
