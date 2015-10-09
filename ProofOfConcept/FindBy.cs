using System;

namespace ProofOfConcept
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FindBy
    {
        public FindBy(LocatorType how, String value)
        {
            this.How = how;
            this.Value = value;
        }
        public LocatorType How { get; private set; }
        public string Value { get; private set; }
    }
}