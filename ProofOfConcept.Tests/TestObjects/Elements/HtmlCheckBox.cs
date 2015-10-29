using Ninject;
using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlCheckBox : ElementBase, ICheckable, ITextReadable
    {
        private readonly ICheckboxBehavior _checkBoxBehavior;

        public HtmlCheckBox()
        {
            _checkBoxBehavior = DependencyManager.Kernel.Get<ICheckboxBehavior>();
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
