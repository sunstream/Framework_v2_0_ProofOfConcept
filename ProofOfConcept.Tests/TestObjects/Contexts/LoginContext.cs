using System;
using System.Threading;
using ProofOfConcept.Tests.TestObjects.Pages;

namespace ProofOfConcept.Tests.TestObjects.Contexts
{
    public class LoginContext
    {
        private LoginPage _loginPage;

        public void OpenApplication()
        {
            NavigationService.NavigateTo(LoginPage.Url);
            _loginPage = PageFactory.GetPage<LoginPage>();
        }

        public void LoginToApplication()
        {
            _loginPage.LoginForm.LoginField.SetText( _loginPage.LoginValue );
            _loginPage.LoginForm.PasswordField.SetText( _loginPage.PasswordValue );
            _loginPage.LoginForm.LoginButton.Click();
        }

        public void VerifyLandingPage()
        {
            
        }

    }

    static class NavigationService
    {
        private static IDriverDecorator _driver;

        public static void NavigateTo(string url)
        {
            _driver.NavigateTo(url);
        }

        public static void VerifyPageUrl(string expectedUrl)
        {
            Thread.Sleep(3000);
            if (String.Compare(_driver.GetCurrentUrl(), expectedUrl, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                throw new Exception("Invalid URL (custom assert, just for demo purposes).");
            }
        }
    }
}