namespace ProofOfConcept
{
    public interface IPageFactory
    {
        T GetPage<T>(IElement containerElement = null) where T : IContainer, new();
    }
}
