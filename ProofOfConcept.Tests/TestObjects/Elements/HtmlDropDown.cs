using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProofOfConcept.Behaviors;

namespace ProofOfConcept.Tests.TestObjects.Elements
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
    }
}
