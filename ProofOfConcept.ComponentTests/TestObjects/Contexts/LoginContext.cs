using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.ComponentTests.TestObjects.Pages;

namespace ProofOfConcept.ComponentTests.TestObjects.Contexts
{
    public class LoginContext : ContextBase
    {
        private LoginPage _loginPage;

        public void OpenApplication()
        {
            NavigationService.NavigateTo(LoginPage.Url);
            _loginPage = PageFactory.Create<LoginPage>();
        }

        public void LoginToApplication()
        {
            _loginPage.LoginForm.LoginField.SetText( _loginPage.LoginValue );
            _loginPage.LoginForm.PasswordField.SetText( _loginPage.PasswordValue );
            _loginPage.LoginForm.LoginButton.Click();
        }

        public void VerifyLoginPageUrl()
        {
            const string expectedUrl = "https://roadshowaccess.qx.ipreo.com/";
            Assert.IsTrue(NavigationService.PageUrlEquals(expectedUrl), 
                string.Format("Login page URL mismatch: expected [{0}], but found [{1}].", 
                expectedUrl, NavigationService.GetCurrentUrl()));
        }



    }
}
