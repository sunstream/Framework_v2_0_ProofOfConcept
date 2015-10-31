using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Tests.Component.TestObjects.Contexts;
using ProofOfConcept.Tests.Component.TestObjects.Pages;
using ProofOfConcept.Tests.Component.TestTemplates;

namespace ProofOfConcept.Tests.Samples.Behaviors
{
    [TestClass]
    public class HtmlButtonTests : UiTestDriverClosedBetweenTests
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
