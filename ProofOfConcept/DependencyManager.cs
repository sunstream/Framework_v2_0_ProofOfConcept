using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;

namespace ProofOfConcept
{
    public static class DependencyManager
    {
        private static readonly ThreadLocal<IKernel> _activeKernel = new ThreadLocal<IKernel>();
        public static IKernel ActiveKernel
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

        //private static IDictionary<ToolFamily, IKernel> _bindingsByTool = new Dictionary<ToolFamily, IKernel>(); 

        
        //static DependencyManager()
        static IKernel InitKernel()
        {
            DependencyConfiguration configuration = (DependencyConfiguration) System.Configuration.ConfigurationManager.GetSection(DependencyConfiguration.SectionName);
            IKernel Kernel = new StandardKernel();
            foreach (DependencyElement dependency in configuration.Dependencies)
            {
                #region comment
                //if tool-independent:
                //resolve type by interface
                //put regular binding
                
                //check if interface or class are generic types: use typeof then.

                //if tool-dependent
                //put named binding
                

                //Kernel.Bind<NavigationService>().To<NavigationService>();

                //Kernel.Bind<ILocatorTransformer<>>

                //Kernel.Bind(typeof(ILocatorTransformer<>)).To(typeof(SeleniumLocatorTransformer));

                //try
                //{
                #endregion
                Type interfaceType = Type.GetType(dependency.InterfaceName);
                Type resolvedByType = Type.GetType(dependency.ClassName);
                //} catch ()
                if (dependency.HasToolFamilyParameter)
                {
                    Kernel.Bind(interfaceType).To(resolvedByType).Named(dependency.ToolFamily);
                }
                else
                {
                    Kernel.Bind(interfaceType).To(resolvedByType);
                }
                //ActiveKernel.

            }
            return Kernel;
        }
        

    }

    public enum ToolFamily
    {
        Selenium,
        CodedUI,
        Protractor
    }
}
