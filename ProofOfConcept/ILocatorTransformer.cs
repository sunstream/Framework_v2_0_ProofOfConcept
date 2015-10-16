namespace ProofOfConcept
{
    public interface ILocatorTransformer<out T>
    {
        T GetNativeLocator(ILocator locator);
    }
}