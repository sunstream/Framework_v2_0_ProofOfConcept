using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlButton : SeleniumElement, IClickable, ITextReadable
    {
        private readonly ButtonBehavior _buttonBehavior;
        private readonly TextFieldBehavior _textFieldBehavior;

        public HtmlButton()
        {
            _buttonBehavior = new ButtonBehavior(this);
            _textFieldBehavior = new TextFieldBehavior(this);
        }

        public void Click()
        {
            _buttonBehavior.Click();
        }

        public string GetText()
        {
            return _textFieldBehavior.GetText();
        }
    }
}
