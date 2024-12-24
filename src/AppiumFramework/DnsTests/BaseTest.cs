using AppiumFramework.Core;
using NUnit.Framework;

namespace AppiumFramework.DnsTests
{
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            LogManager.ConfigureLogging();
            LogManager.LogInfo("Тест запущен");
            AppManager.LoadApp();
        }

        [TearDown]
        public void TearDown()
        {
            LogManager.LogInfo("Завершение теста");
            AppManager.CloseApp();

            AppiumDriver.Instance.Quit();
            LogManager.LogInfo("Тест завершен");
        }
    }
}
