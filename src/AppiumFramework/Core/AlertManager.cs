using OpenQA.Selenium;

namespace AppiumFramework.Core
{
    public static class AlertManager
    {
        private static IAlert _alert;
        public static void SwitchToAlert()
        {
            try
            {
                LogManager.LogDebug("Переключение на Alert.");
                WaitManager.WaitForAlertToBePresent();
                _alert = AppiumDriver.Instance.SwitchTo().Alert();
            }
            catch (Exception ex)
            {
                LogManager.LogError(ex.Message);
                throw;
            }
        }

        public static void AcceptAlert()
        {
            LogManager.LogDebug("Подтверждение Alert.");
            _alert.Accept();
        }

        public static void DismissAlert()
        {
            LogManager.LogDebug("Отклонение Alert.");
            _alert.Dismiss();
        }

        public static string GetAlertText()
        {
            LogManager.LogDebug("Чтение текста из Alert.");
            var text = _alert.Text;
            LogManager.LogDebug($"Alert содержит текст {text}.");
            return text;
        }
    }
}
