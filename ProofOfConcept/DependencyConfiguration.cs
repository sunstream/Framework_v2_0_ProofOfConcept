using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProofOfConcept
{
    public class DependencyElement : ConfigurationElement
    {
        [ConfigurationProperty("interface", IsRequired = true)]
        public string InterfaceName
        {
            get { return (string) this["interface"]; }
        }
        [ConfigurationProperty("toolFamily", IsRequired = false)]
        public string ToolFamily
        {
            get { return (string) this["toolFamily"]; }
        }
        [ConfigurationProperty("resolvedBy", IsRequired = true)]
        public string ClassName
        {
            get { return (string)this["resolvedBy"]; }
        }

        public bool HasToolFamilyParameter
        {
            get { return ToolFamily != null; }
        }
    }
    
    public class DependencyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DependencyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DependencyElement) element).ToolFamily + "." + ((DependencyElement) element).InterfaceName;
        }
    }

    public class DependencyConfiguration : ConfigurationSection
    {
        public const string SectionName = "dependenciesSection";
        [ConfigurationProperty("dependencies", IsDefaultCollection = true)]
        public DependencyCollection Dependencies
        {
            get { return (DependencyCollection) this["dependencies"]; }
        }
    }
}
