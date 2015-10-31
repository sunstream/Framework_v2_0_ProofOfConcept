using System;

namespace ProofOfConcept.Configuration
{
    public interface ITimeoutSettings
    {
        TimeSpan ElementTimeout { get; set; }
        TimeSpan PageTimeout { get; set; }
    }
}