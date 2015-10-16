using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class TextFieldBehavior : ITextEditable, ITextReadable
    {
        private readonly SeleniumElement _element;
        public TextFieldBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void SetText(string textValue)
        {
            _element.WebElement.Clear();
            _element.WebElement.SendKeys(textValue);
        }

        public string GetText()
        {
            return _element.GetAttribute("value");
        }

        public void AppendText(string textValue)
        {
            _element.WebElement.Click();
            _element.WebElement.SendKeys(Keys.End);
            _element.WebElement.SendKeys(textValue);
        }

        public void Clear()
        {
            _element.WebElement.Clear();
        }
    }


}
