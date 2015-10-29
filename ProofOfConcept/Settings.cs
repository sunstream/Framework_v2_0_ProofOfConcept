using System;
using System.Threading;
using System.Configuration;

namespace ProofOfConcept
{
    public static class Settings
    {
        private static readonly ThreadLocal<ElementSearchConfigurationSettings> _elementSearchConfigurationSettings 
            = new ThreadLocal<ElementSearchConfigurationSettings>();
        public static IElementSearchConfigurationSettings ElementSearchConfigurationSettings
        {
            get
            {
                if (!_elementSearchConfigurationSettings.IsValueCreated)
                {
                    var result = GetConfiguration<ElementSearchConfigurationSettings>();
                    _elementSearchConfigurationSettings.Value = result;
                }
                return _elementSearchConfigurationSettings.Value;
            }
        }


        private static T GetConfiguration<T>()
        {
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
            {
                ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            }, ConfigurationUserLevel.None);
            foreach (var section in configuration.Sections)
            {
                if (section is T)
                {
                    return (T)section;
                }
            }
            return default(T);
        }

    }

    public class SettingsException : Exception
    {
        public SettingsException(string message, Exception innerException)
            : base(message, innerException) {}
        public SettingsException(string message)
            : base(message) { }
    }
}