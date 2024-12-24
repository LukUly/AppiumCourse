using AppiumFramework.Core;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.Core.Base
{
    public abstract class BasePage
    {
        protected By _locator;
        protected string _name;

        protected BasePage(By locator, string name)
        {
            _locator = locator;
            _name = name;
        }

        public bool IsDisplayed()
        {
            LogManager.LogDebug($"Проверка отображения страницы '{_name}'.");
            return new Button(_locator, $"Уникальный элемент страницы '{_name}'").IsDisplayed;
        }

        public override string ToString() => $"Page '{_name}'";
    }
}
