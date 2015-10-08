using System;
using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElementFinder
    {
        IElement FindElement(ILocator locator, params SearchFilter[] filters);
        IElement FindElement(ILocators locators, params SearchFilter[] filters);

        IList<IElement> FindElements(ILocator locator, params SearchFilter[] filters);
        IList<IElement> FindElements(ILocators locators, params SearchFilter[] filters);
    }
}