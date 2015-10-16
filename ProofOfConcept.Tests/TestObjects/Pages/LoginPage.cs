using ProofOfConcept.Selenium;
using ProofOfConcept.Tests.TestObjects.Elements;

namespace ProofOfConcept.Tests.TestObjects.Pages
{
    class LoginPage : IPage
    {

        //URL: https://roadshowaccess.qx.ipreo.com/

        public string Login = "qainvestor@ipreo.com";
        public string Password = "Password1";

        [FindBy(How.Id, "loginForm")]
        public LoginForm LoginForm;

    }

    public class LoginForm : IContainer
    {
        [FindBy(How.Name, "UserName")]
        [IsDisplayed]
        public HtmlTextField LoginField;

        [FindBy(How.ClassName, "input-sm")] 
        [HasAttribute("id", "Password")] 
        public HtmlTextField PasswordField;

        [FindBy(How.TagName, "button")]
        [IsDisplayed]
        [HasAttribute("type", "submit")] 
        public IElement LoginButton;

    }
}
