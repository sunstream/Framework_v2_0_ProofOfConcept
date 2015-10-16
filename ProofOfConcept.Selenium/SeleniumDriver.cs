using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriver : IDriverDecorator
    {
        private ILocatorTransformer<By> locatorTransformer = new SeleniumLocatorTransformer(); 
        private readonly IWebDriver _driver;

        public SeleniumDriver(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public IElement FindElement(IFindBy findBy, params FilterBy[] filtersBy)
        {
            IList<IElement> allElements = FindElements(findBy, filtersBy);
            return allElements.FirstOrDefault();
        }

        public IList<IElement> FindElements(IFindBy findBy, params FilterBy[] filtersBy)
        {
            IList<IWebElement> webElements = FindElementsBySingleLocator(findBy);
            IList<IElement> elements = WrapWebElements(webElements);
            IList<IElement> filteredElements = FilterElements(elements);
            return filteredElements;
        }
        
        private IList<IElement> FilterElements(IList<IElement> elements, params FilterBy[] filtersBy)
        {
            IList<IElement> filteredElements = new List<IElement>();
            foreach (IElement element in elements)
            {
                if (element.MatchesAllFilters(filtersBy))
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

        private IList<IWebElement> FindElementsBySingleLocator(IFindBy findBy)
        {
            By seleniumLocator = locatorTransformer.GetNativeLocator(findBy);
            return _driver.FindElements(seleniumLocator);
        }
        #endregion

        public IElement FindElement(FindBy findBy, params FilterBy[] filters)
        {
            throw new System.NotImplementedException();
        }

        public IList<IElement> FindElements(FindBy findBy, params FilterBy[] filters)
        {
            throw new System.NotImplementedException();
        }
    }
}