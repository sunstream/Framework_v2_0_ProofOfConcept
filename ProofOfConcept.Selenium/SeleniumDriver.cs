using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriver : IDriverDecorator
    {
        private IWebDriver _driver;
        
        public IElement FindElement(ILocator locator, params ISearchFilter[] filters)
        {
            IList<IElement> allElements = FindElements(locator, filters);
            return allElements.FirstOrDefault();
        }

        public IList<IElement> FindElements(ILocator locator, params ISearchFilter[] filters)
        {
            IList<IWebElement> webElements = FindElementsBySingleLocator(locator);
            IList<IElement> elements = WrapWebElements(webElements);
            IList<IElement> filteredElements = FilterElements(elements);
            return filteredElements;
        }
        
        private IList<IElement> FilterElements(IList<IElement> elements, params ISearchFilter[] filters)
        {
            IList<IElement> filteredElements = new List<IElement>();
            foreach (IElement element in elements)
            {
                if (element.MatchesAllFilters(filters))
                {
                    filteredElements.Add(element);
                }
            }
            return filteredElements;
        }

        #region References To Native Element
        private IList<IElement> WrapWebElements(IList<IWebElement> webElements)
        {
            IList<IElement> elements = new List<IElement>();
            foreach (IWebElement element in webElements)
            {
                elements.Add(new SeleniumElement(element));
            }
            return elements;
        }

        private IList<IWebElement> FindElementsBySingleLocator(ILocator locator)
        {
            By seleniumLocator = SeleniumLocatorTransformer.GetNativeLocator(locator);
            return _driver.FindElements(seleniumLocator);
        }
        #endregion
    }
}