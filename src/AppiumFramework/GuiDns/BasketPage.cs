using AppiumFramework.Core;
using AppiumFramework.Core.Base;
using AppiumFramework.DnsTests.Models;
using AppiumFramework.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AppiumFramework.GuiDns
{
    public class BasketPage : BasePage
    {
        private static readonly string _universalProductLocator = "//*[contains(@resource-id,'item_card')]";
        private static readonly string _snackBarLocator = "//*[contains(@resource-id,'snackbar_text')]";
        private static readonly string _productLocatorSpecifire = "({0})[{1}]";
        private static readonly string _productPriceId = "cart_item_sum";


        private readonly Button GoToCatalogButton = new Button(
            By.XPath("//*[contains(@resource-id,'empty_content_action_button')]"),
            "Кнопка Перейти в каталог");

        private readonly Label EmtyContentLabel = new Label(
            By.XPath("//*[contains(@resource-id,'empty_content_summary_text')]"),
            "Текст пустого контента");

        private readonly Label TotalPriceLabel = new Label(
            By.XPath("//*[contains(@resource-id,'total_sum_text')]"),
            "Текст пустого контента");

        public BasketPage() : base(By.XPath("//*[contains(@resource-id,'toolbar')]//*[contains(@text,'Корзина')]"),
            "Корзина")
        { }

        private List<Product> GetProducts()
        {
            var products = new List<Product>();
            var items = AppManager.FindElements(By.XPath(_universalProductLocator));
            for (var i = 1; i <= items.Count; i++)
            {
                products.Add(new Product(By.XPath(string.Format(_productLocatorSpecifire, _universalProductLocator, i)),
                    $"Продукт {i}", _productPriceId));
            }

            return products;
        }

        public List<ProductInfo> GetProductsInfo()
        {
            var products = new List<ProductInfo>();
            var items = GetProducts();
            foreach (var item in items)
            {
                products.Add(item.Info);
            }

            return products;
        }

        public void DeleteByName(string name) 
        {
            var product = GetProducts().FirstOrDefault(x => x.ProductName.Contains(name))
               ?? throw new Exception($"Продукт с именем '{name}' не найден");
            product.DeleteFromBasket();
        }

        public bool IsBasketEmpty() => EmtyContentLabel.IsDisplayed;

        public bool IsGoToCatalogButtonDisplayed() => GoToCatalogButton.IsDisplayed;

        public string GetTotalPrice() => TotalPriceLabel.GetText();

        public bool IsSnackBarPresented()
        {
            try
            {
                WaitManager.WaitForElementToBeVisible(By.XPath(_snackBarLocator));
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
