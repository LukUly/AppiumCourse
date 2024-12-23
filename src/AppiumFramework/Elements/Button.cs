using AppiumFramework.Core.Base;
using OpenQA.Selenium;

namespace AppiumFramework.Elements
{
    public class Button : BaseElement
    {
        public Button(By locator, string name) : base(locator, name)
        { }
    }
}
