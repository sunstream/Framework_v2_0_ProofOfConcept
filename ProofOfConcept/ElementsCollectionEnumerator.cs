using System.Collections;

namespace ProofOfConcept
{

    public class ElementsCollectionEnumerator<T> : IElementsCollectionEnumerator<T> where T : IElement
    {

        public ElementsCollectionEnumerator(T[] elements)
        {
            _elements = elements;
        }

        private T[] _elements;
        private int _position = -1;

        public T Current
        {
            get
            {
                return _elements[_position];
            }
        }

        public void Dispose() { }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            return (++_position < _elements.Length);
        }

        public void Reset()
        {
            _position = -1;
        }
    }

}