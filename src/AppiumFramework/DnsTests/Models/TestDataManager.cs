using AppiumFramework.Utils;

namespace AppiumFramework.DnsTests.Models
{
    public static class TestDataManager
    {
        private static readonly string _parentDirectory = AppContext.BaseDirectory;
        private const string _dataPath = @"DnsTests\testData.json";
        private static TestDataModel? _data = null;

        public static TestDataModel TestData
        {
            get
            {
                _data ??= JsonReader.ReadFile<TestDataModel>($"{_parentDirectory}{_dataPath}");
                return _data;
            }
        }
    }
}
