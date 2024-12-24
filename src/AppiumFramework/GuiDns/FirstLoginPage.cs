using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class FirstLoginPage : BasePage
    {
        private readonly Button SkipAuthButton = new Button(
            By.XPath("//*[contains(@resource-id, 'skip_auth_button')]"),
            "Кнопка Войти позже");

        public FirstLoginPage() : base(By.XPath("//*[contains(@resource-id, 'skip_auth_button')]"),
            "Войти")
        { }

        public void ClickSkipAuthButton() => SkipAuthButton.Click();
    }
}
