using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Tests.Component.TestObjects.Contexts;
using ProofOfConcept.Tests.Component.TestObjects.Pages;
using ProofOfConcept.Tests.Component.TestTemplates;

namespace ProofOfConcept.Tests.Component.ElementBehaviorTests
{
    [TestClass]
    public class HtmlButtonTests : UiTest
    {
        private LoginContext _loginContext;

        [TestInitialize]
        public void Init()
        {
            ShareDriverBetweenSessions();

            _loginContext = new LoginContext();
            _loginContext.OpenApplication();
        }

        [ClassCleanup]
        public static void Teardown()
        {
            KillDriverIfLaunched();
        }

        [TestMethod]
        public void GetTextTest()
        {
            var button = DependencyManager.Kernel.Get<IPageFactory>().Create<LoginPage>().LoginForm.LoginButton;
            Assert.AreEqual(button.GetText(), "LOGIN");
        }
    }
}
