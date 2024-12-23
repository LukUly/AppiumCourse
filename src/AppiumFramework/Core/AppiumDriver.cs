using AppiumFramework.Core.Config;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumFramework.Core
{
    public class AppiumDriver
    {
        private static AndroidDriver? _instance = null;

        public static AndroidDriver Instance
        {
            get
            {
                try
                {
                    if (_instance is null)
                    {
                        var options = new AppiumOptions
                        {
                            AutomationName = AutomationName.AndroidUIAutomator2
                        };

                        options.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, ConfigManager.Config.PlatformName);

                        var uri = new Uri(ConfigManager.Config.Uri);

                        _instance = new AndroidDriver(uri, options);
                    }
                }
                catch
                {
                    throw;
                }

                return _instance;
            }
        }
    }
}
