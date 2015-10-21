using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder<TNativeElementType>
    {
        IElementFinder<TNativeElementType> FindBy(FindBy locator);

        IElementFinder<TNativeElementType> FilterBy(FilterBy[] filters);

        IElementFinder<TNativeElementType> From(IElement parentElement);

        bool IsCachingAllowed { get; set; }

        TNativeElementType GetNativeElement();

        TNativeElementType GetParentIfExists();

        IElement FindFirst();

        IList<IElement> FindAll();

        IList<TNativeElementType> Find(TNativeElementType container);

        IElement Wrap(TNativeElementType nativeElement);

        IList<IElement> Wrap(IList<TNativeElementType> nativeElements);

        IList<IElement> Filter(IList<IElement> elements);
    }

    //public interface ISearchConfigurationBuilder<TNativeElementType>
    //{
    //    ISearchConfigurationBuilder<TNativeElementType> FindBy(FindBy locator);

    //    ISearchConfigurationBuilder<TNativeElementType> FilterBy(FilterBy[] filter);

    //    ISearchConfigurationBuilder<TNativeElementType> From(IElement parentElement);

    //    IElementFinder<TNativeElementType> Build();
    //}

    //public class SearchConfigurationBuilder : ISearchConfigurationBuilder<T> where
    //{
        
    //}
}