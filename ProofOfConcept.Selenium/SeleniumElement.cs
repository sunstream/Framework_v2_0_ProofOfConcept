using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumElement : IElement
    {
        public IWebElement WebElement
        {
            get
            {
                if (_webElement == null || !Exists )
                {
                    if (SearchConfiguration == null)
                    {
                        throw new Exception("No element, no search criteria");
                    }
                    _webElement = SearchConfiguration.GetNativeElement();
                }
                return _webElement;
            }
            set { _webElement = value; }
        }

        public IElementFinder<IWebElement> SearchConfiguration { get; set; }

        private IWebElement _webElement;
       
        public SeleniumElement(IElementFinder<IWebElement> searchCofiguration)
        {
            this.SearchConfiguration = searchCofiguration;
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
            throw new NotImplementedException();
        }

        public void Click()
        {
            WebElement.Click();
        }

        //public void Validate()
        //{
        //    throw new NotImplementedException();
        //}

        public IElement FindElement(FindBy findBy, params FilterBy[] filters)
        {
            throw new NotImplementedException();
        }

        public IList<IElement> FindElements(FindBy findBy, params FilterBy[] filters)
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


