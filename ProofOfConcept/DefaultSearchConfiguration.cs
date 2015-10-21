using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProofOfConcept
{
    public abstract class DefaultSearchConfiguration<TNativeElementType, TNativeLocatorType> : IElementFinder<TNativeElementType>, IDescribable
    {
        protected FindBy _findBy;
        protected FilterBy[] _filters;
        protected IElement _parentElement;
        protected bool? _isCachingAllowed;

        protected ILocatorTransformer<TNativeLocatorType> LocatorTransformer;

        //TODO: get default time span from configuration file
        //TODO: use default value from ElementService
        protected readonly TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(5);
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
                        _timeout = DefaultTimeSpan;
                    }
                }
                return _timeout;
            }
            set { _timeout = value; }
        }

        public IElementFinder<TNativeElementType> FindBy(FindBy locator)
        {
            _findBy = locator;
            return this;
        }
        public IElementFinder<TNativeElementType> FilterBy(FilterBy[] filters)
        {
            _filters = filters;
            return this;
        }
        public IElementFinder<TNativeElementType> From(IElement parentElement)
        {
            _parentElement = parentElement;
            return this;
        }

        public DefaultSearchConfiguration(FindBy locator)
        {
            _findBy = locator;
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

        public abstract TNativeElementType GetNativeElement();
        public abstract TNativeElementType GetParentIfExists();
        public abstract IList<TNativeElementType> Find(TNativeElementType container);
        public abstract IElement Wrap(TNativeElementType nativeElement);

        public IElement FindFirst()
        {
            return FindAll().FirstOrDefault();
        }

        public IList<IElement> FindAll()
        {
            SetTimeout();
            IList<IElement> elements;
            TNativeElementType container = GetParentIfExists();

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

        private void SetTimeout()
        {
            Timeout = null;
            foreach (var filterBy in _filters)
            {
                if (filterBy is WithTimeout)
                {
                    Timeout = ((WithTimeout)filterBy).Timeout;
                }
            }
            if (Timeout == null)
            {
                Timeout = DefaultTimeSpan;
            }
        }
        
        public IList<IElement> Wrap(IList<TNativeElementType> nativeElements)
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
