using ProofOfConcept.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Tests.TestObjects.Contexts;
using ProofOfConcept.Tests.TestObjects.Pages;

namespace ProofOfConcept.Tests
{
    [TestClass]
    public class PageFactoryTest : BaseMsTest
    {
        private LoginContext _loginContext;
        
        [TestInitialize]
        public void InitContext()
        {
            _loginContext = new LoginContext();
        }

        [TestMethod]
        public void TestOpensRoadshowAccessWebsite()
        {
            _loginContext.OpenApplication();
        }

        [TestMethod]
        public void TestLogsInAsInvestor()
        {
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();
        }

        [TestMethod]
        public void TestSelectsDealTypeOnInvestorDashboard()
        {
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();
            _loginContext.SelectDealTypeOnInvestorDashboardPage("All Deals");
            _loginContext.ToString();
        }
    }
}
