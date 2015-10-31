using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.ComponentTests.TestObjects.Contexts;
using ProofOfConcept.ComponentTests.TestObjects.Pages;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Samples.Behaviors
{
    [TestClass]
    public class HtmlTextFieldTests
    {
        private LoginContext _loginContext;
        private InvestorDashboardPage _dashboard;

        [TestInitialize]
        public void Init()
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();

            _dashboard = DependencyManager.Kernel.Get<IPageFactory>().Create<InvestorDashboardPage>();
        }

        [TestMethod]
        public void SetTextTest()
        {
            var textValue = "Hello, World!";
            _dashboard.DealAccessCode.SetText(textValue);

            Assert.Equals(textValue, ((IWebElement)_dashboard.DealAccessCode.NativeElement).Text);
        }

        [TestMethod]
        public void GetTextTest()
        {
            Assert.Equals(_dashboard.DealAccessCode.GetText(), ((IWebElement)_dashboard.DealAccessCode.NativeElement).Text);
        }

        [TestMethod]
        public void AppendTextTest()
        {
            var textValue1 = "Hello";
            var textValue2 = ", World!";

            _dashboard.DealAccessCode.AppendText(textValue1);
            StringAssert.EndsWith(textValue1, _dashboard.DealAccessCode.GetText());

            _dashboard.DealAccessCode.AppendText(textValue2);
            StringAssert.EndsWith(_dashboard.DealAccessCode.GetText(), string.Format("{0}{1}", textValue1, textValue2));
        }

        [TestMethod]
        public void ClearTest()
        {
            var textValue = "Hello, World!";
            _dashboard.DealAccessCode.SetText(textValue);

            Assert.Equals(textValue, _dashboard.DealAccessCode.GetText());

            _dashboard.DealAccessCode.Clear();
            Assert.IsTrue(string.IsNullOrEmpty(_dashboard.DealAccessCode.GetText()));
        }
    }
}