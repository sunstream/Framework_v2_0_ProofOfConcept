using System;
using System.Linq;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class CheckBoxBehavior : ICheckable, ITextReadable
    {
        private readonly SeleniumElement _element;

        public CheckBoxBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void Check()
        {
            if (!IsChecked())
            {
                _element.Click();
            }
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                _element.Click();
            }
        }

        public bool IsChecked()
        {
            return _element.WebElement.Selected;
        }

        public string GetText()
        {
            var parent = _element.WebElement.FindElement(By.XPath(".."));
            if (parent.TagName.Equals("label", StringComparison.InvariantCultureIgnoreCase))
            {
                return parent.Text;
            }

            var label = parent.FindElements(By.TagName("label")).FirstOrDefault(IsLabelElemetFor);
            return label == null ? string.Empty : label.Text;
        }

        private bool IsLabelElemetFor(IWebElement labelElement)
        {
            var forValue = labelElement.GetAttribute("for");
            var idValue = _element.GetAttribute("id");
            var nameValue = _element.GetAttribute("name");

            return forValue.Equals(idValue, StringComparison.InvariantCultureIgnoreCase) || forValue.Equals(nameValue, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
