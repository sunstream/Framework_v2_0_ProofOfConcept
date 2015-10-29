using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace ProofOfConcept
{
    public interface IElement : INativeElementHandler
    {
        bool MatchesFilter(FilterBy filterBy);

        bool MatchesAllFilters(params FilterBy[] filtersBy);

    }

    



    
}
