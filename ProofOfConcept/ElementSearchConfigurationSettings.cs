using System;
using System.Configuration;

namespace ProofOfConcept
{

    public interface IElementSearchConfigurationSettings
    {
        TimeSpan Timeout { get; set; }
    }

    public class ElementSearchConfigurationSettings : ConfigurationSection, IElementSearchConfigurationSettings
    {

        public const string TimeoutDefault = "00:00:05";

        [ConfigurationProperty("Timeout", DefaultValue = TimeoutDefault)]
        public TimeSpan Timeout
        {
            get { return (TimeSpan)this["Timeout"]; }
            set { this["Timeout"] = value; }
        }

    }

}