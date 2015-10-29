using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace ProofOfConcept
{
    public class ElementBase : IElement
    {
        public IElementSearchConfiguration SearchConfiguration { get; set; }

        private INativeElementHandler ElementHandler
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
        
        public dynamic NativeElement { get; set; }

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

        public void Click()
        {
            ElementHandler.Click();
        }
    }
}
