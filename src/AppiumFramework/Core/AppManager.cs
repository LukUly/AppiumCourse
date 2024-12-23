using AppiumFramework.Core.Base;
using AppiumFramework.Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AppiumFramework.Core
{
    public static class AppManager
    {
        public static void LoadApp(string package = null) 
        {
            package ??= ConfigManager.Config.Package;
            LogManager.LogInfo($"Запускаем приложение {package}");
            AppiumDriver.Instance.ActivateApp(package);
        }

        public static void CloseApp(string package = null) 
        {
            package ??= ConfigManager.Config.Package;
            LogManager.LogInfo($"Закрытие приложения {package}");
            AppiumDriver.Instance.TerminateApp(package);
        }

        public static AppiumElement FindElement(By locator) 
        {
            try
            {
                WaitManager.WaitForElementToBeVisible(locator);
                return AppiumDriver.Instance.FindElement(locator);
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }

        public static List<AppiumElement> FindElements(By locator) 
        {
            try
            {
                //WaitManager.WaitForElementToBeVisible(locator);
                return AppiumDriver.Instance.FindElements(locator).ToList();
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }
    }
}
