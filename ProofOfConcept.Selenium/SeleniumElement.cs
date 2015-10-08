using System.Collections.Generic;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    class SeleniumElement : IElement
    {
        private IWebElement _webElement;
        public SeleniumElement(IWebElement webElement)
        {
            _webElement = webElement;
        }

        public IElement FindElement(ILocator locator, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        public IElement FindElement(ILocators locators, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        public IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        public IList<IElement> FindElements(ILocators locators, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        public bool Displayed
        {
            get { return _webElement.Displayed; }
        }

        public string GetAttribute(string attributeName)
        {
            return _webElement.GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            throw new System.NotImplementedException();
        }
    }
}
