using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlButton : SeleniumElement, ITextReadable
    {
        private readonly ButtonBehavior _buttonBehavior;

        public HtmlButton()
        {
            _buttonBehavior = new ButtonBehavior(this);
        }

        public string GetText()
        {
            return _buttonBehavior.GetText();
        }
    }
}
