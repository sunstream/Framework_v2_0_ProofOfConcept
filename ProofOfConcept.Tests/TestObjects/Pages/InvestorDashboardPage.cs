using ProofOfConcept.Samples.TestObjects.Elements;
using ProofOfConcept.Selenium;

namespace ProofOfConcept.Samples.TestObjects.Pages
{
    public class InvestorDashboardPage : IPage
    {
        public static string Url = "https://roadshowaccess.qx.ipreo.com/Deal";

        [FindBy(How.ClassName, "input-group")]
        public DealSearchField DealSearchField;

        [FindBy(How.Id, "filter")]
        public DealSearchFilters Filters;

        [FindBy(How.Id, "dealCode")]
        public HtmlTextField DealAccessCode;

        [FindBy(How.Id, "InvestorDealFilterId")]
        public HtmlDropDown DealTypeFilter;

        public void WaitUntilLoaded()
        {
            
        }
    }
}
