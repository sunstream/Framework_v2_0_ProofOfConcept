using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Selenium
{
    public class SeleniumDriverDecorator : IDriverDecorator
    {
        private IWebDriver _driver;
        private IWebDriver Driver
        {
            get
            {
                if (!DriverCreated)
                {
                    _driver = DependencyManager.Kernel.Get<IWebDriver>();
                }
                return _driver;
            }
        }

        public bool DriverCreated
        {
            get { return !(_driver == null || IsDisposed()); }
            set { }
        }

        public bool ApplicationStarted
        {
            get
            {
                if (DriverCreated)
                    return _driver.Url != "data:,";
                return false;
            }
            set { }
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