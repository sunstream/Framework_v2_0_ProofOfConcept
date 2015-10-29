using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlCheckBox : SeleniumElement, ICheckable, ITextReadable
    {
        private readonly CheckBoxBehavior _checkBoxBehavior;
        private readonly TextFieldBehavior _textFieldBehavior;

        public HtmlCheckBox()
        {
            _textFieldBehavior = new TextFieldBehavior(this);
            _checkBoxBehavior = new CheckBoxBehavior(this);
        }

        public void Check()
        {
            _checkBoxBehavior.Check();
        }

        public bool IsChecked()
        {
            return _checkBoxBehavior.IsChecked();
        }

        public string GetText()
        {
            return _textFieldBehavior.GetText();
        }
    }
}
