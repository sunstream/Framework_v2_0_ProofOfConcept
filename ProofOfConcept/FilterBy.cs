namespace ProofOfConcept
{

    public abstract class FilterBy
    {
        protected bool IncludeMatchingElements;

        protected FilterBy(bool includeMatchingElements)
        {
            IncludeMatchingElements = includeMatchingElements;
        }
        public abstract bool Check(IElement element);
    }

    public sealed class IsDisplayed : FilterBy
    {
        public IsDisplayed(bool includeMatchingElements = true) : base(includeMatchingElements)
        {
        }

        public override bool Check(IElement element)
        {
            return element.Displayed ^ IncludeMatchingElements;
        }
    }

    public sealed class HasAttribute : FilterBy
    {
        private readonly string _attributeName;
        private readonly string _attributeValue;
        public HasAttribute(string attributeName, string attributeValue, bool includeMatchingElements = true)
            : base(includeMatchingElements)
        {
            _attributeName = attributeName;
            _attributeValue = attributeValue;
        }
        public override bool Check(IElement element)
        {
            return (element.GetAttribute(_attributeName) == _attributeValue) ^ IncludeMatchingElements;
        }
    }

    //public class NoCaching : FilterBy
    //{

    //    public NoCaching() : base(true)
    //    {
    //    }
    //    public override bool Check(IElement element)
    //    {
    //        return true;
    //    }
    //}

    
}