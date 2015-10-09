using System;

namespace ProofOfConcept
{
    //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    //public class FilterBy : Attribute
    //{
    //    private SearchFilter _filter;
    //    public FilterBy(SearchFilter filter)
    //    {
    //        _filter = filter;
    //    }
    //}
    //
    //[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    //public class FilterByVisibility : Attribute
    //{
    //    public SearchFilter Filter { get; private set; }
    //    public FilterByVisibility(bool includeMatchingElements = true)
    //    {
    //        Filter = new IsDisplayed(includeMatchingElements);
    //    }
    //}
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FilterBy : Attribute
    {
        public SearchFilter filter;
        //public FilterType FilterType { get; set; }
        //public AttributeName 
        //public FilterBy()
        //{

        //}
    }

    public enum FilterType
    {
        IsVisible,
        HasAttribute
    }
}