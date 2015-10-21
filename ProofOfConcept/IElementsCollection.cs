using System.Collections;
using System.Collections.Generic;

namespace ProofOfConcept
{

    public interface IElementsCollection<T> : IEnumerable<T>, IEnumerable where T : IElement
    {

        T this[int index] { get; }

        IElementSearchConfiguration SearchConfiguration { get; set; }
        
        void Refresh();

    }

}