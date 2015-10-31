using System;
using System.Configuration;

namespace ProofOfConcept.Configuration
{
    public class DependencyElement : ConfigurationElement, IDescribable
    {
        public static string ProperNodeFormat = string.Format("Proper node format instruction:{0}<add automationTool = \"Selenium | CodedUI | {{other family name}}\"{0}\t" 
            + "interface = \"{{Full interface name}}, {{Assembly name}}\" - e.g. \"ProofOfConcept.IPageFactory, ProofOfConcept\"{0}\t" 
            + "resolvedBy = \"{{Full class name}}, {{Assembly name}}\" - e.g. \"ProofOfConcept.PageFactoryBase, ProofOfConcept\">{0}</add>", Environment.NewLine);
        
        [ConfigurationProperty("interface", IsRequired = true)]
        public string InterfaceName
        {
            get { return (string) this["interface"]; }
        }
        [ConfigurationProperty("automationTool", IsRequired = false)]
        public string automationTool
        {
            get { return (string) this["automationTool"]; }
        }
        [ConfigurationProperty("resolvedBy", IsRequired = true)]
        public string ClassName
        {
            get { return (string)this["resolvedBy"]; }
        }

        [ConfigurationProperty("isSingleton", IsRequired = false)]
        public bool IsSingleton
        {
            get { return (bool)this["isSingleton"]; }
        }

        [ConfigurationProperty("isThreadLocal", IsRequired = false)]
        public bool IsThreadLocal
        {
            get { return (bool)this["isThreadLocal"]; }
        }

        public bool HasAutomationToolParameter
        {
            get { return !string.IsNullOrEmpty(automationTool); }
        }

        public string Describe()
        {
            string description = HasAutomationToolParameter ? 
                string.Format("Interface [{0}] is resolved by [{1}] for {2} test objects.", InterfaceName, ClassName, automationTool) : 
                string.Format("Interface [{0}] is always resolved by [{1}].", InterfaceName, ClassName);
            return description;
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
            return ((DependencyElement) element).automationTool + "." + ((DependencyElement) element).InterfaceName;
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
