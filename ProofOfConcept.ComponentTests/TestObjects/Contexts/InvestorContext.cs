using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Tests.Component.TestObjects.Pages;

namespace ProofOfConcept.Tests.Component.TestObjects.Contexts
{
    public class InvestorContext : ContextBase
    {
        public InvestorDashboardPage InvestorDashboardPage;

        public void VerifyLandingPageUrl()
        {
            const string expectedUrl = "https://roadshowaccess.qx.ipreo.com/Deal";
            Assert.IsTrue(NavigationService.PageUrlEquals(expectedUrl),
                string.Format("Login page URL mismatch: expected [{0}], but found [{1}].",
                expectedUrl, NavigationService.GetCurrentUrl()));
        }

        public void SelectDealTypeOnInvestorDashboardPage(string dealType)
        {
            InvestorDashboardPage = PageFactory.Create<InvestorDashboardPage>();
            InvestorDashboardPage.DealTypeFilter.Select(dealType);
        }
    }
}