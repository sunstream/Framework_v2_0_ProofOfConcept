using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace ProofOfConcept.Tests
{

    [TestClass]
    public class Edit_ElementSearchConfigurationSettings_Tests
    {

        [TestMethod]
        public void Change_ElementSearchConfigurationSettings_Timeout()
        {
            var value = new TimeSpan(0, 0, 1);
            Settings.ElementSearchConfigurationSettings.Timeout = value;
            Assert.AreEqual(value, Settings.ElementSearchConfigurationSettings.Timeout);
        }

    }

    [TestClass]
    public class Read_ElementSearchConfigurationSettings_Tests
    {

        [TestMethod]
        public void ElementSearchConfigurationSettings_Timeout_DefaultValue()
        {
            var configuration = new ElementSearchConfigurationSettings();
            Assert.AreEqual(TimeSpan.Parse(ElementSearchConfigurationSettings.TimeoutDefault), configuration.Timeout);
        }

        [TestMethod]
        public void Get_ElementSearchConfigurationSettings()
        {
            Assert.IsNotNull(Settings.ElementSearchConfigurationSettings);
        }

        [TestMethod]
        public void Get_ElementSearchConfigurationSettings_Timeout()
        {
            var elementSearchConfigurationSettings = (ElementSearchConfigurationSettings)ConfigurationManager.GetSection("ProofOfConcept.ElementSearchConfigurationSettings");
            Assert.AreEqual(elementSearchConfigurationSettings.Timeout, Settings.ElementSearchConfigurationSettings.Timeout);
        }

    }

}