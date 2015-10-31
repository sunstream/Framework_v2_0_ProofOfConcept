using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.ComponentTests.TestObjects.Pages;

namespace ProofOfConcept.ComponentTests.TestObjects.Contexts
{
    public class InvestorContext : ContextBase
    {
        private InvestorDashboardPage _investorDashboardPage;

        public void VerifyLandingPageUrl()
        {
            const string expectedUrl = "https://roadshowaccess.qx.ipreo.com/Deal";
            Assert.IsTrue(NavigationService.PageUrlEquals(expectedUrl),
                string.Format("Login page URL mismatch: expected [{0}], but found [{1}].",
                expectedUrl, NavigationService.GetCurrentUrl()));
        }

        public void SelectDealTypeOnInvestorDashboardPage(string dealType)
        {
            _investorDashboardPage = PageFactory.Create<InvestorDashboardPage>();
            _investorDashboardPage.DealTypeFilter.Select(dealType);
        }
    }
}