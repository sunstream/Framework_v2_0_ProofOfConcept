using System;
using System.Configuration;
using System.Threading;
using Ninject;

namespace ProofOfConcept.Configuration
{

    public static class DependencyManager
    {
        private static readonly ThreadLocal<IKernel> ActiveKernel = new ThreadLocal<IKernel>();
        public static IKernel Kernel
        {
            get
            {
                if (!ActiveKernel.IsValueCreated)
                {
                    ActiveKernel.Value = InitKernel();
                }
                return ActiveKernel.Value;
            }
            private set { ActiveKernel.Value = value; }
        }

        private static readonly ThreadLocal<AutomationTool> ActiveTool = new ThreadLocal<AutomationTool>();

        public static AutomationTool Tool
        {
            get
            {
                if (!ActiveTool.IsValueCreated)
                {
                    string defaultToolName = ConfigurationManager.AppSettings["automationTool"];
                    try
                    {
                        ActiveTool.Value = (AutomationTool)Enum.Parse(typeof(AutomationTool), defaultToolName);
                    }
                    catch (Exception e)
                    {
                        if (e is ArgumentNullException | e is ArgumentException)
                        {
                            throw new DependencyConfigurationException(
                                "Failed to parse AutomationTool parameter in in appSettings section of app.config file. Expected format: <add key=\"automationTool\" value=\"Selenium\"/>");
                        }
                        throw;
                    }
                }
                return ActiveTool.Value;
            }
            set { ActiveTool.Value = value; }
        }

        public static DependencyConfiguration GetDependencyConfiguration()
        {
            return (DependencyConfiguration)ConfigurationManager.GetSection(DependencyConfiguration.SectionName);
        }

        private static IKernel InitKernel()
        {
            DependencyConfiguration configuration = GetDependencyConfiguration();
            IKernel kernel = new StandardKernel();
            foreach (DependencyElement dependency in configuration.Dependencies)
            {
                #region Conditional binding
//                try
//                {
//                    Type interfaceType = Type.GetType(dependency.InterfaceName);
//                    if (dependency.InterfaceName == dependency.ClassName)
//                    {
//                        if (dependency.IsSingleton)
//                        {
//                            kernel.Bind(interfaceType).ToSelf().InSingletonScope();
//                        }
//                        else
//                        {
//                            kernel.Bind(interfaceType).ToSelf();
//                        }
//                    }
//                    else
//                    {
//                        Type resolvedByType = Type.GetType(dependency.ClassName);
//                        if (dependency.HasautomationToolParameter)
//                        {
//                            if (dependency.IsSingleton)
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType)
//                                    .InSingletonScope()
//                                    .Named(dependency.automationTool);
//                            }
//                            else
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType)
//                                    .Named(dependency.automationTool);
//                            }
//                        }
//                        else
//                        {
//                            if (dependency.IsSingleton)
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType)
//                                    .InSingletonScope();
//                            }
//                            else
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType);
//                            }
//                        }
//                   }
//                    
//                }
//                catch (ArgumentNullException e)
//                {
//                    throw new DependencyConfigurationException(
//                        string.Format(
//                            "No proper type name provided in dependency configuration node. Node description:{0}{1}{2}",
//                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
//                        e);
//                }
//                catch (FileLoadException e)
//                {
//                    throw new DependencyConfigurationException(
//                        string.Format(
//                            "Failed to load the assembly described in the node (or one of its dependencies). Node description:{0}{1}{2}",
//                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
//                        e);
//                }
//                catch (BadImageFormatException e)
//                {
//                    throw new DependencyConfigurationException(
//                        string.Format(
//                            "The assembly described in the node (or one of its dependencies) is invalid. Node description:{0}{1}{2}",
//                            dependency.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
//                        e);
//                }
                #endregion

                new DependencyParser(dependency).ParseBinding(kernel);
            }
            return kernel;
        }

    }


    //public class AutomationTool
    //{
    //    public const string Selenium = "Selenium";
    //    public const string CodedUI = "CodedUI";
    //    public const string Protractor = "Protractor";
    //}
    
}
