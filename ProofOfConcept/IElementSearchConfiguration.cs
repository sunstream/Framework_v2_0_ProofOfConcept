using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementSearchConfiguration
    {
        IElementSearchConfiguration FindBy(FindBy locator);

        IElementSearchConfiguration FilterBy(FilterBy[] filters);

        IElementSearchConfiguration From(IElement parentElement);
        
        bool IsCachingAllowed { get; set; }

        IElement FindFirst();

        IList<IElement> FindAll();

    }

    public interface IElementFinder<TNativeElement>
    {
        TNativeElement GetNativeElement();

        TNativeElement GetParentIfExists();

        IList<TNativeElement> Find(TNativeElement container);

        IElement Wrap(TNativeElement nativeElement);

        IList<IElement> Wrap(IList<TNativeElement> nativeElements);

        IList<IElement> Filter(IList<IElement> elements);
    }
    
    //public interface IElementSearchConfiguration<TNativeElementType> where TNativeElementType : class
    //{
    //    IElementSearchConfiguration<TNativeElementType> FindBy(FindBy locator);

    //    IElementSearchConfiguration<TNativeElementType> FilterBy(FilterBy[] filters);

    //    IElementSearchConfiguration<TNativeElementType> From(IElement parentElement);

    //    bool IsCachingAllowed { get; set; }

    //    TNativeElementType GetNativeElement();

    //    TNativeElementType GetParentIfExists();

    //    IElement FindFirst();

    //    IList<IElement> FindAll();

    //    IList<TNativeElementType> Find(TNativeElementType container);

    //    IElement Wrap(TNativeElementType nativeElement);

    //    IList<IElement> Wrap(IList<TNativeElementType> nativeElements);

    //    IList<IElement> Filter(IList<IElement> elements);
    //}
}