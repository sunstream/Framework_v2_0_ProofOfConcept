using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;

namespace ProofOfConcept.Tests.Component.TestTemplates
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class UiTestDriverClosedBetweenTests : UiTest
    {
//        [ClassInitialize]
//        public void ShareDriverBetweenSessions()
//        {
//            NavigationService navigationService = DependencyManager.Kernel.Get<NavigationService>();
//            navigationService.CloseDriverBetweenTests();
//        }

        [TestCleanup]
        public void KillDriverIfLaunched()
        {
            DependencyManager.Kernel.Get<NavigationService>().Stop();
        }
    }
}
