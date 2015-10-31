using System;

namespace ProofOfConcept
{
    public abstract class FilterBy : IDescribable
    {
        protected bool IncludeMatchingElements;

        protected FilterBy()
        {
            IncludeMatchingElements = true;
        }

        protected FilterBy(bool includeMatchingElements)
        {
            IncludeMatchingElements = includeMatchingElements;
        }

        public bool Check(IElement element)
        {
            return !(CalculateCondition(element) ^ IncludeMatchingElements);
        }

        public abstract bool CalculateCondition(IElement element);

        public string DescribeIncludeRule()
        {
            return (IncludeMatchingElements ? "Include" : "Exclude");
        }

        public abstract string Describe();
    }

    public sealed class IsDisplayed : FilterBy
    {
        public IsDisplayed(bool includeMatchingElements = true) : base(includeMatchingElements)
        {
        }

        public override bool CalculateCondition(IElement element)
        {
            return !(element.Displayed ^ IncludeMatchingElements);
        }

        public override string Describe()
        {
            return string.Format("{0} only visible", DescribeIncludeRule());
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
        public override bool CalculateCondition(IElement element)
        {
            return !((element.GetAttribute(_attributeName) == _attributeValue) ^ IncludeMatchingElements);
        }
        public override string Describe()
        {
            return string.Format("{0} elements with attribute {1} = {2}", DescribeIncludeRule(), _attributeName, _attributeValue);
        }
    }

    public sealed class WithTimeout : FilterBy
    {
        public TimeSpan Timeout;
        public WithTimeout(TimeSpan timeout)
        {
            Timeout = timeout;
        }
        public override bool CalculateCondition(IElement element)
        {
            return true;
        }
        public override string Describe()
        {
            return string.Format("Element should be found within {0} seconds, or considered absent.", Timeout.TotalSeconds);
        }
        
    }

    public class NoCaching : FilterBy
    {
        public override bool CalculateCondition(IElement element)
        {
            return true;
        }
        public override string Describe()
        {
            return string.Format("Element should always be looked up from scratch (no caching allowed).");
        }
    }

    
}