using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class DeletePermissionPage : BasePage
    {
        private readonly Button PositiveButton = new Button(
            By.XPath("//*[contains(@resource-id,'positive_button')]"),
            "Кнопка подтверждения");

        public DeletePermissionPage() : base(By.XPath("//*[contains(@resource-id,'positive_button')]"),
            "Подтверждение удаления")
        { }

        public void ClickPositiveButton() => PositiveButton.Click();
    }
}
