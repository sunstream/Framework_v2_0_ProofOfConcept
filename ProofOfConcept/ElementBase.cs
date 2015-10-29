using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace ProofOfConcept
{
    public class ElementBase : IElement
    {
        public IElementSearchConfiguration SearchConfiguration { get; set; }

        private INativeElementHandler ElementState
        {
            get
            {
                INativeElementHandler handler = DependencyManager.Kernel.Get<INativeElementHandler>(_tool.ToString());
                handler.SearchConfiguration = SearchConfiguration;
                return handler;
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
            get { return ElementState.NativeElement; }
            set { NativeElement = value; }
        }

        public bool Displayed
        {
            get { return ElementState.Displayed; }
        }

        public bool Exists
        {
            get { return ElementState.Exists; }
        }

        public bool Equals(IElement element)
        {
            return ElementState.Equals(element);
        }

        public string GetAttribute(string attributeName)
        {
            return ElementState.GetAttribute(attributeName);
        }

        public IEnumerable<IElement> GetChildren()
        {
            return ElementState.GetChildren();
        }

        public bool MatchesFilter(FilterBy filterBy)
        {
            return filterBy.Check(this);
        }

        public bool MatchesAllFilters(params FilterBy[] filtersBy)
        {
            return filtersBy.All(searchFilter => searchFilter.Check(this));
        }

        public void Click()
        {
            ElementState.Click();
        }
    }
}
