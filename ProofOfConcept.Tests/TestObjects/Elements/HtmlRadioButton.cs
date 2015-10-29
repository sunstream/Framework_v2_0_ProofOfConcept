using Ninject;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
{
    public class HtmlRadioButton : ElementBase, IRadioButtonBehavior
    {
        private readonly IRadioButtonBehavior _radioButtonBehavior;

        public HtmlRadioButton()
        {
            _radioButtonBehavior = DependencyManager.Kernel.Get<IRadioButtonBehavior>();
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
