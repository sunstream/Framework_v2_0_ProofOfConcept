using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Services;

namespace ProofOfConcept.Tests
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class BaseMsTest
    {
        //[TestCleanup]
        //public void KillDriver()
        //{
        //    DependencyManager.Kernel.Get<NavigationService>().Stop();
        //}
    }
}
