using System;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class ButtonBehavior : IClickable
    {
        private readonly SeleniumElement _element;

        public ButtonBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void Click()
        {
            _element.WebElement.Click();
        }
    }
}
