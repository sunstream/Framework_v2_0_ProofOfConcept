using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder
    {
        IElement FindElement(FindBy locator, params FilterBy[] filters);

        IList<IElement> FindElements(FindBy locator, params FilterBy[] filters);
        
    }
}