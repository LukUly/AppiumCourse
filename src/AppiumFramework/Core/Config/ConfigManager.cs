using AppiumFramework.Utils;

namespace AppiumFramework.Core.Config
{
    public static class ConfigManager
    {
        private static readonly string _parentDirectory = AppContext.BaseDirectory;
        private const string _configPath = @"Core\Config\config.json";
        private static ConfigModel? _config = null;

        public static ConfigModel Config
        {
            get
            {
                _config ??= JsonReader.ReadFile<ConfigModel>($"{_parentDirectory}{_configPath}");
                return _config;
            }
        }
    }
}
