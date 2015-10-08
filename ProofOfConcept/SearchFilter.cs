using System.Linq;

namespace ProofOfConcept
{

    public abstract class SearchFilter
    {
        public abstract bool Check(IElement element);
    }

    public class IsDisplayed : SearchFilter
    {
        public override bool Check(IElement element)
        {
            return element.Displayed;
        }
    }

    public class HasAttribute : SearchFilter
    {
        private readonly string _attributeName;
        private readonly string _attributeValue;
        public HasAttribute(string attributeName, string attributeValue)
        {
            _attributeName = attributeName;
            _attributeValue = attributeValue;
        }
        public override bool Check(IElement element)
        {
            return element.GetAttribute(_attributeName) == _attributeValue;
        }
    }
    
}