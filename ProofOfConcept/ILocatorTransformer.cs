using System;

namespace ProofOfConcept
{
    public interface ILocatorTransformer<TNativeLocator> where TNativeLocator : class
    {
        TNativeLocator GetNativeLocator(FindBy findBy);
        
    }
}