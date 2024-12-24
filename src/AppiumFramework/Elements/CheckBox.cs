using AppiumFramework.Core;
using AppiumFramework.Core.Base;
using OpenQA.Selenium;

namespace AppiumFramework.Elements
{
    public class CheckBox : BaseElement
    {
        public CheckBox(By locator, string name) : base(locator, name)
        { }

        public bool IsChecked 
        {
            get 
            {
                return Element.Selected;
            }
        }

        public void Check()
        {
            if (!IsChecked)
            {
                LogManager.LogDebug("Выбор.");
                try
                {
                    Element.Click();
                }
                catch (Exception ex)
                {
                    LogManager.LogError(ex.Message);
                    throw;
                }
            }
            else
            {
                LogManager.LogDebug("Элемент уже выбран.");
            }
        }
    }
}
