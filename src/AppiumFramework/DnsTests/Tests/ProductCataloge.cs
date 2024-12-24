using AppiumFramework.Core;
using AppiumFramework.Utils;
using AppiumFramework.GuiDns;
using NUnit.Framework;
using AppiumFramework.DnsTests.Models;

namespace AppiumFramework.DnsTests.Tests
{
    public class ProductCatalog : BaseTest
    {
        private UniversalCatalogePage _newPage;
        private ProductInfo _product;

        [Test(Description = "Взаимодействие с каталогом товаров")]
        public void ProductCatalogTest()
        {
            LogManager.Step("1. Запустить приложение", () =>
            {
                AssertLogger.That(DnsGui.MainPage.IsDisplayed, $"Отражается страница '{DnsGui.MainPage}'.");
            });

            LogManager.Step("2. Нажать на иконку каталога в нижнем меню (список с лупой)", () =>
            {
                DnsGui.Navigation.GoToCatalogPage();
                AssertLogger.That(DnsGui.CatalogPage.IsDisplayed, $"Отражается страница '{DnsGui.CatalogPage}'.");
            });

            LogManager.Step("3. Перейти в меню 'Аксессуары и услуги > Для мобильных устройств > Карты памяти'", () =>
            {
                var catalogIteractions = new CatalogIteractions();
                foreach (var item in TestDataManager.TestData.ProductCatalogeModel.MenuItems) 
                {
                    catalogIteractions.Add(() => CatalogIteractions.ClickCatalogItem(item), item);
                }
                _newPage = catalogIteractions.Execute();

                AssertLogger.That(_newPage.IsDisplayed, $"Отражается страница '{_newPage}'.");
            });

            LogManager.Step("4. Открыть фильтры", () =>
            {
                _newPage.ClickFilterButton();
                AssertLogger.That(DnsGui.FilterPage.IsDisplayed, $"Отражается страница '{DnsGui.FilterPage}'.");
            });

            LogManager.Step("5. В пункте 'Объём ГБ' выбрать указанный в ТК объём, нажать 'Применить'", () =>
            {
                var volume = TestDataManager.TestData.ProductCatalogeModel.CardVolume;
                var filter = TestDataManager.TestData.ProductCatalogeModel.FilterText;
                DnsGui.FilterPage.SelectFilter(filter);
                DnsGui.FilterPage.CheckItems(new string[] { volume });
                DnsGui.FilterPage.ClickApplyButton();

                AssertLogger.That(_newPage.IsDisplayed, $"Отражается страница '{_newPage}'.");

                var products = _newPage.GetProductsInfo();
                var isExpected = products.All(prod => prod.ProductName.Contains(volume));

                AssertLogger.That(isExpected, $"Отражаются товары нужного объема {volume}.");

                _product = products.First();
            });

            LogManager.Step("6. Для первого товара в списке запомнить цену и название - далее цена1 и товар1", () =>
            {
                LogManager.LogInfo($"{_product}");
            });

            LogManager.Step("7. Открыть страницу товара1", () =>
            {
                _newPage.ClickProductByName(_product.ProductName);
                AssertLogger.That(DnsGui.ProductPage.IsDisplayed, $"Отражается страница '{DnsGui.ProductPage}'.");

                var productInfo = DnsGui.ProductPage.GetProductInfo();
                AssertLogger.That(productInfo.Equals(_product), "Цена товара совпадает с ценой ценой1.");
            });

            LogManager.Step("8. Нажать кнопку 'Купить'", () =>
            {
                DnsGui.ProductPage.ClickBuyButton();

                var buttonText = DnsGui.ProductPage.GetBuyButtonText();
                AssertLogger.That(buttonText.Equals(TestDataManager.TestData.ProductCatalogeModel.NewBuyButtonText), 
                    $"Кнопка изменилась на '{TestDataManager.TestData.ProductCatalogeModel.NewBuyButtonText}'");
                var productsCount = DnsGui.Navigation.GetBasketCount();
                AssertLogger.That(productsCount.Equals(TestDataManager.TestData.ProductCatalogeModel.CountBasketItems), 
                    $"Рядом с иконкой корзины в меню появилось число с количеством товаров в корзине - " +
                    $"'{TestDataManager.TestData.ProductCatalogeModel.CountBasketItems}'");
            });

            LogManager.Step("9. Нажать на кнопку корзины в нижнем меню", () =>
            {
                DnsGui.Navigation.GoToBasketPage();

                AssertLogger.That(DnsGui.BasketPage.IsDisplayed, $"Отражается страница '{DnsGui.BasketPage}'.");
                var productInfo = DnsGui.BasketPage.GetProductsInfo().FirstOrDefault();
                var totalPrice = DnsGui.BasketPage.GetTotalPrice();

                Assert.Multiple(() =>
                {
                    AssertLogger.That(productInfo.Equals(_product), "Цена товара совпадает с ценой ценой1.");
                    AssertLogger.That(totalPrice.Equals(_product.ProductPrice),
                        "Общая цена товаров в корзине совпадает с ценой1.");
                });
            });

            LogManager.Step("10. Удалить товар1 из корзины", () =>
            {
                LogManager.Step("10.1. Нажать на значок с мусорным баком рядом с товаром", () =>
                {
                    DnsGui.BasketPage.DeleteByName(_product.ProductCode);
                    AssertLogger.That(DnsGui.DeletePermissionPage.IsDisplayed, 
                        $"Отражается страница '{DnsGui.DeletePermissionPage}'.");
                });

                LogManager.Step("10.2. Подтвердить удаление товара из корзины", () =>
                {
                    DnsGui.DeletePermissionPage.ClickPositiveButton();

                    Assert.Multiple(() =>
                    {
                        AssertLogger.That(DnsGui.BasketPage.IsSnackBarPresented(),
                            $"Появился снек-бар(уведомление снизу) о том, что товар удалён.");
                        AssertLogger.That(DnsGui.BasketPage.IsBasketEmpty(), "Корзина пуста.");
                        AssertLogger.That((DnsGui.BasketPage.GetProductsInfo().FirstOrDefault() is null),
                            $"Товар1 больше не отображается на странице.");
                    });
                });
            });
        }
    }
}
