using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProofOfConcept.Samples
{
    [TestClass]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class DependencyManagerTests
    {
        [TestInitialize]
        public void testSetup()
        {
            //DependencyManager.Kernel.
        }
    }
}