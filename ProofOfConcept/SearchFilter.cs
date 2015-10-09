namespace ProofOfConcept
{

    //public static class FilterType
    //{
    //    //public static SearchFilter ProperFilter { get; set; }
    //    public static IsDisplayed IsDisplayed(bool includeMatchingElements = true)
    //    {
    //        return new IsDisplayed(includeMatchingElements);
    //    }

    //    public static HasAttribute HasAttribute(string attributeName, string attributeValue, bool includeMatchingElements = true)
    //    {
    //        return new HasAttribute(includeMatchingElements, attributeName, attributeValue);
    //    }
        
    //}

    public abstract class SearchFilter
    {
        protected bool IncludeMatchingElements;

        protected SearchFilter(bool includeMatchingElements)
        {
            IncludeMatchingElements = includeMatchingElements;
        }
        public abstract bool Check(IElement element);
    }

    public class IsDisplayed : SearchFilter
    {
        public IsDisplayed(bool includeMatchingElements) : base(includeMatchingElements)
        {
        }

        public override bool Check(IElement element)
        {
            return element.Displayed ^ IncludeMatchingElements;
        }
    }

    public class HasAttribute : SearchFilter
    {
        private readonly string _attributeName;
        private readonly string _attributeValue;
        public HasAttribute(bool includeMatchingElements, string attributeName, string attributeValue) : base(includeMatchingElements)
        {
            _attributeName = attributeName;
            _attributeValue = attributeValue;
        }
        public override bool Check(IElement element)
        {
            return (element.GetAttribute(_attributeName) == _attributeValue) ^ IncludeMatchingElements;
        }
    }
    
}