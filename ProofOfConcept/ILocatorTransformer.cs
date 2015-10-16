namespace ProofOfConcept
{
    public interface ILocatorTransformer<T>
    {
        T GetNativeLocator(IFindBy findBy);
    }
}