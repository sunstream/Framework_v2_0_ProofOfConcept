using System.Collections.Generic;
using System.Linq;
using Ninject;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept
{
    public class ElementBase : IElement, IElementBehavior
    {
        private IElementSearchConfiguration _searchConfiguration;
        public IElementSearchConfiguration SearchConfiguration
        {
            get { return _searchConfiguration; }
            set
            {
                _searchConfiguration = value;
                ElementHandler.SearchConfiguration = value;
            }
        }

        private INativeElementHandler _elementHandler;
        protected INativeElementHandler ElementHandler
        {
            get {
                return _elementHandler ??
                       (_elementHandler = DependencyManager.Kernel.Get<INativeElementHandler>(Tool.ToString()));
            }
        } 
        private ToolFamily? _tool;
        public ToolFamily Tool
        {
            get
            {
                if (_tool == null)
                {
                    _tool = DependencyManager.Tool;
                }
                return _tool.GetValueOrDefault();
            }
            set { _tool = value; }
        }

        public dynamic NativeElement
        {
            get { return ElementHandler.NativeElement; }
            set { ElementHandler.NativeElement = value; }
        }

        public bool Displayed
        {
            get { return ElementHandler.Displayed; }
        }

        public bool Exists
        {
            get { return ElementHandler.Exists; }
        }

        public bool Equals(IElement element)
        {
            return ElementHandler.Equals(element);
        }

        public string GetAttribute(string attributeName)
        {
            return ElementHandler.GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            return ElementHandler.GetChildren();
        }

        public bool MatchesFilter(FilterBy filterBy)
        {
            return filterBy.Check(this);
        }

        public bool MatchesAllFilters(params FilterBy[] filtersBy)
        {
            return filtersBy.All(searchFilter => searchFilter.Check(this));
        }

        public TElementBehavior AddBehavior<TElementBehavior>() where TElementBehavior : IElementBehavior
        {
            TElementBehavior behavior = (TElementBehavior)DependencyManager.Kernel.Get(typeof(TElementBehavior));
            behavior.Element = this;
            return behavior;
        }

        public void Click()
        {
            ElementHandler.Click();
        }

        public ElementBase Element
        {
            get { return this; }
            set { }
        }
    }

    public static class IElementExtensions
    {
        public static void InitFromBase(this IElement element, IElement baseElement)
        {
            element.SearchConfiguration = baseElement.SearchConfiguration;
        }
    }
}