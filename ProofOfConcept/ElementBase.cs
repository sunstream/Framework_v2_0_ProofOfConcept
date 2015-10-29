using System;
using System.Collections.Generic;
using System.Linq;

namespace ProofOfConcept
{
    public abstract class ElementBase<TNativeElement> : IElement
    {
        public IElementSearchConfiguration<TNativeElement> SearchConfiguration { get; set; }

        private dynamic _webElement;

        public ElementBase(IElementSearchConfiguration<TNativeElement> searchCofiguration)
        {
            this.SearchConfiguration = searchCofiguration;
        }

        public dynamic WebElement
        {
            get
            {
                if (_webElement == null || !Exists || IsNewLookupAlwaysRequired)
                {
                    if (SearchConfiguration == null)
                    {
                        throw new Exception("No element, on search criteria");
                    }
                    _webElement = SearchConfiguration.GetNativeElement();
                }
                return _webElement;
            }
            set { _webElement = value; }
        }



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
                    //catch (StaleElementReferenceException) { }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                return exists;
            }
        }

        public bool Equals(IElement element)
        {
            if (element.GetType().IsAssignableFrom(typeof(ElementBase<TNativeElement>)))
            {
                return this.WebElement.IsEqualTo(((ElementBase<TNativeElement>)element).WebElement);
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

        public abstract IEnumerable<IElement> GetChildren();
        //{
        //    FindBy childrenLocator = new FindBy(IHow.Xpath, ".//*");
        //    IElementSearchConfiguration childrenSearchConfiguration =
        //        (Program.Container as StandardKernel).Get<IElementSearchConfiguration>
        //        (new Ninject.Parameters.Parameter("locator", childrenLocator, true));
        //    return childrenSearchConfiguration.FindAll();

        //}

        public void Click()
        {
            WebElement.Click();
        }

    }
}
