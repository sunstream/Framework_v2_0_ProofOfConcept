using Ninject;
using ProofOfConcept.Services;
using ProofOfConcept.Tests.TestObjects.Pages;

namespace ProofOfConcept.Tests.TestObjects.Contexts
{
    public class LoginContext 
    {
        private readonly IPageFactory _pageFactory;
        private readonly NavigationService _navigationService;
        private LoginPage _loginPage;

        public LoginContext()
        {
            //_pageFactory = new PageFactoryBase();          
            //Kernel.Bind(typeof(ILocatorTransformer<>)).To(typeof(SeleniumLocatorTransformer));
            //Kernel.Bind<IPageFactory>().To<PageFactoryBase>();
            //Kernel.Bind<IDriverDecorator>().To<SeleniumDriver>();
            //Kernel.Bind<IElement>().To<SeleniumElement>();
            //Kernel.Bind<IHow>().To<ProofOfConcept.Selenium.How>();
            //Kernel.Bind<NavigationService>().To<NavigationService>();
            //Kernel.Bind<IElementSearchConfiguration>().To<SeleniumElementFinder>();
            _pageFactory = DependencyManager.Kernel.Get<IPageFactory>();
            _navigationService = DependencyManager.Kernel.Get<NavigationService>();
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
}
