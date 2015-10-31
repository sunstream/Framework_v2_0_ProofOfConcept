﻿using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProofOfConcept.Samples
{
    [TestClass]
    public class TimeoutConfigurationTests
    {
        [TestMethod]
        public void TestChangesElementTimeout()
        {
            var value = new TimeSpan(0, 0, 1);
            Settings.TimeoutSettings.ElementTimeout = value;
            Assert.AreEqual(value, Settings.TimeoutSettings.ElementTimeout);
        }

        public void TestChangesPageTimeout()
        {
            var value = new TimeSpan(0, 0, 1);
            Settings.TimeoutSettings.PageTimeout = value;
            Assert.AreEqual(value, Settings.TimeoutSettings.PageTimeout);
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
            Assert.IsNotNull(Settings.TimeoutSettings);
        }

        [TestMethod]
        public void TestValidatesElementTimeoutProperty()
        {
            var elementSearchConfigurationSettings = (TimeoutSettings)ConfigurationManager.GetSection("ProofOfConcept.TimeoutSettings");
            Assert.AreEqual(elementSearchConfigurationSettings.ElementTimeout, Settings.TimeoutSettings.ElementTimeout);
        }

        [TestMethod]
        public void TestValidatesPageTimeoutProperty()
        {
            var elementSearchConfigurationSettings = (TimeoutSettings)ConfigurationManager.GetSection("ProofOfConcept.TimeoutSettings");
            Assert.AreEqual(elementSearchConfigurationSettings.PageTimeout, Settings.TimeoutSettings.PageTimeout);
        }


    }

    [TestClass]
    public class Read_ElementSearchConfigurationSettings_Tests
    {

        

        

       
    }

}