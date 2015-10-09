using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder
    {
        IElement FindElement(ILocator locator, params ISearchFilter[] filters);
        
        IList<IElement> FindElements(ILocator locator, params ISearchFilter[] filters);
        
    }
}