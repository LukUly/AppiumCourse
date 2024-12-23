using AppiumFramework.Core;
using AppiumFramework.Core.Base;
using OpenQA.Selenium;

namespace AppiumFramework.Elements
{
    public class Input : BaseElement
    {
        public Input(By locator, string name) : base(locator, name)
        { }

        public void SendKeys(string text) 
        {
            try
            {
                LogManager.LogDebug($"Ввод текста {text}.");
                Element.SendKeys(text);
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }
    }
}
