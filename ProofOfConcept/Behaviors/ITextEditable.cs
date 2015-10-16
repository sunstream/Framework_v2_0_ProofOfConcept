namespace ProofOfConcept.Behaviors
{
    public interface ITextEditable
    {
        void SetText(string textValue);

        void AppendText(string textValue);

        void Clear();
    }
}