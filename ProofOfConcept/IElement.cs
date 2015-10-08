using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElement : IElementFinder
    {
        bool IsVisible();

        string GetAttribute(string attributeName);

        IEnumerable<IElement> GetChildren();

    }
}
