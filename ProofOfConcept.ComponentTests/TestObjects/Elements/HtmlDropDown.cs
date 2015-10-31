using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Tests.Component.TestObjects.Elements
{
    public class HtmlDropDown : ElementBase, IDropDownBehavior
    {
        private IDropDownBehavior _dropDownBehavior;
        public HtmlDropDown()
        {
            _dropDownBehavior = AddBehavior<IDropDownBehavior>();
        }
        public void Select(string value)
        {
            _dropDownBehavior.Select(value);
        }

        public bool IsSelected(string value)
        {
            return _dropDownBehavior.IsSelected(value);
        }

        public string GetSelected()
        {
            return _dropDownBehavior.GetSelected();
        }
    }
}
