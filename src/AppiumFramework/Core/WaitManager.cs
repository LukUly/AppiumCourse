using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AppiumFramework.Core;
using AppiumFramework.Core.Config;
using AppiumFramework.Core.Base;

namespace AppiumFramework
{
    public static class WaitManager
    {
        private static WebDriverWait _wait;

        public static void WaitForElementToBeVisible(By locator)
        {
            _wait ??= new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitForElementToDisappear(BaseElement element)
        {
            _wait ??= new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element.Locator));
        }

        public static void WaitForAlertToBePresent()
        {
            _wait ??= new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }
    }
}
