using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlCheckBox : SeleniumElement, ICheckable, ITextReadable
    {
        private readonly CheckBoxBehavior _checkBoxBehavior;

        public HtmlCheckBox()
        {
            _checkBoxBehavior = new CheckBoxBehavior(this);
        }

        public void Check()
        {
            _checkBoxBehavior.Check();
        }

        public void Uncheck()
        {
            _checkBoxBehavior.Uncheck();
        }

        public bool IsChecked()
        {
            return _checkBoxBehavior.IsChecked();
        }

        public string GetText()
        {
            return _checkBoxBehavior.GetText();
        }
    }
}
