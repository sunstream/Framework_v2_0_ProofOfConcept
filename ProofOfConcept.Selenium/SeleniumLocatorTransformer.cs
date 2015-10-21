using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumLocatorTransformer : ILocatorTransformer<By>
    {
        public By GetNativeLocator(FindBy findBy)
        {
            string value = findBy.Value;
            By result;
            switch (findBy.How)
            {
                case How.ClassName:
                    result = By.ClassName(value);
                    break;
                case How.Id:
                    result = By.Id(value);
                    break;
                case How.CssSelector:
                    result = By.CssSelector(value);
                    break;
                case How.LinkText:
                    result = By.LinkText(value);
                    break;
                case How.Name:
                    result = By.Name(value);
                    break;
                case How.PartialLinkText:
                    result = By.PartialLinkText(value);
                    break;
                case How.TagName:
                    result = By.TagName(value);
                    break;
                case How.Xpath:
                    result = By.XPath(value);
                    break;
                default:
                    throw new ArgumentException(string.Format("findBy type [{0}] is not described in {1}: cannot apply search by provided value [{2}].",
                        findBy.How,
                        new StackFrame().GetMethod().DeclaringType.Name,
                        value));

            }
            return result;
        }
    }
}
