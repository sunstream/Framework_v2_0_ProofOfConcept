using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    sealed class SeleniumNativeElementHandler : INativeElementHandler
    {
        public IElementSearchConfiguration SearchConfiguration { get; set; }
        private IWebElement _webElement;

        public dynamic NativeElement
        {
            get
            {
                if (_webElement == null || !Exists || IsNewLookupAlwaysRequired)
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

        public bool Exists
        {
            get
            {
                bool exists = false;
                if (_webElement != null)
                {
                    try
                    {
                        _webElement.GetAttribute("innerHTML");
                        exists = true;
                    }
                    catch (StaleElementReferenceException) { }
                }
                return exists;
            }
        }

        private bool IsNewLookupAlwaysRequired
        {
            get
            {
                return SearchConfiguration != null && !SearchConfiguration.IsCachingAllowed;
            }
        }

        public bool Displayed
        {
            get { return ((IWebElement)NativeElement).Displayed; }
        }
        
        public bool Equals(IElement element)
        {
            return ((IWebElement)NativeElement).IsEqualTo((IWebElement)element.NativeElement);
        }

        public string GetAttribute(string attributeName)
        {
            return ((IWebElement) NativeElement).GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            FindBy childrenLocator = new FindBy(How.Xpath, "./*");
            IElementSearchConfiguration childrenSearchConfiguration = new SeleniumElementFinder(childrenLocator, new SeleniumLocatorTransformer());
            return childrenSearchConfiguration.FindAll();
        }

        public void Click()
        {
            ((IWebElement) NativeElement).Click();
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
