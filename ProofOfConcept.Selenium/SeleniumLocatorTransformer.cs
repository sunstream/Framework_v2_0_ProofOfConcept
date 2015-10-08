using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumLocatorTransformer
    {
        public static By GetNativeLocator (ILocator locator)
        {
            return By.XPath("//*");
        }
    }
}
