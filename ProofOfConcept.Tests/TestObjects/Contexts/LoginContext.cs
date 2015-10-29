using System;
using System.Configuration;
using System.Threading;
using Ninject;
using ProofOfConcept.Selenium;
using ProofOfConcept.Tests.TestObjects.Pages;

namespace ProofOfConcept.Tests.TestObjects.Contexts
{
    public class LoginContext : ContextBase
    {
        private readonly IPageFactory _pageFactory;
        private readonly NavigationService _navigationService;
        private LoginPage _loginPage;

        public LoginContext()
        {
            //_pageFactory = new PageFactoryBase();
            Kernel.Bind<IPageFactory>().To<PageFactoryBase>();
            Kernel.Bind<IDriverDecorator>().To<SeleniumDriver>();
            Kernel.Bind<IElement>().To<SeleniumElement>();
            Kernel.Bind<IHow>().To<ProofOfConcept.Selenium.How>();
            Kernel.Bind<NavigationService>().To<NavigationService>();

            //Kernel.Bind(typeof(ILocatorTransformer<>)).To(typeof(SeleniumLocatorTransformer));
            Kernel.Bind(typeof(IElementSearchConfiguration)).To(typeof(SeleniumElementFinder));

            _pageFactory = Kernel.Get<IPageFactory>();
            _navigationService = Kernel.Get<NavigationService>();
        }

        public void OpenApplication()
        {
            _navigationService.NavigateTo(LoginPage.Url);
            _loginPage = _pageFactory.Create<LoginPage>();
        }

        public void LoginToApplication()
        {
            _loginPage.LoginForm.LoginField.SetText( _loginPage.LoginValue );
            _loginPage.LoginForm.PasswordField.SetText( _loginPage.PasswordValue );
            _loginPage.LoginForm.LoginButton.Click();
        }

        public void VerifyLandingPage()
        {
            _navigationService.VerifyPageUrl("https://roadshowaccess.qx.ipreo.com/Deal");
        }

    }

    public class NavigationService
    {
        private readonly IDriverDecorator _driver;

        public NavigationService(IDriverDecorator driverDecorator)
        {
            this._driver = driverDecorator;
        }
        
        public void NavigateTo(string url)
        {
            _driver.NavigateTo(url);
        }

        public void VerifyPageUrl(string expectedUrl)
        {
            Thread.Sleep(3000);
            if (String.Compare(_driver.GetCurrentUrl(), expectedUrl, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                throw new Exception("Invalid URL (custom assert, just for demo purposes).");
            }
        }
    }
}