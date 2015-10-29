using System;
using System.Linq;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class SeleniumCheckboxBehavior : ICheckboxBehavior
    {
        public ElementBase Element { get; set; }
        private IWebElement NativeElement
        {
            get
            {
                return ((IWebElement)Element.NativeElement);
            }
        }

        public SeleniumCheckboxBehavior(ElementBase element)
        {
            Element = element;
        }

        public void Check()
        {
            if (!IsChecked())
            {
                NativeElement.Click();
            }
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                NativeElement.Click();
            }
        }

        public bool IsChecked()
        {
            return NativeElement.Selected;
        }

        public string GetText()
        {
            var parent = NativeElement.FindElement(By.XPath(".."));
            if (parent.TagName.Equals("label", StringComparison.InvariantCultureIgnoreCase))
            {
                return parent.Text;
            }

            var label = parent.FindElements(By.TagName("label")).FirstOrDefault(IsLabelElementFor);
            return label == null ? string.Empty : label.Text;
        }

        private bool IsLabelElementFor(IWebElement labelElement)
        {
            var forValue = labelElement.GetAttribute("for");
            var idValue = NativeElement.GetAttribute("id");
            var nameValue = NativeElement.GetAttribute("name");

            return forValue.Equals(idValue, StringComparison.InvariantCultureIgnoreCase) || forValue.Equals(nameValue, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
