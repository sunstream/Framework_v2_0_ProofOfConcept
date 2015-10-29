using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumElementFinder : ElementFinderBase<IWebElement, By>
    {
        public IWebDriver Driver;

        public SeleniumElementFinder(FindBy locator, ILocatorTransformer<By> locatorTransformer) : base(locator, locatorTransformer)
        {
        }

        public override IWebElement GetNativeElement()
        {
            IWebElement result = null;
            IElement firstElement = FindFirst();
            if (firstElement != null && firstElement.Exists)
            {
                //result = ((SeleniumElement) firstElement).WebElement;
                result = ((IWebElement)firstElement.NativeElement);
            }
            return result;
        }

        public override IWebElement GetParentIfExists()
        {
            IWebElement container = null;
            if (_parentElement != null)
            {
                container = ((IWebElement)_parentElement.NativeElement);
                if (container == null)
                {
                    throw new ArgumentException("Parent element does not match any real element on the page.");
                }
            }
            return container;
        }

        public override IList<IWebElement> Find(IWebElement container)
        {
            By locator = LocatorTransformer.GetNativeLocator(_findBy);
            IList<OpenQA.Selenium.IWebElement> nativeElements = container == null
                ? Driver.FindElements(locator)
                : container.FindElements(locator);
            return nativeElements;
        }

        public override IElement Wrap(IWebElement nativeElement)
        {
            ElementBase wrappedElement = new ElementBase {NativeElement = nativeElement};
            return wrappedElement;
        }
    }
}