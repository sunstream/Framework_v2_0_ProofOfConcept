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
            
            
        }

        public IElement FindElement(ILocators locators, params SearchFilter[] filters)
        {
            foreach (ILocator locator in locators)
            {
                
            }
            //locators
        }
        
        public IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters)
        {
            By seleniumLocator = SeleniumLocatorTransformer.GetNativeLocator(locator);
            IList<IWebElement> webElements = FindElementsBySingleLocator(seleniumLocator);
            IList<IElement> elements = webElements.Select(webElement => new SeleniumElement(webElement)).Cast<IElement>().ToList();
            IList<IElement> filteredElements = FilterElements(elements);
            return filteredElements;
        }

        public IList<IElement> FindElements(ILocators locators, params SearchFilter[] filters)
        {
            throw new System.NotImplementedException();
        }

        private IList<IWebElement> FindElementsBySingleLocator(By seleniumLocator)
        {
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