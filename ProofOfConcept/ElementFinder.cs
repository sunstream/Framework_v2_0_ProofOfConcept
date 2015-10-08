using System.Collections.Generic;

namespace ProofOfConcept
{
    public abstract class ElementFinder : IElementFinder
    {
        public abstract IElement FindElement(ILocator locator, params SearchFilter[] filters);
        public abstract IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters);
    }
}