using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementSearchConfiguration<TNativeElementType>
    {
        IElementSearchConfiguration<TNativeElementType> FindBy(FindBy locator);

        IElementSearchConfiguration<TNativeElementType> FilterBy(FilterBy[] filters);

        IElementSearchConfiguration<TNativeElementType> From(IElement parentElement);

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
}