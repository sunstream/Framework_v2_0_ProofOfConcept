using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;
using ProofOfConcept.Tests.Component.TestObjects.Contexts;
using ProofOfConcept.Tests.Component.TestObjects.Pages;
using ProofOfConcept.Tests.Component.TestTemplates;

namespace ProofOfConcept.Tests.Component.ElementBehaviorTests
{
    [TestClass]
    public class HtmlDropDownTests : UiTest
    {
        private static LoginContext _loginContext;
        private static InvestorContext _investorContext;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            ShareDriverBetweenSessions();
            
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();

            _investorContext = new InvestorContext();
        }

        [ClassCleanup]
        public static void Teardown()
        {
            KillDriverIfLaunched();
        }

        [TestMethod]
        public void TestSelectsDealTypeOnInvestorDashboard()
        {
            _investorContext.SelectDealTypeOnInvestorDashboardPage("All Deals");
            Assert.AreEqual(_investorContext.InvestorDashboardPage.DealTypeFilter.GetSelected(), "All Deals", "Invalid deal type found selected on Investor Dashboard:");
        }
    }
}
