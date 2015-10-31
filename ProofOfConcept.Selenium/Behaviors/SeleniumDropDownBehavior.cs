using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class SeleniumDropDownBehavior : IDropDownBehavior
    {
        public ElementBase Element { get; set; }
        private IWebElement NativeElement
        {
            get
            {
                return ((IWebElement)Element.NativeElement);
            }
        }

        public void Select(string value)
        {
            SelectElement s = new SelectElement(NativeElement);
            s.SelectByText(value);
        }

        public bool IsSelected(string value)
        {
            SelectElement s = new SelectElement(NativeElement);
            IList<IWebElement> selectedOptions = s.AllSelectedOptions;
            if (selectedOptions.Any(option => option.Text == value))
            {
                return true;
            }
            return false;

        }

        public string GetSelected()
        {
            SelectElement s = new SelectElement(NativeElement);
            IWebElement selectedOption = s.AllSelectedOptions.FirstOrDefault();
            return selectedOption == null ? null : selectedOption.Text;
        }
    }
}