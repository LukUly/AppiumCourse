using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class SelectedPage : BasePage
    {
        private readonly Button LoginButton = new Button(
            By.XPath("//*[contains(@resource-id, 'login_button')]"),
            "Кнопка Войти");

        private readonly Label EmtyContentLabel = new Label(
            By.XPath("//*[contains(@resource-id,'empty_content_summary_text')]"),
            "Текст пустого контента");

        private readonly Button GoToCatalogButton = new Button(
            By.XPath("//*[contains(@resource-id,'empty_content_action_button')]"),
            "Кнопка Перейти в каталог");

        public SelectedPage() : base(By.XPath("//*[contains(@resource-id,'toolbar')]//*[contains(@text,'Избранное')]"),
            "Избранное")
        { }

        public bool IsSelectedEmpty() => EmtyContentLabel.IsDisplayed;

        public bool IsGoToCatalogButtonDisplayed() => GoToCatalogButton.IsDisplayed;

        public bool IsLoginButtonDisplayed() => LoginButton.IsDisplayed;
    }
}
