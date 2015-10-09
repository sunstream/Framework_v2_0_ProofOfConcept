using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElement : IElementFinder
    {
        bool Displayed { get; }

        bool Exists { get; }

        bool Equals(IElement element);

        bool MatchesFilter(SearchFilter filter);

        bool MatchesAllFilters(params SearchFilter[] filters);

        string GetAttribute(string attributeName);

        IEnumerable<IElement> GetChildren();

        

    }
}
