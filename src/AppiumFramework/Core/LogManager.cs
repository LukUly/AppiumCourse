using AppiumFramework.Core.Config;
using NLog;

namespace AppiumFramework.Core
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public static class LogManager
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Step(string stepDescription, Action action)
        {
            Log(LogLevel.Info, $"");
            Log(LogLevel.Info, $"Начало шага: {stepDescription}");
            try
            {
                action.Invoke();
                Log(LogLevel.Info, $"Выполнен шаг: {stepDescription}");
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"Ошибка в шаге '{stepDescription}': {ex.Message}");
                throw;
            }
        }

        public static void Log(LogLevel level = LogLevel.Info, string message = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                    Logger.Debug(message);
                    break;
                case LogLevel.Info:
                    Logger.Info(message);
                    break;
                case LogLevel.Warning:
                    Logger.Warn(message);
                    break;
                case LogLevel.Error:
                    Logger.Error(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public static void LogInfo(string message) => Log(LogLevel.Info, message);

        public static void LogDebug(string message) => Log(LogLevel.Debug, message);

        public static void LogError(string message) => Log(LogLevel.Error, message);


        public static void ConfigureLogging(bool logToFile = true, string logFilePath = null, bool logToConsole = false)
        {
            logFilePath ??= ConfigManager.Config.LogPath;
            var config = new NLog.Config.LoggingConfiguration();

            if (logToFile)
            {
                var fileTarget = new NLog.Targets.FileTarget("fileTarget")
                {
                    FileName = logFilePath,
                    Layout = "${longdate}|${level:uppercase=true}|${message}"
                };

                config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, fileTarget);
            }

            if (logToConsole)
            {
                var consoleTarget = new NLog.Targets.ConsoleTarget("consoleTarget")
                {
                    Layout = "${longdate}|${level:uppercase=true}|${message}"
                };

                config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget);
            }

            NLog.LogManager.Configuration = config;
        }
    }
}
