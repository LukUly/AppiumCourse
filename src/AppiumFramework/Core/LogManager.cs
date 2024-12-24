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
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string _userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static void Step(string stepDescription, Action action)
        {
            Log(LogLevel.Info, $"Начало шага: {stepDescription}");
            try
            {
                action.Invoke();
                Log(LogLevel.Info, $"Выполнен шаг: {stepDescription}");
                TakeScreenshot();
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"Ошибка в шаге '{stepDescription}': {ex.Message}");
                TakeScreenshot();
                throw;
            }
        }

        public static void Log(LogLevel level = LogLevel.Info, string message = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(message);
                    break;
                case LogLevel.Info:
                    _logger.Info(message);
                    break;
                case LogLevel.Warning:
                    _logger.Warn(message);
                    break;
                case LogLevel.Error:
                    _logger.Error(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public static void LogInfo(string message) => Log(LogLevel.Info, message);

        public static void LogDebug(string message) => Log(LogLevel.Debug, message);

        public static void LogError(string message) => Log(LogLevel.Error, message);

        public static void TakeScreenshot() 
        {
            var logFilePath = Path.Combine(Path.Combine(_userPath, ConfigManager.Config.LogPath), ConfigManager.Config.ScreenshotFolder);
            CreateDirectoryIfNotExists(logFilePath);
            ScreenShotHelper.CaptureScreenshot(logFilePath);
        }

        public static void ConfigureLogging(bool logToFile = true, string logFilePath = null, bool logToConsole = false)
        {
            logFilePath ??= Path.Combine(Path.Combine(_userPath, ConfigManager.Config.LogPath), ConfigManager.Config.LogName);
            var config = new NLog.Config.LoggingConfiguration();

            if (logToFile)
            {
                var fileTarget = new NLog.Targets.FileTarget("fileTarget")
                {
                    FileName = logFilePath,
                    Layout = ConfigManager.Config.LogFormat
                };

                config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, fileTarget);
            }

            if (logToConsole)
            {
                var consoleTarget = new NLog.Targets.ConsoleTarget("consoleTarget")
                {
                    Layout = ConfigManager.Config.LogFormat
                };

                config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget);
            }

            NLog.LogManager.Configuration = config;
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
