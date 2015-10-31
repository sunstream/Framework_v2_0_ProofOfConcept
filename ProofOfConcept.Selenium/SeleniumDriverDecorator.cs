using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriverDecorator : IDriverDecorator
    {
        private IWebDriver _driver;
        private IWebDriver Driver
        {
            get
            {
                if (_driver == null || IsDisposed())
                {
                    _driver = DependencyManager.Kernel.Get<IWebDriver>();
                }
                return _driver;
            }
        }

        private bool IsDisposed()
        {
            bool sessionDead = true;
            try
            {
                var result = _driver.Title;
                sessionDead = false;
            }
            catch
            {
            }
            return sessionDead;
        }

        public SeleniumDriverDecorator(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string url)
        {
            Driver.Url = url;
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        public TDriverType GetDriver<TDriverType>() where TDriverType : class
        {
            return Driver as TDriverType;
        }

        public void Stop()
        {
            try
            {
                if (_driver != null) _driver.Quit();
            }
            catch (Exception e)
            {
                if (!(e is OpenQA.Selenium.DriverServiceNotFoundException))
                {
                    throw;
                }
            }
        }
    }
}