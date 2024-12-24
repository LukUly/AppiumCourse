using AppiumFramework.Core.Config;
using OpenQA.Selenium;
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

                        var capabilities = ConfigManager.Config.Capabilities;
                        foreach ( var capability in capabilities) 
                        {
                            options.AddAdditionalAppiumOption(capability.Name, capability.Value);
                        }
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
