using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Tests.Component.TestObjects.Elements
{
    public class HtmlButton : ElementBase, IButtonBehavior
    {
        private readonly IButtonBehavior _buttonBehavior;

        public HtmlButton()
        {
            _buttonBehavior = AddBehavior<IButtonBehavior>();
        }

        public string GetText()
        {
            return _buttonBehavior.GetText();
        }
    }
}
