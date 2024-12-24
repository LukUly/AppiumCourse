using Newtonsoft.Json;

namespace AppiumFramework.Utils
{
    public static class JsonReader
    {
        public static T ReadFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file '{filePath}' not found.");
            }

            using var reader = new StreamReader(filePath);
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
