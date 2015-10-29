using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumElement : IElement
    {
 public OpenQA.Selenium.IWebElement WebElement
        {
            get
            {
                if (_webElement == null || !Exists || IsNewLookupAlwaysRequired )
                {
                    if (SearchConfiguration == null)
                    {
                        throw new Exception("No element, no search criteria");
                    }
                    _webElement = ((SeleniumElementFinder)SearchConfiguration).GetNativeElement();
                }
                return _webElement;
            }
            set { _webElement = value; }
        }

        public IElementSearchConfiguration SearchConfiguration { get; set; }

        private OpenQA.Selenium.IWebElement _webElement;

        private bool IsNewLookupAlwaysRequired
        {
            get
            {
                return SearchConfiguration != null && !SearchConfiguration.IsCachingAllowed;
            }
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

        public bool Displayed
        {
            get { return WebElement.Displayed; }
        }

        public bool MatchesFilter(FilterBy filterBy)
        {
            return filterBy.Check(this);
        }

        public bool MatchesAllFilters(params FilterBy[] filtersBy)
        {
            return filtersBy.All(searchFilter => searchFilter.Check(this));
        }

        public string GetAttribute(string attributeName)
        {
            return WebElement.GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            FindBy childrenLocator = new FindBy(How.Xpath, "./*");
            IElementSearchConfiguration childrenSearchConfiguration = new SeleniumElementFinder(childrenLocator, new SeleniumLocatorTransformer());
            return childrenSearchConfiguration.FindAll();

        }

        public void Click()
        {
            WebElement.Click();
        }

    }

    public static class WebElementExtension
    {
        public static bool IsEqualTo(this OpenQA.Selenium.IWebElement element1, OpenQA.Selenium.IWebElement element2)
        {
            const string equalityParam = "outerHTML";
            return element1 != null && element2 != null && element1.GetAttribute(equalityParam) == element2.GetAttribute(equalityParam);
        }
    }
}


