using System;

namespace ProofOfConcept
{
    public class FindBy
    {
        public string How { get; set; }
        public string Value { get; set; }

        public FindBy(string how, String value)
        {
            this.How = how;
            this.Value = value;
        }
    }
}
