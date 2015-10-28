using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProofOfConcept
{
    public abstract class ElementFinderBase<TNativeElement, TNativeLocator> : 
        IElementSearchConfiguration, 
        IElementFinder<TNativeElement>, 
        IDescribable 
        where TNativeElement : class 
        where TNativeLocator : class
    {
        protected ElementFinderBase(FindBy locator, ILocatorTransformer<TNativeLocator> locatorTransformer)
        {
            _findBy = locator;
            LocatorTransformer = locatorTransformer;
        }
        
        protected FindBy _findBy;
        protected FilterBy[] _filters;
        protected IElement _parentElement;
        protected bool? _isCachingAllowed;

        protected ILocatorTransformer<TNativeLocator> LocatorTransformer;

        private TimeSpan? _timeout;
        public TimeSpan? Timeout
        {
            get
            {
                if (_timeout == null)
                {
                    foreach (WithTimeout filterBy in _filters.OfType<WithTimeout>())
                    {
                        _timeout = (filterBy).Timeout;
                        break;
                    }
                    if (_timeout == null)
                    {
                        _timeout = Settings.ElementSearchConfigurationSettings.Timeout;
                    }
                }
                return _timeout;
            }
            set { _timeout = value; }
        }

        public IElementSearchConfiguration<TNativeElement> FindBy(FindBy locator)
        {
            _findBy = locator;
            return this;
        }
        public IElementSearchConfiguration<TNativeElement> FilterBy(FilterBy[] filters)
        {
            _filters = filters;
            return this;
        }
        public IElementSearchConfiguration<TNativeElement> From(IElement parentElement)
        {
            _parentElement = parentElement;
            return this;
        }

        public bool IsCachingAllowed
        {
            get
            {
                if (_isCachingAllowed == null)
                {
                    foreach (var filterBy in _filters)
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

        public abstract TNativeElement GetNativeElement();
        public abstract TNativeElement GetParentIfExists();
        public abstract IList<TNativeElement> Find(TNativeElement container);
        public abstract IElement Wrap(TNativeElement nativeElement);

        public IElement FindFirst()
        {
            return FindAll().FirstOrDefault();
        }

        public IList<IElement> FindAll()
        {
            IList<IElement> elements;
            dynamic container = GetParentIfExists();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                var nativeElements = Find(container);
                elements = Wrap(nativeElements);
                elements = Filter(elements);
            } while (elements.Count == 0 || stopwatch.Elapsed < Timeout);

            return elements;
        }

        public IList<IElement> Wrap(IList<TNativeElement> nativeElements)
        {
            return nativeElements.Select(Wrap).ToList();
        }

        public IList<IElement> Filter(IList<IElement> elements)
        {
            return elements.Where(element => element.MatchesAllFilters(_filters)).ToList();
        }

        public string Describe()
        {
            string locatorDescription = LocatorTransformer.GetNativeLocator(_findBy).ToString();
            string description = string.Format("Looking up elements by the following parameters:{0}1) locator = '{1}{0}; 2) filters: {2}",
                Environment.NewLine,
                locatorDescription,
                String.Join(string.Format("{0}\\t", Environment.NewLine), _filters.Select(s => s.Describe())));
            return description;
        }
    }
}
