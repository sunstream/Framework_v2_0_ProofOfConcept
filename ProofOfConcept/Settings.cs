using System;
using System.Threading;
using System.Configuration;

namespace ProofOfConcept
{
    public static class Settings
    {

        private static ThreadLocal<ElementSearchConfigurationSettings> _elementSearchConfigurationSettings;
        public static IElementSearchConfigurationSettings ElementSearchConfigurationSettings
        {
            get
            {
                if (_elementSearchConfigurationSettings == null)
                {
                    _elementSearchConfigurationSettings = new ThreadLocal<ElementSearchConfigurationSettings>(() =>
                    {
                        var result = GetConfiguration<ElementSearchConfigurationSettings>();
                        return result != null ? result : new ElementSearchConfigurationSettings();
                    });
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
}