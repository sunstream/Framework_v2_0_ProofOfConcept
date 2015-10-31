using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Services;

namespace ProofOfConcept.Samples
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class BaseMsTest
    {
        [TestCleanup]
        public void KillDriverIfLaunched()
        {
            DependencyManager.Kernel.Get<NavigationService>().Stop();
        }
    }
}
