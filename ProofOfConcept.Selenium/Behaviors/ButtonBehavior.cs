using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class ButtonBehavior : ITextReadable
    {
        private readonly SeleniumElement _element;

        public ButtonBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public string GetText()
        {
            return _element.WebElement.Text;
        }
    }
}
