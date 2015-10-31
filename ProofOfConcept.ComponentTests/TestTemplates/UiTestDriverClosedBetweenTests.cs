using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;

namespace ProofOfConcept.ComponentTests.TestTemplates
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class UiTestDriverClosedBetweenTests
    {
        [ClassInitialize]
        public void ShareDriverBetweenSessions()
        {
            NavigationService navigationService = DependencyManager.Kernel.Get<NavigationService>();
            navigationService.CloseDriverBetweenTests();
        }

        [TestCleanup]
        public void KillDriverIfLaunched()
        {
            DependencyManager.Kernel.Get<NavigationService>().Stop();
        }
    }
}
