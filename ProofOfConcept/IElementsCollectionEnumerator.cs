using System.Collections.Generic;

namespace ProofOfConcept
{

    public interface IElementsCollectionEnumerator<T> : IEnumerator<T> where T : IElement { }

}