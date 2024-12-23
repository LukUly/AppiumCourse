using AppiumFramework.Elements;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace AppiumFramework.GuiDns
{
    public class Navigation
    {
        private static readonly string _universalXpath = "//*[contains(@content-desc,'{0}')]";
        
        private readonly Button BasketButton = new Button(By.XPath(String.Format(_universalXpath, "Корзина")),
            "Кнопка меню Корзина");
        
        private readonly Button ProfileButton = new Button(By.XPath(String.Format(_universalXpath, "Профиль")),
            "Кнопка меню Профиль");

        private readonly Button CatalogButton = new Button(By.XPath(String.Format(_universalXpath, "Каталог")),
            "Кнопка меню Каталог");

        private static int ExtractNotificationCount(Button button)
        {
            var message = button.GetContentDesc();
            string pattern = @"(d+)s+новоеs+уведомление";

            Match match = Regex.Match(message, pattern);

            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }

            return 0;
        }
        public int GetBasketCount() => ExtractNotificationCount(BasketButton);

        public void GoToBasketPage() => BasketButton.Click();

        public void GoToProfilePage() => ProfileButton.Click();

        public void GoToCatalogPage() => CatalogButton.Click();
    }
}
