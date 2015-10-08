using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ProofOfConcept
{
    public interface IElement : IElementFinder
    {
        bool Displayed { get; }

        bool MatchesFilter(SearchFilter filter);

        bool MatchesAllFilters(params SearchFilter[] filters);

        string GetAttribute(string attributeName);

        IEnumerable<IElement> GetChildren();

        

    }
}
