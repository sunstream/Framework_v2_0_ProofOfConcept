using System.Security.Cryptography.X509Certificates;

namespace ProofOfConcept
{
    public interface IDriverDecorator
    {
        bool DriverCreated { get; set; }

        bool ApplicationStarted { get; set; }

        void NavigateTo(string url);

        string GetCurrentUrl();

        TDriverType GetDriver<TDriverType>() where TDriverType : class;

        void Stop();
    }
}
