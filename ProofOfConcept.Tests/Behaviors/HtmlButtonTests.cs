using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Tests.TestObjects.Contexts;
using ProofOfConcept.Tests.TestObjects.Pages;

namespace ProofOfConcept.Tests.Behaviors
{
    [TestClass]
    public class HtmlButtonTests : BaseMsTest
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
