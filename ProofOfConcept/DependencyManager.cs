using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;

namespace ProofOfConcept
{

    public static class DependencyManager
    {
        private static readonly ThreadLocal<IKernel> _activeKernel = new ThreadLocal<IKernel>();
        public static IKernel Kernel
        {
            get
            {
                if (!_activeKernel.IsValueCreated)
                {
                    _activeKernel.Value = InitKernel();
                }
                return _activeKernel.Value;
            }
            private set { _activeKernel.Value = value; }
        }

        //private static readonly ThreadLocal<string> _activeTool = new ThreadLocal<string>();
        private static readonly ThreadLocal<ToolFamily> _activeTool = new ThreadLocal<ToolFamily>();

        //public static string Tool
        public static ToolFamily Tool
        {
            get
            {
                if (!_activeTool.IsValueCreated)
                {
                    string defaultToolName = ConfigurationManager.AppSettings["toolFamily"];
                    try
                    {
                        _activeTool.Value = (ToolFamily)Enum.Parse(typeof(ToolFamily), defaultToolName);
                    }
                    catch (Exception e)
                    {
                        if (e is ArgumentNullException | e is ArgumentException)
                        {
                            throw new DependencyConfigurationException(
                                "Failed to parse ToolFamily parameter in in appSettings section of app.config file. Expected format: <add key=\"toolFamily\" value=\"Selenium\"/>");
                        }
                        throw;
                    }
                    //_activeTool.Value = defaultToolName;
                }
                return _activeTool.Value;
            }
            set { _activeTool.Value = value; }
        }

        private static IKernel InitKernel()
        {
            DependencyConfiguration configuration = (DependencyConfiguration) ConfigurationManager.GetSection(DependencyConfiguration.SectionName);
            IKernel kernel = new StandardKernel();
            foreach (DependencyElement dependency in configuration.Dependencies)
            {
                try
                {
                    Type interfaceType = Type.GetType(dependency.InterfaceName);
                    if (dependency.InterfaceName == dependency.ClassName)
                    {
                        kernel.Bind(interfaceType).ToSelf();
                    }
                    else
                    {
                        Type resolvedByType = Type.GetType(dependency.ClassName);
                        if (dependency.HasToolFamilyParameter)
                        {
                            if (dependency.IsSingleton)
                            {
                                kernel.Bind(interfaceType)
                                    .To(resolvedByType)
                                    .InSingletonScope()
                                    .Named(dependency.ToolFamily);
                            }
                            else
                            {
                                kernel.Bind(interfaceType)
                                    .To(resolvedByType)
                                    .Named(dependency.ToolFamily);
                            }
                        }
                        else
                        {
                            if (dependency.IsSingleton)
                            {
                                kernel.Bind(interfaceType)
                                    .To(resolvedByType)
                                    .InSingletonScope();
                            }
                            else
                            {
                                kernel.Bind(interfaceType)
                                    .To(resolvedByType);
                            }
                        }
                   }
                    
                }
                catch (ArgumentNullException e)
                {
                    throw new DependencyConfigurationException(
                        string.Format(
                            "No proper type name provided in dependency configuration node. Node description:{0}{1}{2}",
                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                        e);
                }
                catch (FileLoadException e)
                {
                    throw new DependencyConfigurationException(
                        string.Format(
                            "Failed to load the assembly described in the node (or one of its dependencies). Node description:{0}{1}{2}",
                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                        e);
                }
                catch (BadImageFormatException e)
                {
                    throw new DependencyConfigurationException(
                        string.Format(
                            "The assembly described in the node (or one of its dependencies) is invalid. Node description:{0}{1}{2}",
                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                        e);
                }
                

            }
            return kernel;
        }
        

    }

    public class DependencyConfigurationException : Exception
    {
        public DependencyConfigurationException(string message, Exception innerException)
            : base(message, innerException) {}
        public DependencyConfigurationException(string message)
            : base(message) { }
    }

    public enum ToolFamily
    {
        Selenium,
        CodedUI,
        Protractor
    }

    //public class AutomationTool
    //{
    //    public const string Selenium = "Selenium";
    //    public const string CodedUI = "CodedUI";
    //    public const string Protractor = "Protractor";
    //}
    
}
