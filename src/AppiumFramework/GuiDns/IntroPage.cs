using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class IntroPage : BasePage
    {
        private readonly Button ConfirmSettlementButton = new Button(
            By.XPath("//*[contains(@resource-id, 'confirm_current_settlement_button')]"), 
            "Кнопка подтверждения города");

        private readonly Label CurrentSettlementLabel = new Label(
            By.XPath("//*[contains(@resource-id, 'current_settlement_text')]"), 
            "Текст текущего города");

        public IntroPage() : base(By.XPath("//*[contains(@resource-id, 'current_settlement_title_text')]"), 
            "Выбор города")
        { }

        public string GetCurrentSettlementText() => CurrentSettlementLabel.GetText();

        public void ClickConfirmSettlementButton() => ConfirmSettlementButton.Click();
    }
}
