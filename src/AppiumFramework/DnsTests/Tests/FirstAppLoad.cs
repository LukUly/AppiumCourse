using AppiumFramework.Core;
using AppiumFramework.DnsTests.Models;
using AppiumFramework.Utils;
using AppiumFramework.GuiDns;
using NUnit.Framework;

namespace AppiumFramework.DnsTests.Tests
{
    public class FirstAppLoad : BaseTest
    {
        private string _currentSettlement = string.Empty;

        [Test(Description = "Первый запуск приложения")]
        public void FirstAppLoadTest()
        {
            LogManager.Step("1. Запустить приложение", () =>
            {
                AssertLogger.That(DnsGui.IntroPage.IsDisplayed, $"Отражается страница '{DnsGui.IntroPage}'." +
                    $"Приложение запущено впервые.");

                _currentSettlement = StringUtils.ExtractCityName(DnsGui.IntroPage.GetCurrentSettlementText());
                AssertLogger.That(_currentSettlement,
                    Does.Contain(TestDataManager.TestData.FirstAppLoadDataModel.ExpectedSettlement));
            });

            LogManager.Step("2. Запомнить указанный город, подтвердить выбор города", () =>
            {
                LogManager.LogInfo($"Узаканный город: {_currentSettlement}");

                DnsGui.IntroPage.ClickConfirmSettlementButton();

                AssertLogger.That(DnsGui.FirstLoginPage.IsDisplayed, $"Отражается страница '{DnsGui.FirstLoginPage}'." +
                    $"Отображается экран с предложением войти в учётную запись.");
            });

            LogManager.Step("3. Нажать 'Войти позже'", () =>
            {
                DnsGui.FirstLoginPage.ClickSkipAuthButton();
                AlertManager.SwitchToAlert();
                var alertText = AlertManager.GetAlertText();
                AssertLogger.That(alertText, Does.Contain(
                    TestDataManager.TestData.FirstAppLoadDataModel.ExpectedNotificationText));
            });

            LogManager.Step("4. Разрешить отправку уведомлений", () =>
            {
                AlertManager.AcceptAlert();

                AssertLogger.That(DnsGui.MainPage.IsDisplayed, $"Отражается страница '{DnsGui.MainPage}'.");
                AssertLogger.That(DnsGui.MainPage.GetCurrentSettlementText(), Does.Contain("Москва"));
            });

            LogManager.Step("5. Нажать на иконку с корзиной в нижнем меню", () =>
            {
                DnsGui.Navigation.GoToBasketPage();

                AssertLogger.That(DnsGui.BasketPage.IsDisplayed, $"Отражается страница '{DnsGui.BasketPage}'.");
                Assert.Multiple(() =>
                {
                    AssertLogger.That(DnsGui.BasketPage.IsBasketEmpty(), "Корзина пуста.");
                    AssertLogger.That(DnsGui.BasketPage.IsGoToCatalogButtonDisplayed(),
                        $"Отражается кнопка 'Перейти в каталог'.");
                });
            });

            LogManager.Step("6. Перейти в профиль (икона с человечком справа в меню навигации)", () =>
            {
                DnsGui.Navigation.GoToProfilePage();

                AssertLogger.That(DnsGui.ProfilePage.IsDisplayed, $"Отражается страница '{DnsGui.ProfilePage}'.");

                Assert.Multiple(() =>
                {
                    AssertLogger.That(DnsGui.ProfilePage.IsLoginButtonDisplayed(), "Отражается кнопка 'Войти'.");
                    AssertLogger.That(DnsGui.ProfilePage.GetCurrentSettlementText(), Does.Contain("Москва"));
                });
            });

            LogManager.Step("7. Нажать на кнопку 'Избранное'", () =>
            {
                DnsGui.ProfilePage.ClickSelectedButton();

                AssertLogger.That(DnsGui.SelectedPage.IsDisplayed, $"Отражается страница '{DnsGui.SelectedPage}'.");

                Assert.Multiple(() =>
                {
                    AssertLogger.That(DnsGui.SelectedPage.IsSelectedEmpty(), "Избранное пусто.");
                    AssertLogger.That(DnsGui.SelectedPage.IsLoginButtonDisplayed(), "Отражается кнопка 'Войти'.");
                    AssertLogger.That(DnsGui.SelectedPage.IsGoToCatalogButtonDisplayed(),
                        "Отражается кнопка 'Перейти в каталог'.");
                });
            });
        }
    }
}
