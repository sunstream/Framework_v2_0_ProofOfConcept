using OpenQA.Selenium;
using ProofOfConcept.Selenium;
namespace ProofOfConcept.Tests.TestObjects

{
    public class HtmlTextField : SeleniumElement
    {
        public HtmlTextField(IWebElement webElement) : base(webElement) { }
    }

}