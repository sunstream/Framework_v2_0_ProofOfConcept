using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    class SeleniumElement : IElement
    {
        public IWebElement WebElement { get; private set; }

        public SeleniumElement(IWebElement webElement)
        {
            WebElement = webElement;
        }
        
        public bool Exists
        {
            get
            {
                bool exists = false;
                if (WebElement != null)
                {
                    try
                    {
                        WebElement.GetAttribute("innerHTML");
                        exists = true;
                    }
                    catch (StaleElementReferenceException) {}
                }
                return exists;
            }
        }

        public bool Equals(IElement element)
        {
            if (element.GetType().IsAssignableFrom(typeof (SeleniumElement)))
            {
                return this.WebElement.IsEqualTo(((SeleniumElement) element).WebElement);
            }
            return false;
        }

        public IElement FindElement(ILocator locator, params SearchFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public IElement FindElement(ILocators locators, params SearchFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public IList<IElement> FindElements(ILocators locators, params SearchFilter[] filters)
        {
            throw new NotImplementedException();
        }

        public bool Displayed
        {
            get { return WebElement.Displayed; }
        }

        public bool MatchesFilter(SearchFilter filter)
        {
            return filter.Check(this);
        }

        public bool MatchesAllFilters(params SearchFilter[] filters)
        {
            return filters.All(searchFilter => searchFilter.Check(this));
        }

        public string GetAttribute(string attributeName)
        {
            return WebElement.GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            throw new NotImplementedException();
        }
    }

    public static class WebElementExtension
    {
        public static bool IsEqualTo(this IWebElement element1, IWebElement element2)
        {
            const string equalityParam = "outerHTML";
            return element1 != null && element2 != null && element1.GetAttribute(equalityParam) == element2.GetAttribute(equalityParam);
        }
    }
}


