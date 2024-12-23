using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AppiumFramework.Core;
using AppiumFramework.Core.Config;
using AppiumFramework.Core.Base;

namespace AppiumFramework
{
    public static class WaitManager
    {
        public static void WaitForElementToBeVisible(BaseElement element)
        {
            new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element.Locator));
        }

        public static void WaitForElementToBeVisible(By locator)
        {
            new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitForElementToDisappear(BaseElement element)
        {
            new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element.Locator));
        }

        public static void WaitForAlertToBePresent()
        {
            new WebDriverWait(AppiumDriver.Instance, TimeSpan.FromSeconds(ConfigManager.Config.WaitingTimeSeconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }
    }
}
