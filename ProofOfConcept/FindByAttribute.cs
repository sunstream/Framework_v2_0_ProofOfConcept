using System;

namespace ProofOfConcept
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class FindByAttribute : Attribute
    {
        public FindBy FindBy { get; private set; }

        public FindByAttribute(string how, String value)
        {
            FindBy = new FindBy(how, value);
        }
        
    }
}