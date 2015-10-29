using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class SeleniumButtonBehavior : IButtonBehavior
    {
        public ElementBase Element { get; set; }
        private IWebElement NativeElement
        {
            get
            {
                return ((IWebElement)Element.NativeElement);
            }
        }

        public SeleniumButtonBehavior(ElementBase element)
        {
            Element = element;
        }

        public string GetText()
        {
            return NativeElement.Text;
        }
    }

    
}
