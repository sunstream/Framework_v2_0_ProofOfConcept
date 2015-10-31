using System;

namespace ProofOfConcept.Configuration
{
    public class DependencyConfigurationException : Exception
    {
        public DependencyConfigurationException(string message, Exception innerException)
            : base(message, innerException) { }
        public DependencyConfigurationException(string message)
            : base(message) { }
    }
}