using System;
using System.Linq;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class RadioButtonBehavior : ISelectable, ITextReadable
    {
        private readonly SeleniumElement _element;

        public RadioButtonBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void Select()
        {
            if (!IsSelected())
            {
                _element.Click();
            }
        }

        public bool IsSelected()
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
