using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class ProfilePage : BasePage
    {
        private readonly Button LoginButton = new Button(
            By.XPath("//*[contains(@resource-id, 'login_button')]"),
            "Кнопка Войти");

        private readonly Button SelectedButton = new Button(
            By.XPath("//*[contains(@resource-id,'button_text') and contains(@text,'Избранное')]"),
            "Кнопка Избранное");

        private readonly Label SettlementLabel = new Label(
            By.XPath("//*[contains(@resource-id,'settlement_text')]"),
            "Текст города");

        public ProfilePage() : base(By.XPath("//*[contains(@resource-id,'toolbar')]//*[contains(@text,'Профиль')]"),
            "Профиль")
        { }

        public string GetCurrentSettlementText() => SettlementLabel.GetText();

        public bool IsLoginButtonDisplayed() => LoginButton.IsDisplayed;

        public void ClickSelectedButton() => SelectedButton.Click();
    }
}
