using AppiumFramework.Core;
using AppiumFramework.Core.Base;
using AppiumFramework.DnsTests.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AppiumFramework.Elements
{
    public class Product : BaseElement
    {
        private readonly By _locator;
        private readonly AppiumElement _productName;
        private readonly AppiumElement _productPrice;
        private static readonly string _actionButtonLocator = "//*[contains(@resource-id,'action_menu_button')]";
        private static readonly string _trashButtonLocator = "//*[contains(@resource-id,'trash_button')]";
        
        public Product(By locator, string name, string priceId) : base(locator, name)
        {
            _productName = AppManager.FindElement(By.XPath(locator.Criteria + "//*[contains(@resource-id,'product_title_text')]"));
            _productPrice = AppManager.FindElement(By.XPath(locator.Criteria + string.Format("//*[contains(@resource-id,'{0}')]", priceId)));
            _locator = locator;
        }

        public ProductInfo Info
        {
            get
            {
                return new ProductInfo(_productName.Text, _productPrice.Text);
            }
        } 
        
        public string ProductName
        {
            get
            {
                return _productName.Text;
            }
        }

        public void DeleteFromBasket() 
        {
            new Button(By.XPath(_locator.Criteria + _actionButtonLocator), "Кнопка действия").Click();
            new Button(By.XPath(_trashButtonLocator), "Кнопка удалить").Click();
        }

        public override void Click()
        {
            _productName.Click();
        }
    }
}
