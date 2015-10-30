using System.Collections.Generic;
using System.Linq;
using Ninject;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept
{
    public interface IElement : INativeElementHandler
    {
        bool MatchesFilter(FilterBy filterBy);

        bool MatchesAllFilters(params FilterBy[] filtersBy);

        TElementBehavior AddBehavior<TElementBehavior>() where TElementBehavior : IElementBehavior;

    }

    



    
}
