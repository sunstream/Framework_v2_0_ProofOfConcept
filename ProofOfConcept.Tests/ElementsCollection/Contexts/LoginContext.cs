using Ninject;
using ProofOfConcept.Services;
using ProofOfConcept.Tests.ElementsCollection.Pages;

namespace ProofOfConcept.Tests.ElementsCollection.Contexts
{
    public class LoginContext
    {
        private readonly IPageFactory _pageFactory;
        private readonly NavigationService _navigationService;
        private LoginPage _loginPage;

        public LoginContext()
        {
            _pageFactory = DependencyManager.Kernel.Get<IPageFactory>();
            _navigationService = DependencyManager.Kernel.Get<NavigationService>();
        }

        public void OpenApplication()
        {
            _navigationService.NavigateTo(LoginPage.Url);
            _loginPage = _pageFactory.Create<LoginPage>();
            var x = _loginPage.Fields[0];
        }

    }
}
