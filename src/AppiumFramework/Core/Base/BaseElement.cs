using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AppiumFramework.Core.Base
{
    public abstract class BaseElement
    {
        private static string _contentDescPropertyName = "content-desc";
        private readonly string _elementName;
        private By _elementLocator;

        protected BaseElement(By elementLocator, string elementName)
        {
            _elementLocator = elementLocator;
            _elementName = elementName;
        }

        protected AppiumElement Element
        {
            get
            {
                return AppManager.FindElement(_elementLocator);
            }
        }
        
        public By Locator
        {
            get
            {
                return _elementLocator;
            }
        }

        public virtual bool IsExist
        {
            get
            {
                var isExist = AppManager.FindElements(_elementLocator).Count > 0;
                LogManager.LogDebug($"Элемент {_elementName}{(isExist ? string.Empty : " не")} существует.");
                return isExist;
            }
        }

        public virtual bool IsDisplayed
        {
            get
            {
                var isDisplayed = Element.Displayed;
                LogManager.LogDebug($"Элемент {_elementName}{(isDisplayed ? string.Empty : " не")} виден.");
                return isDisplayed;
            }
        }

        public virtual void Click()
        {
            try
            {
                LogManager.LogDebug($"Нажатие на элемент {_elementName}.");
                Element.Click();
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }

        public virtual string GetText()
        {
            try
            {
                LogManager.LogDebug($"Чтение из элемента {_elementName}.");
                var text = Element.Text;
                LogManager.LogDebug($"Элемент {_elementName} содержит текст {text}.");
                return text;
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }

        private string GetProperty(string name) 
        {
            try
            {
                LogManager.LogDebug($"Чтение свойства {name} из элемента {_elementName}.");
                return Element.GetProperty(name);
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }

        public string GetContentDesc() => GetProperty(_contentDescPropertyName);
    }
}
