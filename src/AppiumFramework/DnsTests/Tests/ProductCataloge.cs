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
                _newPage = new CatalogIteractions()
                    .Add(() => CatalogIteractions.ClickCatalogItem("Аксессуары и услуги"), "Аксессуары и услуги")
                    .Add(() => CatalogIteractions.ClickCatalogItem("Для мобильных устройств"), "Для мобильных устройств")
                    .Add(() => CatalogIteractions.ClickCatalogItem("Карты памяти"), "Карты памяти")
                    .Execute();

                AssertLogger.That(_newPage.IsDisplayed, $"Отражается страница '{_newPage}'.");
            });

            LogManager.Step("4. Открыть фильтры", () =>
            {
                _newPage.ClickFilterButton();
                AssertLogger.That(DnsGui.FilterPage.IsDisplayed, $"Отражается страница '{DnsGui.FilterPage}'.");
            });

            LogManager.Step("5. В пункте 'Объём ГБ' выбрать указанный в ТК объём, нажать 'Применить'", () =>
            {
                DnsGui.FilterPage.SelectFilter("Объем (ГБ)");
                DnsGui.FilterPage.CheckItems(new string[] { "128" });
                DnsGui.FilterPage.ClickApplyButton();

                AssertLogger.That(_newPage.IsDisplayed, $"Отражается страница '{_newPage}'.");

                var products = _newPage.GetProductsInfo();
                var isExpected = products.Any(p => !p.ProductName.Contains("128"));

                AssertLogger.That(isExpected, $"Отражаются товары нужного объема {"128"}.");

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
                AssertLogger.That(buttonText.Equals("В корзине"), $"Кнопка изменилась на '{""}'");
                var productsCount = DnsGui.Navigation.GetBasketCount();
                AssertLogger.That(productsCount.Equals(1), 
                    $"Рядом с иконкой корзины в меню появилось число с количеством товаров в корзине - '{1}'");
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
                    var productInfo = DnsGui.BasketPage.GetProductsInfo().FirstOrDefault();

                    Assert.Multiple(() =>
                    {
                        AssertLogger.That(DnsGui.BasketPage.IsSnackBarPresented(),
                            $"Появился снек-бар(уведомление снизу) о том, что товар удалён.");
                        AssertLogger.That(DnsGui.BasketPage.IsBasketEmpty(), "Корзина пуста.");
                        AssertLogger.That((productInfo is null),
                            $"Товар1 больше не отображается на странице.");
                    });
                });
            });
        }
    }
}
