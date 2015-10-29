using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlRadioButton : SeleniumElement, ISelectable, ITextReadable
    {
        private readonly RadioButtonBehavior _radioButtonBehavior;

        public HtmlRadioButton()
        {
            _radioButtonBehavior = new RadioButtonBehavior(this);
        }

        public void Select()
        {
            _radioButtonBehavior.Select();
        }

        public bool IsSelected()
        {
            return _radioButtonBehavior.IsSelected();
        }

        public string GetText()
        {
            return _radioButtonBehavior.GetText();
        }
    }
}
