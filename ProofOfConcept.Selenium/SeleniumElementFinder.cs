using System;
using System.Collections.Generic;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;

namespace ProofOfConcept.Selenium
{
    public class SeleniumElementFinder : ElementFinderBase<IWebElement, By>
    {
        private IWebDriver Driver
        {
            get
            {
                return DependencyManager.Kernel.Get<NavigationService>().GetDriver<IWebDriver>();
            }
        }

        public SeleniumElementFinder()
        {
            LocatorTransformer = new SeleniumLocatorTransformer();
        }

        public override IWebElement GetNativeElement()
        {
            IWebElement result = null;
            IElement firstElement = FindFirst();
            if (firstElement != null && firstElement.Exists)
            {
                result = ((IWebElement)firstElement.NativeElement);
            }
            return result;
        }

        public override IWebElement GetParentIfExists()
        {
            IWebElement container = null;
            if (ContainerElement != null)
            {
                container = ((IWebElement)ContainerElement.NativeElement);
                if (container == null)
                {
                    throw new ArgumentException("Parent element does not match any real element on the page.");
                }
            }
            return container;
        }

        public override IList<IWebElement> Find(IWebElement container)
        {
            By locator = LocatorTransformer.GetNativeLocator(Locator);
            IList<OpenQA.Selenium.IWebElement> nativeElements = container == null
                ? Driver.FindElements(locator)
                : container.FindElements(locator);
            return nativeElements;
        }

        public override IElement Wrap(IWebElement nativeElement)
        {
            ElementBase wrappedElement = new ElementBase {NativeElement = nativeElement, SearchConfiguration = this};
            return wrappedElement;
        }
    }
}