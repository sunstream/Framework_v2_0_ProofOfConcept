using System.Security.Cryptography.X509Certificates;

namespace ProofOfConcept
{
    public interface IDriverDecorator
    {
        void NavigateTo(string url);

        string GetCurrentUrl();
    }
}
