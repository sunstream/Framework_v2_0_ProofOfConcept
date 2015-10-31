using System;
using System.IO;
using Ninject;

namespace ProofOfConcept.Configuration
{
    public class DependencyParser
    {
        private readonly DependencyElement _element;
        public bool IsSingleton;
        public bool IsSelfBound;
        public bool HasautomationTool;

        public readonly Type BaseType;
        public readonly Type ResolvingType;

        public DependencyParser(DependencyElement element)
        {
            this._element = element;

            IsSingleton = element.IsSingleton;
            IsSelfBound = element.InterfaceName == element.ClassName;
            HasautomationTool = element.HasautomationToolParameter;

            BaseType = GetType(element.InterfaceName);
            ResolvingType = GetType(element.ClassName);
        }

        public void ParseBinding(IKernel kernel)
        {
            var initialState = kernel.Bind(BaseType);
            var boundState = IsSelfBound ? initialState.ToSelf() : initialState.To(ResolvingType);
            if (HasautomationTool)
            {
                boundState.Named(_element.automationTool);
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
}