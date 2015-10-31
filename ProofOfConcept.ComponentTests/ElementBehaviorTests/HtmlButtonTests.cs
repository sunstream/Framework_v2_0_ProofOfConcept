using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.ComponentTests.TestObjects.Contexts;
using ProofOfConcept.ComponentTests.TestObjects.Pages;
using ProofOfConcept.ComponentTests.TestTemplates;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.ComponentTests.ElementBehaviorTests
{
    [TestClass]
    public class HtmlButtonTests : UiTestDriverSharedBetweenTests
    {
        private LoginContext _loginContext;

        [TestInitialize]
        public void Init()
        {
            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
        }

        [TestMethod]
        public void GetTextTest()
        {
            var button = DependencyManager.Kernel.Get<IPageFactory>().Create<LoginPage>().LoginForm.LoginButton;
            Assert.AreEqual(button.GetText(), "LOGIN");
        }
    }
}
