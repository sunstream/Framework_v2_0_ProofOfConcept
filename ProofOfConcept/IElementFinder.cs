using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder<TNativeElementType>
    {
        bool IsCachingAllowed { get; set; }

        IElement FindFirst();

        IList<IElement> FindAll();

        TNativeElementType GetNativeElement();

        TNativeElementType GetParentIfExists();

        IList<TNativeElementType> Find(TNativeElementType container);

        IList<IElement> Wrap(IList<TNativeElementType> nativeElements);

        IElement Wrap(TNativeElementType nativeElement);

        IList<IElement> Filter(IList<IElement> elements);
    }
}