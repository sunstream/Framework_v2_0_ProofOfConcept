namespace ProofOfConcept.Behaviors.Traits
{
    public interface ISelectableValue
    {
        void Select(string value);

        bool IsSelected(string value);

        string GetSelected();
    }
}