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
        [TestCleanup]
        public void CloseDriver()
        {
            KillDriverIfLaunched();
        }
    }
}
