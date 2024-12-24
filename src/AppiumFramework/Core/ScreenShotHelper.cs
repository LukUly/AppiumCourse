namespace AppiumFramework.Core
{
    public static class ScreenShotHelper
    {
        public static void CaptureScreenshot(string path, string fileName = null)
        {
            try
            {
                var screenshot = AppiumDriver.Instance.GetScreenshot();

                fileName ??= GenerateScreenshotName();
                var filePath = Path.Combine(path, fileName);
                screenshot.SaveAsFile(filePath);
                LogManager.LogInfo($"Скриншот сохранен: {filePath}");
            }
            catch (Exception ex)
            {
                LogManager.LogDebug($"Ошибка при захвате скриншота: {ex.Message}");
            }
        }

        private static string GenerateScreenshotName()
        {
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string screenshotName = $"Screenshot_{formattedDate}.png";

            return screenshotName;
        }
    }
}
