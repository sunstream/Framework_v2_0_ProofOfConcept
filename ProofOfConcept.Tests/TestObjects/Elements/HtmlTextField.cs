
using ProofOfConcept.Behaviors;
using Ninject;

namespace ProofOfConcept.Tests.TestObjects.Elements

{
    public class HtmlTextField : ElementBase, ITextFieldBehavior
    {
        private ITextFieldBehavior _textFieldBehavior;

        //private ITextFieldBehavior TextFieldBehavior
        //{
        //    get
        //    {
        //        if (_textFieldBehavior == null)
        //        {
        //            _textFieldBehavior = DependencyManager.Kernel.Get<ITextFieldBehavior>();
        //            _textFieldBehavior.Element = this;
        //        }
        //        return _textFieldBehavior;
        //    }
        //}
        public HtmlTextField()
        {
            var element = new Ninject.Parameters.ConstructorArgument("element", this);
            _textFieldBehavior = DependencyManager.Kernel.Get<ITextFieldBehavior>(element);
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
