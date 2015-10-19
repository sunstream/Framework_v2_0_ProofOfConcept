using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumElementFinder : IElementFinder<IWebElement>, IDescribable
    {
        public FindBy FindBy { get; set; }
        public IElement ParentElement { get; set; }
        public FilterBy[] Filters { get; set; }

        private bool? _isCachingAllowed;
        public bool IsCachingAllowed
        {
            get
            {
                if (_isCachingAllowed == null)
                {
                    foreach (var filterBy in Filters)
                    {
                        if (filterBy is NoCaching)
                        {
                            _isCachingAllowed = false;
                            break;
                        }
                    }
                }
                return _isCachingAllowed.GetValueOrDefault(true);
            }
            set { _isCachingAllowed = value; }
        }

        public IWebDriver Driver;

        //TODO: get default time span from configuration file
        //TODO: use default value from ElementService
        private TimeSpan? _timeout;
        
        private readonly TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(5);
        private readonly ILocatorTransformer<By> _locatorTransformer = new SeleniumLocatorTransformer();

        public SeleniumElementFinder(FindBy findBy, IElement parentElement = null, params FilterBy[] filters)
        {
            FindBy = findBy;
            ParentElement = parentElement;
            Filters = filters;
        }

        public string Describe()
        {
            By nativeLocator = _locatorTransformer.GetNativeLocator(FindBy);
            string description = string.Format("Looking up elements by the following parameters:{0}1) locator = '{1}{0}; 2) filters: {2}", 
                Environment.NewLine, 
                nativeLocator, 
                String.Join(string.Format("{0}\\t", Environment.NewLine), Filters.Select(s => s.Describe())));
            return description;
        }

        public IWebElement GetNativeElement()
        {
            IWebElement result = null;
            IElement firstElement = FindFirst();
            if (firstElement != null && firstElement.Exists)
            {
                result = ((SeleniumElement)firstElement).WebElement;
            }
            return result;
        }

        public IElement FindFirst()
        {
            return FindAll().FirstOrDefault();
        }

        public IList<IElement> FindAll()
        {
            SetTimeout();
            IList<IElement> elements;
            IWebElement container = GetParentIfExists();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                var nativeElements = Find(container);
                elements = Wrap(nativeElements);
                elements = Filter(elements);
            } while (elements.Count == 0 || stopwatch.Elapsed < _timeout);
            
            return elements;
        }

        private void SetTimeout()
        {
            _timeout = null;
            foreach (var filterBy in Filters)
            {
                if (filterBy is WithTimeout)
                {
                    _timeout = ((WithTimeout)filterBy).Timeout;
                }
            }
            if (_timeout == null)
            {
                _timeout = DefaultTimeSpan;
            }
        }

        public IWebElement GetParentIfExists()
        {
            IWebElement container = null;
            if (ParentElement != null)
            {
                if (ParentElement.Exists)
                {
                    container = ((SeleniumElement)ParentElement).WebElement;
                }
                else
                {
                    throw new ArgumentException("Parent element does not match any real element on the page.");
                }
            }
            return container;
        }

        public IList<IWebElement> Find(IWebElement container = null)
        {
            By locator = _locatorTransformer.GetNativeLocator(FindBy);
            IList<IWebElement> nativeElements = container == null ? Driver.FindElements(locator) : container.FindElements(locator);
            return nativeElements;
        }

        public IList<IElement> Wrap(IList<IWebElement> nativeElements)
        {
            IList<IElement> elements = new List<IElement>();
            foreach (var nativeElement in nativeElements)
            {
                elements.Add(Wrap(nativeElement));
            }
            return elements;
        }

        public IElement Wrap(IWebElement nativeElement)
        {
            SeleniumElement wrappedElement = new SeleniumElement(this);
            wrappedElement.WebElement = nativeElement;
            return wrappedElement;
        }

        public IList<IElement> Filter(IList<IElement> elements)
        {
            IList<IElement> filteredElements = new List<IElement>();
            foreach (var element in elements)
            {
                if (element.MatchesAllFilters(Filters))
                {
                    filteredElements.Add(element);
                }
            }
            return filteredElements;
        }

    }
}