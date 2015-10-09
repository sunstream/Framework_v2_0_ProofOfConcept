using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriver : IDriverDecorator
    {
        private IWebDriver _driver;
        
        public IElement FindElement(ILocator locator, params SearchFilter[] filters)
        {
            IList<IElement> allElements = FindElements(locator, filters);
            return allElements.FirstOrDefault();
        }

        public IElement FindElement(ILocators locators, params SearchFilter[] filters)
        {
            IList<IWebElement> webElements = FindElementsBySingleLocator(locators.First());
            if (locators.Count > 1)
            {
                for (int i = 1; i < locators.Count; i++)
                {
                    IList<IWebElement> additionalWebElements = FindElementsBySingleLocator(locators[i]);

                }
            }
            //locators
        }
        
        public IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters)
        {
            IList<IWebElement> webElements = FindElementsBySingleLocator(locator);
            IList<IElement> elements = webElements.Select(webElement => new SeleniumElement(webElement)).Cast<IElement>().ToList();
            IList<IElement> filteredElements = FilterElements(elements);
            return filteredElements;
        }

        public IList<IElement> FindElements(ILocators locators, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        private IList<IWebElement> FindElementsBySingleLocator(ILocator locator)
        {
            By seleniumLocator = SeleniumLocatorTransformer.GetNativeLocator(locator);
            return _driver.FindElements(seleniumLocator);
        }

        private IList<IElement> FilterElements(IList<IElement> elements, params SearchFilter[] filters)
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
    }
}