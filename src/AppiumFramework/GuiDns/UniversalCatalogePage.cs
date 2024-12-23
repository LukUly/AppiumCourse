using AppiumFramework.Core;
using AppiumFramework.Core.Base;
using AppiumFramework.DnsTests.Models;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public  class UniversalCatalogePage : BasePage
    {
        private static readonly string _universalPageLocator = "//*[contains(@resource-id,'toolbar')]//*[contains(@text,'{0}')]";
        private static readonly string _universalProductLocator = "//*[contains(@resource-id,'product_list')]/androidx.cardview.widget.CardView";
        private static readonly string _productLocatorSpecifire = "({0})[{1}]";
        private static readonly string _productPriceId = "current_price_text";

        private readonly Button FilterButton = new Button(By.XPath("//*[contains(@resource-id,'filter_button')]"),
            "Кнопка Фильтр");

        public UniversalCatalogePage(string name) : base(By.XPath(String.Format(_universalPageLocator, name)),
           name)
        { }

        public void ClickFilterButton() => FilterButton.Click();

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

        public void ClickProductByName(string name) 
        {
            var product = GetProducts().FirstOrDefault(x => x.ProductName.Contains(name))
                ?? throw new Exception($"Продукт с именем '{name}' не найден");
            product.Click();
        }
    }
}
