using System;

namespace ProofOfConcept
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class Locator
    {
        public Locator(LocatorType how, String value)
        {
            this.How = how;
            this.Value = value;
        }
        public LocatorType How { get; private set; }
        public string Value { get; private set; }
    }
}