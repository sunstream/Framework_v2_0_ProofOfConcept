using System.Collections.Specialized;
using ProofOfConcept;
namespace ProofOfConcept.Tests.TestObjects
{
    public struct KVP
    {
        public string Name, Value;
    }

    class LoginPage : IPage
    {

        //URL: https://roadshowaccess.qx.ipreo.com/
        //Login: 

        //[FilterBy( new IsDisplayed(true) )]
        //[FilterByVisibility]
        [FindBy( FindBy.Xpath, "" )]
        [FilterBy( IsVisible, false )]
        public HtmlTextField LoginField;

        public IElement PasswordField;

        public string Login = "qainvestor@ipreo.com";
        public string Password = "Password1";

        [FindBy(FindBy.Xpath, "")]
        [FilterBy(IsVisible, false)]
        public LoginForm LoginForm;
    }

    public class LoginForm : IContainer
    {
        [FindBy(FindBy.Xpath, "")]
        [FilterBy(IsVisible, false)]
        public HtmlTextField LoginField;
    }
}
