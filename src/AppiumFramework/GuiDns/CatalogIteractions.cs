using OpenQA.Selenium;
using WindowsInput.Native;
using WindowsInput;
using AppiumFramework.Core;
using AppiumFramework.Elements;

namespace AppiumFramework.GuiDns
{
    public class CatalogIteractions
    {
        private static readonly string _universalMenuItem = "//*[contains(@resource-id,'title_text')]";
        private static readonly string _specificMenuItem = "//*[contains(@resource-id,'title_text') and contains(@text,'{0}')]";
        private static readonly List<Tuple<Action, string>> _actions = new List<Tuple<Action, string>>();

        public CatalogIteractions Add(Action action, string  name)
        {
            _actions.Add(new Tuple<Action, string>(action, name));
            return this;
        }

        public UniversalCatalogePage Execute()
        {
            foreach ((var action, var _) in _actions)
            {
                action();
            }

            var name = _actions.Last().Item2 as string;
            return new UniversalCatalogePage(name);
        }

        public static void ClickCatalogItem(string itemName)
        {
            var items = AppManager.FindElements(By.XPath(_universalMenuItem));
            var expectedItem = items.FirstOrDefault(item => item.Text.Equals(itemName));

            while (expectedItem is null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var sim = new InputSimulator();
                    sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                }

                items = AppManager.FindElements(By.XPath(_universalMenuItem));
                expectedItem = items.FirstOrDefault(item => item.Text.Equals(itemName));
            }
            expectedItem.Click();
            WaitManager.WaitForElementToDisappear(new Button(By.XPath(
                string.Format(_specificMenuItem, itemName)), itemName));
        }
    }
}
