using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Tests.Component.TestObjects.Contexts;
using ProofOfConcept.Tests.Component.TestTemplates;

namespace ProofOfConcept.Tests.Samples
{
    [TestClass]
    public class SampleRoadshowAccessTest : UiTestDriverClosedBetweenTests
    {
        private LoginContext _loginContext;
        private InvestorContext _investorContext;
        
        [ClassInitialize]
        public static void SetupDriver(TestContext context)
        {
            CloseDriverBetweenSessions();
        }

        [TestInitialize]
        public void InitContext()
        {
            _loginContext = new LoginContext();
            _investorContext = new InvestorContext();
        }

        [TestMethod]
        public void TestOpensRoadshowAccessWebsite()
        {
            _loginContext.OpenApplication();
            _loginContext.VerifyLoginPageUrl();
        }

        [TestMethod]
        public void TestLogsInAsInvestor()
        {
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();
            _investorContext.VerifyLandingPageUrl();
        }

    }
}
