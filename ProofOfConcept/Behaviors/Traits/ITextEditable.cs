namespace ProofOfConcept.Behaviors.Traits
{
    public interface ITextEditable
    {
        void SetText(string textValue);

        void AppendText(string textValue);

        void Clear();
    }
}