using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;

namespace ProofOfConcept.Tests.Component.TestTemplates
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class UiTest
    {
        public static void ShareDriverBetweenSessions()
        {
            NavigationService navigationService = DependencyManager.Kernel.Get<NavigationService>();
            navigationService.ShareDriverBetweenTests();
        }

        public static void CloseDriverBetweenSessions()
        {
            NavigationService navigationService = DependencyManager.Kernel.Get<NavigationService>();
            navigationService.CloseDriverBetweenTests();
        }

        public static void KillDriverIfLaunched()
        {
            DependencyManager.Kernel.Get<NavigationService>().Driver.Stop();
        }
    }
}