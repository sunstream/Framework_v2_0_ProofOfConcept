using System;

namespace ProofOfConcept
{
    public interface ILocatorTransformer<out TNativeLocator> where TNativeLocator : class
    {
        TNativeLocator GetNativeLocator(FindBy findBy);
        
    }
}