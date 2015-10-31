using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Samples.TestObjects.Contexts;
using ProofOfConcept.Samples.TestObjects.Elements;

namespace ProofOfConcept.Samples.Behaviors
{
    [TestClass]
    public class HtmlCheckBoxTests
    {
        private LoginContext _loginContext;
        //private DisclaimerPage _disclaimerPage;
        private HtmlCheckBox _disclaimerCheckBox;

        private const string CHECKBOX_LABEL = @"I acknowledge that I have read the disclaimer";

        //[TestInitialize]
        //public void Init()
        //{
        //    _loginContext = new LoginContext();
        //    _loginContext.OpenApplication();
        //    _loginContext.LoginToApplication();

        //    //_disclaimerPage = DependencyManager.Kernel.Get<IPageFactory>().Create<DisclaimerPage>();
        //    //_disclaimerCheckBox = _disclaimerPage.DisclaimerCheckBox;
        //}

        //[TestMethod]
        //public void CheckTest()
        //{
        //    _disclaimerCheckBox.Check();

        //    Assert.IsTrue(((IWebElement)_disclaimerCheckBox.NativeElement).Selected);
        //}

        //[TestMethod]
        //public void UncheckTest()
        //{
        //    _disclaimerCheckBox.Uncheck();

        //    Assert.IsFalse(((IWebElement)_disclaimerCheckBox.NativeElement).Selected);
        //}

        //[TestMethod]
        //public void IsCheckedTest()
        //{
        //    Assert.Equals(_disclaimerCheckBox.IsChecked(), ((IWebElement)_disclaimerCheckBox.NativeElement).Selected);
        //}

        //[TestMethod]
        //public void GetTextTest()
        //{
        //    Assert.Equals(_disclaimerCheckBox.GetText().Trim(), CHECKBOX_LABEL);
        //}
    }
}
