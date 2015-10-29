using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium;
using ProofOfConcept.Selenium.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements

{
    public class HtmlTextField : SeleniumElement, ITextEditable, ITextReadable
    {
        private readonly TextFieldBehavior _textFieldBehavior;
        public HtmlTextField()
        {
            _textFieldBehavior = new TextFieldBehavior(this);
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