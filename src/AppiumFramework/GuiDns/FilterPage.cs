using AppiumFramework.Core.Base;
using AppiumFramework.Elements;
using OpenQA.Selenium;

namespace AppiumFramework.GuiDns
{
    public class FilterPage : BasePage
    {
        private static readonly string _universalFilterLocator = "//*[contains(@resource-id,'filter_list')]//*[contains(@text,'{0}')]";
        private static readonly string _universalCheckBoxLocator = "//*[contains(@resource-id,'check') and contains(@text,'{0}')]";

        private readonly Input CheckBoxFilter = new Input(By.XPath("//*[contains(@resource-id,'search_edit') and @text='Поиск']"),
            "Фильтр чекбоксов");

        private readonly Button ApplyButton = new Button(By.XPath("//*[contains(@resource-id,'apply_button')]"),
            "Кнопка применить");

        public FilterPage() : base(By.XPath("//*[contains(@resource-id,'toolbar')]//*[contains(@text,'Фильтры')]"),
           "Фильтры")
        { }

        public void FillInCheckBoxFilter(string text) => CheckBoxFilter.SendKeys(text);

        public void SelectFilter(string name) 
        {
            new Button(By.XPath(string.Format(_universalFilterLocator, name)),
                $"Кнопка Фильтра {name}").Click();
        }

        public void CheckItem(string name) 
        {
            FillInCheckBoxFilter(name);
            new CheckBox(By.XPath(string.Format(_universalCheckBoxLocator, name)),
                $"Кнопка Фильтра {name}").Check();
        }

        public void CheckItems(IEnumerable<string> names) 
        {
            foreach (var name in names) 
            {
                CheckItem(name);
            }
        }

        public void ClickApplyButton() => ApplyButton.Click();
    }
}
