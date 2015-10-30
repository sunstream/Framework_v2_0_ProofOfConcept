using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriverDecorator : IDriverDecorator
    {
        //TODO: make _driver private
        private readonly IWebDriver _driver;

        public SeleniumDriverDecorator(IWebDriver driver)
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

        public TDriverType GetDriver<TDriverType>() where TDriverType : class
        {
            return _driver as TDriverType;
        }

        public void Stop()
        {
            _driver.Close();
        }
    }
}