using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface IElement
    {
        bool Displayed { get; }

        bool Exists { get; }

        bool Equals(IElement element);

        bool MatchesFilter(FilterBy filterBy);

        bool MatchesAllFilters(params FilterBy[] filtersBy);

        string GetAttribute(string attributeName);

        IEnumerable<IElement> GetChildren();

        void Click();

    }
}
