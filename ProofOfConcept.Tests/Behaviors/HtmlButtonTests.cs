using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Samples.TestObjects.Contexts;
using ProofOfConcept.Samples.TestObjects.Pages;

namespace ProofOfConcept.Samples.Behaviors
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
