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

        private static readonly ThreadLocal<ToolFamily> ActiveTool = new ThreadLocal<ToolFamily>();

        public static ToolFamily Tool
        {
            get
            {
                if (!ActiveTool.IsValueCreated)
                {
                    string defaultToolName = ConfigurationManager.AppSettings["toolFamily"];
                    try
                    {
                        ActiveTool.Value = (ToolFamily)Enum.Parse(typeof(ToolFamily), defaultToolName);
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
//                        if (dependency.HasToolFamilyParameter)
//                        {
//                            if (dependency.IsSingleton)
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType)
//                                    .InSingletonScope()
//                                    .Named(dependency.ToolFamily);
//                            }
//                            else
//                            {
//                                kernel.Bind(interfaceType)
//                                    .To(resolvedByType)
//                                    .Named(dependency.ToolFamily);
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

    public class DependencyParser
    {
        private readonly DependencyElement _element;
        public bool IsSingleton;
        public bool IsSelfBound;
        public bool HasToolFamily;

        public readonly Type BaseType;
        public readonly Type ResolvingType;

        public DependencyParser(DependencyElement element)
        {
            this._element = element;

            IsSingleton = element.IsSingleton;
            IsSelfBound = element.InterfaceName == element.ClassName;
            HasToolFamily = element.HasToolFamilyParameter;

            BaseType = GetType(element.InterfaceName);
            ResolvingType = GetType(element.ClassName);
        }

        public void ParseBinding(IKernel kernel)
        {
            var initialState = kernel.Bind(BaseType);
            var boundState = IsSelfBound ? initialState.ToSelf() : initialState.To(ResolvingType);
            if (HasToolFamily)
            {
                boundState.Named(_element.ToolFamily);
            }
            if (IsSingleton)
            {
                boundState.InSingletonScope();
            }
        }

        Type GetType(string name)
        {
            Type result;
            try
            {
                result = Type.GetType(name);
            }
            catch (ArgumentNullException e)
            {
                throw new DependencyConfigurationException(
                    string.Format(
                        "No proper type name provided in dependency configuration node. Node description:{0}{1}{2}",
                        _element.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                    e);
            }
            catch (FileLoadException e)
            {
                throw new DependencyConfigurationException(
                    string.Format(
                        "Failed to load the assembly described in the node (or one of its dependencies). Node description:{0}{1}{2}",
                        _element.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                    e);
            }
            catch (BadImageFormatException e)
            {
                throw new DependencyConfigurationException(
                    string.Format(
                        "The assembly described in the node (or one of its dependencies) is invalid. Node description:{0}{1}{2}",
                        _element.Describe(), Environment.NewLine, DependencyElement.ProperNodeFormat),
                    e);
            }
            return result;
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
