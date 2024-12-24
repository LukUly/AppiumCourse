using AppiumFramework.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AppiumFramework.Utils
{
    public static class AssertLogger
    {
        public static void That<T>(T actual, IResolveConstraint constraint)
        {
            try
            {
                Assert.That(actual, constraint);
                LogManager.LogInfo($"Проверка прошла успешно: {actual} соответствует {constraint}");
            }
            catch (AssertionException ex)
            {
                LogManager.LogError($"Проверка не прошла: {actual} не соответствует {constraint}. Ошибка: {ex.Message}");
                throw;
            }
        }
        
        public static void That(bool actual, string? message)
        {
            try
            {
                Assert.That(actual, message);
                LogManager.LogInfo($"Проверка прошла успешно: {message}");
            }
            catch (AssertionException ex)
            {
                LogManager.LogError($"Проверка не прошла: {message}. Ошибка: {ex.Message}");
                throw;
            }
        }

        public static void That(Func<bool> condition, string? message)
        {
            try
            {
                Assert.That(condition.Invoke(), Is.True, message); 
                LogManager.LogInfo($"Проверка прошла успешно: {message}");
            }
            catch (AssertionException ex)
            {
                LogManager.LogError($"Проверка не прошла: {message}. Ошибка: {ex.Message}");
                throw;
            }
        }
    }
}
