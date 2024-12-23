using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class MainPage : BasePage
    {
        private readonly Button SettlementButton = new Button(
            By.XPath("//*[contains(@resource-id,'change_current_settlement_button')]"),
            "Текст города");

        public MainPage() : base(By.XPath("//*[contains(@resource-id,'logo_image')]"),
            "Главная")
        { }

        public string GetCurrentSettlementText() => SettlementButton.GetText();
    }
}
