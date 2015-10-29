using System;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class RadioButtonBehavior : ISelectable, ITextReadable
    {
        private readonly SeleniumElement _element;

        public RadioButtonBehavior(SeleniumElement element)
        {
            _element = element;
        }

        public void Select()
        {
            if (!IsSelected())
            {
                _element.Click();

                //TODO Add setting on performing or not verification afetr action
            }
        }

        public bool IsSelected()
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
