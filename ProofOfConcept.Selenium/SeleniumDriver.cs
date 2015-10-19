using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriver : IDriverDecorator
    {
        private readonly IWebDriver _driver;

        public SeleniumDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string url)
        {
            _driver.Url = url;
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }
    }
}