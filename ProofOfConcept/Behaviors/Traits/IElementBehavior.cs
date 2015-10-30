using System.Security.Cryptography.X509Certificates;

namespace ProofOfConcept.Behaviors
{
    public interface IElementBehavior
    {
        ElementBase Element { get; set; }
    }
}