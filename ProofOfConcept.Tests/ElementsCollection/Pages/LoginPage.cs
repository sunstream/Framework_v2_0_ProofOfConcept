using ProofOfConcept.Selenium;
using ProofOfConcept.Tests.TestObjects.Elements;

namespace ProofOfConcept.Tests.ElementsCollection.Pages
{
    class LoginPage : IPage
    {
        public static string Url = "https://roadshowaccess.qx.ipreo.com/";

        [FindBy(How.CssSelector, "input.form-control")]
        public ElementsCollection<HtmlTextField> Fields;
    }

}