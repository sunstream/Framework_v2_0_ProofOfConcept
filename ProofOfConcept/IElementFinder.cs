using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder
    {
        IElement FindElement(FindBy findBy, params FilterBy[] filters);

        IList<IElement> FindElements(FindBy findBy, params FilterBy[] filters);
        
    }
}