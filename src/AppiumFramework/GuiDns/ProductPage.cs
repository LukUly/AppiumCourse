using AppiumFramework.Core.Base;
using AppiumFramework.DnsTests.Models;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class ProductPage : BasePage
    {
        private Label ProductNameLabel = new Label(By.XPath("//*[contains(@resource-id,'product_title_text')]"), "Название продукта");
        private Label ProductPriceLabel = new Label(By.XPath("//*[contains(@resource-id,'price_card')]//*[contains(@resource-id,'price_layout')]"), 
            "Цена продукта");        
        
        private Button ProductToBasketButton = new Button(By.XPath("//*[contains(@resource-id,'buy_button')]"), 
            "Купить");

        public ProductPage() : base(By.XPath("//*[contains(@resource-id,'product_code_button')]"),
           "Продукт")
        { }

        public void ClickBuyButton() => ProductToBasketButton.Click();

        public string GetBuyButtonText() => ProductToBasketButton.GetText();

        public ProductInfo GetProductInfo() 
        {
            return new ProductInfo(ProductNameLabel.GetText(), ProductPriceLabel.GetText());
        }
    }
}
