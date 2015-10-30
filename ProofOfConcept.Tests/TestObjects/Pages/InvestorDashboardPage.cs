using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProofOfConcept.Selenium;
using ProofOfConcept.Tests.TestObjects.Elements;

namespace ProofOfConcept.Tests.TestObjects.Pages
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
    }
}
