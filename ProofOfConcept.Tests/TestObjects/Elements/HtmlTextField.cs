
using ProofOfConcept.Behaviors;
using Ninject;

namespace ProofOfConcept.Tests.TestObjects.Elements

{
    public class HtmlTextField : ElementBase, ITextFieldBehavior
    {
        private readonly ITextFieldBehavior _textFieldBehavior;
        public HtmlTextField()
        {
            _textFieldBehavior = DependencyManager.Kernel.Get<ITextFieldBehavior>();
        }
        
        public void SetText(string textValue)
        {
            _textFieldBehavior.SetText(textValue);
        }

        public string GetText()
        {
            return _textFieldBehavior.GetText();
        }

        public void AppendText(string textValue)
        {
            _textFieldBehavior.AppendText(textValue);
        }

        public void Clear()
        {
            _textFieldBehavior.Clear();
        }
    }

}
