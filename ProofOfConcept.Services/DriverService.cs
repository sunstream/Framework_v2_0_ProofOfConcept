using System;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Services
{
    public class DriverService
    {
        //TODO: driver must be private. Provide a list of methods to manipulate driver instead.
        public readonly IDriverDecorator Driver;

        public DriverService(IDriverDecorator driver)
        {
            Driver = driver;
        }
        public TDriverType GetDriver<TDriverType>() where TDriverType : class
        {
            return Driver.GetDriver<TDriverType>();
        }

        public void ShareDriverBetweenTests()
        {
            const bool driverIsSingleton = true;
            if (Driver.DriverCreated && Driver.ApplicationStarted) Driver.Stop();
            SwitchDriverMode(driverIsSingleton);
        }

        public void CloseDriverBetweenTests()
        {
            const bool driverIsSingleton = false;
            SwitchDriverMode(driverIsSingleton);
        }

        private void SwitchDriverMode(bool isSingleton)
        {
            Type concreteDriverType = Driver.GetType();
            DependencyManager.Kernel.Unbind(concreteDriverType);

            DependencyConfiguration config = DependencyManager.GetDependencyConfiguration();
            bool driverBindingUpdated = false;
            foreach (DependencyElement dependency in config.Dependencies)
            {
                DependencyParser parser = new DependencyParser(dependency);
                if (parser.ResolvingType == concreteDriverType)
                {
                    parser.IsSingleton = isSingleton;
                    parser.ParseBinding(DependencyManager.Kernel);
                    driverBindingUpdated = true;
                    break;
                }
            }
            if (!driverBindingUpdated)
            {
                throw new DependencyConfigurationException("Failed to make Driver instance shared between tests in the same suite: no proper Driver mapping was found in App.config file.");
            }
        }

    }
}