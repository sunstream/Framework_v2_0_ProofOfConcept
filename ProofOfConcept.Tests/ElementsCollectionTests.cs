using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Tests.ElementsCollection.Contexts;

namespace ProofOfConcept.Tests
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class ElementsCollectionTests
    {
        private LoginContext _loginContext;

        [TestMethod]
        public void TestLogsInto()
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
        }
    }
}
