using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.ComponentTests.TestObjects.Contexts;
using ProofOfConcept.ComponentTests.TestObjects.Pages;
using ProofOfConcept.ComponentTests.TestTemplates;

namespace ProofOfConcept.ComponentTests.ElementBehaviorTests
{
    [TestClass]
    public class HtmlTextFieldTest : UiTestDriverSharedBetweenTests
    {
        private static LoginContext _loginContext;
        private static InvestorDashboardPage _dashboard;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
            _loginContext.LoginToApplication();

            _dashboard = DependencyManager.Kernel.Get<IPageFactory>().Create<InvestorDashboardPage>();
        }

        [TestMethod]
        public void SetTextTest()
        {
            const string textValue = "Hello, World!";
            _dashboard.DealAccessCode.SetText(textValue);

            Assert.AreEqual(textValue, ((OpenQA.Selenium.IWebElement)_dashboard.DealAccessCode.NativeElement).GetAttribute("value"));
        }

        [TestMethod]
        public void GetTextTest()
        {
            Assert.AreEqual(_dashboard.DealAccessCode.GetText(), ((IWebElement)_dashboard.DealAccessCode.NativeElement).Text);
        }

        [TestMethod]
        public void AppendTextTest()
        {
            const string textValue1 = "Hello";
            const string textValue2 = ", World!";

            _dashboard.DealAccessCode.AppendText(textValue1);
            StringAssert.EndsWith(textValue1, _dashboard.DealAccessCode.GetText());

            _dashboard.DealAccessCode.AppendText(textValue2);
            StringAssert.EndsWith(_dashboard.DealAccessCode.GetText(), string.Format("{0}{1}", textValue1, textValue2));
        }

        [TestMethod]
        public void ClearTest()
        {
            const string textValue = "Hello, World!";
            _dashboard.DealAccessCode.SetText(textValue);

            Assert.AreEqual(textValue, _dashboard.DealAccessCode.GetText());

            _dashboard.DealAccessCode.Clear();
            Assert.IsTrue(string.IsNullOrEmpty(_dashboard.DealAccessCode.GetText()));
        }
    }
}