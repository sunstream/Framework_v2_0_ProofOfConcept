using System;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class CheckBoxBehavior : ICheckable, ITextReadable
    {
        private readonly SeleniumElement _element;

        public CheckBoxBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void Check()
        {
            if (!IsChecked())
            {
                _element.Click();

                //TODO Add setting on performing or not verification afetr action
            }
        }

        public bool IsChecked()
        {
            return _element.WebElement.Selected;
        }

        public string GetText()
        {
            //return _element.WebElement.Text;
            /*
             *  Oops, we have to look for label control with given name or id in 'for' attribute
             */
            throw new NotImplementedException();
        }
    }
}
