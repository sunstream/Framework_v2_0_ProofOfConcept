using System;

namespace ProofOfConcept
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class FilterByAttribute : Attribute
    {
        public FilterBy FilterBy { get; set; }
    }

    public sealed class IsDisplayedAttribute : FilterByAttribute
    {
        public IsDisplayedAttribute(bool includeMatchingElements = true)
        {
            FilterBy = new IsDisplayed(includeMatchingElements);
        }
    }

    public sealed class HasAttributeAttribute : FilterByAttribute
    {
        public HasAttributeAttribute(string attributeName, string attributeValue, bool includeMatchingElements = true)
        {
            FilterBy = new HasAttribute(attributeName, attributeValue, includeMatchingElements);
        }
    }

    
}