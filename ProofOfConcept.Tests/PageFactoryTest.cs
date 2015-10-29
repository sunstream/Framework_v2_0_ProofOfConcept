using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Tests.TestObjects.Contexts;

namespace ProofOfConcept.Tests
{
    [TestClass]
    public class PageFactoryTest
    {
        private LoginContext _loginContext;

        [TestMethod]
        public void TestOpensRoadshowAccessWebsite()
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
        }

        [TestMethod]
        public void TestLogsInto()
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
        }
    }
}
