using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder<TNativeElementType>
    {
        TNativeElementType GetNativeElement();

        IElement FindFirst();

        IList<IElement> FindAll();

        IList<TNativeElementType> Find(TNativeElementType container);

        IList<IElement> Wrap(IList<TNativeElementType> nativeElements);

        IElement Wrap(TNativeElementType nativeElement);

        IList<IElement> Filter(IList<IElement> elements);
    }
}