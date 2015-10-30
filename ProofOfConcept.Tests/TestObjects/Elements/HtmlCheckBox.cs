using Ninject;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlCheckBox : ElementBase, ICheckable, ITextReadable
    {
        private readonly ICheckboxBehavior _checkBoxBehavior;

        public HtmlCheckBox()
        {
            _checkBoxBehavior = AddBehavior<ICheckboxBehavior>();
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
