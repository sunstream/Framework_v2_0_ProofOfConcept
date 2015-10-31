using System;
using System.Configuration;

namespace ProofOfConcept
{
    public interface ITimeoutSettings
    {
        TimeSpan ElementTimeout { get; set; }
        TimeSpan PageTimeout { get; set; }
    }

    public class TimeoutSettings : ConfigurationSection, ITimeoutSettings
    {
        public const string ElementTimeoutDefault = "00:00:10";
        public const string PageTimeoutDefault = "00:00:30";

        [ConfigurationProperty("ElementTimeout", DefaultValue = ElementTimeoutDefault)]
        public TimeSpan ElementTimeout
        {
            get { return (TimeSpan)this["ElementTimeout"]; }
            set { this["ElementTimeout"] = value; }
        }

        [ConfigurationProperty("PageTimeout", DefaultValue = PageTimeoutDefault)]
        public TimeSpan PageTimeout
        {
            get { return (TimeSpan)this["PageTimeout"]; }
            set { this["PageTimeout"] = value; }
        }

    }

}