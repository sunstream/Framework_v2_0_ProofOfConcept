using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Tests.Unit
{
    [TestClass]
    public class TimeoutConfigurationTests
    {
        [TestCleanup]
        public void RestoreDefaultTimeouts()
        {
            SettingsService.TimeoutSettings.ElementTimeout = ((TimeoutSettings) ConfigurationManager.GetSection(TimeoutSettings.SectionName)).ElementTimeout;
            SettingsService.TimeoutSettings.PageTimeout = ((TimeoutSettings)ConfigurationManager.GetSection(TimeoutSettings.SectionName)).PageTimeout;
        }

        [TestMethod]
        public void TestChangesElementTimeout()
        {
            var value = new TimeSpan(0, 0, 1);
            SettingsService.TimeoutSettings.ElementTimeout = value;
            Assert.AreEqual(value, SettingsService.TimeoutSettings.ElementTimeout);
        }

        public void TestChangesPageTimeout()
        {
            var value = new TimeSpan(0, 0, 1);
            SettingsService.TimeoutSettings.PageTimeout = value;
            Assert.AreEqual(value, SettingsService.TimeoutSettings.PageTimeout);
        }

        [TestMethod]
        public void TestValidatesDefaultElementTimeout()
        {
            var configuration = new TimeoutSettings();
            Assert.AreEqual(TimeSpan.Parse(TimeoutSettings.ElementTimeoutDefault), configuration.ElementTimeout);
        }

        [TestMethod]
        public void TestValidatesDefaultPageTimeout()
        {
            var configuration = new TimeoutSettings();
            Assert.AreEqual(TimeSpan.Parse(TimeoutSettings.PageTimeoutDefault), configuration.PageTimeout);
        }

        [TestMethod]
        public void TestVerifiesThatElementSearchConfigurationExists()
        {
            Assert.IsNotNull(SettingsService.TimeoutSettings);
        }

        [TestMethod]
        public void TestValidatesElementTimeoutProperty()
        {
            var elementSearchConfigurationSettings = (TimeoutSettings)ConfigurationManager.GetSection(TimeoutSettings.SectionName);
            Assert.AreEqual(elementSearchConfigurationSettings.ElementTimeout, SettingsService.TimeoutSettings.ElementTimeout);
        }

        [TestMethod]
        public void TestValidatesPageTimeoutProperty()
        {
            var elementSearchConfigurationSettings = (TimeoutSettings)ConfigurationManager.GetSection(TimeoutSettings.SectionName);
            Assert.AreEqual(elementSearchConfigurationSettings.PageTimeout, SettingsService.TimeoutSettings.PageTimeout);
        }


    }

    [TestClass]
    public class Read_ElementSearchConfigurationSettings_Tests
    {

        

        

       
    }

}