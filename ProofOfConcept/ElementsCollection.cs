using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProofOfConcept;

namespace ProofOfConcept
{

    public class ElementsCollection<T> : IElementsCollection<T> where T : IElement
    {

        public ElementsCollection() { }

        public ElementsCollection(IElementSearchConfiguration searchConfiguration)
        {
            SearchConfiguration = searchConfiguration;
        }

        public ElementsCollection(IList<IElement> elements)
        {
            _elements = new T[elements.Count];
            SearchConfiguration = (IElementSearchConfiguration)elements.FirstOrDefault().SearchConfiguration.Clone();
            for (var i = 0; i < elements.Count; i++)
            {
                _elements[i] = Wrapper.WrapElement<T>(elements[i]);
                _elements[i].SearchConfiguration.Index = i;
                //_elements[i] = Cast(elements[i]);
                //_elements[i].SearchConfiguration = (IElementSearchConfiguration)SearchConfiguration.Clone();
                //_elements[i].SearchConfiguration.Index = i;
            }
        }

        private T[] _elements;
        private T[] Elements
        {
            get
            {
                if (_elements == null)
                {
                    Refresh();
                }
                return _elements;
            }
        }

        public IElementSearchConfiguration SearchConfiguration { get; set; }

        public T this[int index]
        {
            get { return Elements[index]; }
        }

        public void Refresh()
        {
            _elements = SearchConfiguration.FindAll().Select(x => Cast(x)).ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ElementsCollectionEnumerator<T>(Elements);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private T Cast(IElement element)
        {
            if (element is T)
            {
                return (T)element;
            }
            return default(T);
        }

    }

    public static class Wrapper {
        public static T WrapElement<T>(this IElement element) where T: IElement {
            var result = (T)System.Activator.CreateInstance(typeof(T));
            result.SearchConfiguration = (IElementSearchConfiguration)element.SearchConfiguration.Clone();
            return result;
        }
    }

    public static class IElementSearchConfigurationExtension {
        public static IList<T> FindAll<T>(this IElementSearchConfiguration configuration) {
            throw new System.NotImplementedException();
        }
    }

}