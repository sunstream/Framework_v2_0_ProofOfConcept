using System;

namespace ProofOfConcept
{
    public interface ILocatorTransformer<TNativeLocatorType>
    {
        TNativeLocatorType GetNativeLocator(FindBy findBy);
        
    }
}