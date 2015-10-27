using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementSearchConfiguration<TNativeElement>
    {
        IElementSearchConfiguration<TNativeElement> FindBy(FindBy locator);

        IElementSearchConfiguration<TNativeElement> FilterBy(FilterBy[] filters);

        IElementSearchConfiguration<TNativeElement> From(IElement parentElement);
        
        IList<IElement> Filter(IList<IElement> elements);
        
        bool IsCachingAllowed { get; set; }

        TNativeElement GetNativeElement();

        TNativeElement GetParentIfExists();

        IElement FindFirst();

        IList<IElement> FindAll();

        IList<TNativeElement> Find(TNativeElement container);

        IElement Wrap(TNativeElement nativeElement);

        IList<IElement> Wrap(IList<TNativeElement> nativeElements);

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