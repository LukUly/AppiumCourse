using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumProject
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
                    if ( _instance is null)
                    {
                        var options = new AppiumOptions
                        {
                            AutomationName = AutomationName.AndroidUIAutomator2
                        };

                        options.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Andriod");

                        var uri = new Uri("http://localhost:4723/");

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
