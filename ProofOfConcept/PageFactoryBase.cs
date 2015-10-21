using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ninject;
using System.Reflection;
using Newtonsoft.Json;
using ProofOfConcept.Utils;

namespace ProofOfConcept
{
    public interface IPageFactory
    {
        TPageType Create<TPageType>(IElement commonContainerElement = null) where TPageType : IContainer, new();
    }

    public class PageFactoryBase : IPageFactory
    {

        public TPageType Create<TPageType>(IElement commonContainerElement = null) where TPageType : IContainer, new()
        {
            TPageType requestedPage = new TPageType();
            Type requestedPageType = typeof(TPageType);

            IEnumerable<MemberInfo> pageMembers = ExtractPageMembers(requestedPageType);
            foreach (MemberInfo pageMember in pageMembers)
            {
                if (IsAComplexControl(pageMember))
                {
                    IContainer complexControlInstance = InitializeComplexControl(pageMember, commonContainerElement);
                    switch (pageMember.MemberType)
                    {
                        case MemberTypes.Field: ((FieldInfo)pageMember).SetValue(requestedPage, complexControlInstance); break;
                        case MemberTypes.Property: ((PropertyInfo)pageMember).SetValue(requestedPage, complexControlInstance); break;
                    }

                }
                if (IsAnElement(pageMember))
                {
                    IElementSearchConfiguration searchConfiguration = GetSearchConfigurationByAttributes(pageMember,
                        commonContainerElement);
                    Type expectedElementType = pageMember.GetUnderlyingType();
                    var expectedElementInstance = Activator.CreateInstance(expectedElementType);
                    ((IElement)expectedElementInstance).SearchConfiguration = searchConfiguration;

                    switch (pageMember.MemberType)
                    {
                        case MemberTypes.Field:
                            ((FieldInfo)pageMember).SetValue(requestedPage, expectedElementInstance);
                            break;
                        case MemberTypes.Property:
                            ((PropertyInfo)pageMember).SetValue(requestedPage, expectedElementInstance);
                            break;
                    }

                    #region InstantiateElementOfProperType

                    //Type elementType = pageMember.GetUnderlyingType();
                    //Type baseElementType = DependencyManager.Kernel.Get<IElement>().GetType();

                    //var concreteElementTemplate = Activator.CreateInstance(elementType);
                    //var concreteElementInstance = elementInstance.ShallowConvert<>(concreteElementTemplate);

                    //Type elementType = pageMember.GetUnderlyingType();
                    //var serializedParent = JsonConvert.SerializeObject(elementInstance);
                    //var deserializedChild = JsonConvert.DeserializeObject(serializedParent, elementType);

                    //var concreteElementInstance = Activator.CreateInstance(elementType);
                    //elementInstance.ShallowConvert(concreteElementInstance);
                    //((IElement) concreteElementInstance).SearchConfiguration = elementInstance.SearchConfiguration;

                    //switch (pageMember.MemberType)
                    //    {
                    //        case MemberTypes.Field: 
                    //            //elementInstance
                    //            //((FieldInfo)pageMember).SetValue(requestedPage, elementInstance); 
                    //            ((FieldInfo)pageMember).SetValue(requestedPage, deserializedChild);
                    //            break;
                    //        case MemberTypes.Property: ((PropertyInfo)pageMember).SetValue(requestedPage, elementInstance); break;
                    //    }

                    #endregion
                }
                if (IsAnElementsCollection(pageMember))
                {
                    IElementSearchConfiguration searchConfiguration = GetSearchConfigurationByAttributes(pageMember, commonContainerElement);
                    Type expectedElementType = pageMember.GetUnderlyingType().GetGenericTypeDefinition();
                    Type[] typeArguments = pageMember.GetUnderlyingType().GenericTypeArguments;
                    var constructedType = expectedElementType.MakeGenericType(typeArguments);
                    object expectedElementInstance = Activator.CreateInstance(constructedType, searchConfiguration);

                    switch (pageMember.MemberType)
                    {
                        case MemberTypes.Field:
                            ((FieldInfo)pageMember).SetValue(requestedPage, expectedElementInstance);
                            break;
                        case MemberTypes.Property:
                            ((PropertyInfo)pageMember).SetValue(requestedPage, expectedElementInstance);
                            break;
                    }
                }
            }
            return requestedPage;
        }

        #region Private Methods

        private IElementSearchConfiguration GetSearchConfigurationByAttributes(MemberInfo pageMember, IElement parentElement)
        {
            var pageMemberAttributes = pageMember.GetCustomAttributes(false);
            FindBy locator = GetLocator(pageMemberAttributes);
            if (locator != null)
            {
                FilterBy[] filters = GetFilters(pageMemberAttributes);
                IElementSearchConfiguration searchConfiguration =
                    DependencyManager.Kernel.Get<IElementSearchConfiguration>(DependencyManager.Tool.ToString());
                searchConfiguration.FindBy(locator).FilterBy(filters).From(parentElement);
                return searchConfiguration;
            }
            return null;
        }

        private FindBy GetLocator(object[] pageMemberAttributes)
        {
            FindByAttribute locatorAttribute =
                        pageMemberAttributes.FirstOrDefault(item => item.GetType() == typeof(FindByAttribute)) as FindByAttribute;
            return (locatorAttribute == null ? null : locatorAttribute.FindBy);
        }

        private FilterBy[] GetFilters(object[] pageMemberAttributes)
        {
            IList<FilterByAttribute> filterAttributes =
                            pageMemberAttributes.Where(item => item.GetType().IsSubclassOf(typeof(FilterByAttribute))).Cast<FilterByAttribute>().ToList();


            FilterBy[] filters = filterAttributes.Select(filterAttribute => filterAttribute.FilterBy).ToArray();
            return filters;
        }

        private IContainer InitializeComplexControl(MemberInfo pageMember, IElement commonContainerElement)
        {
            //IElement containerElementForComplexControl = GetElementByAttributes(pageMember, commonContainerElement);
            IElementSearchConfiguration searchConfiguration = GetSearchConfigurationByAttributes(pageMember,
                commonContainerElement);
            IElement containerElementForComplexControl = DependencyManager.Kernel.Get<IElement>();
            containerElementForComplexControl.SearchConfiguration = searchConfiguration;

            Type complexControlType = pageMember.GetUnderlyingType();
            MethodInfo getPageMethod = (typeof(PageFactoryBase)).GetMethod("Create").MakeGenericMethod(complexControlType);
            object[] getPageMethodArguments = { containerElementForComplexControl };
            IContainer complexControlInstance = (IContainer)getPageMethod.Invoke(this, getPageMethodArguments);

            return complexControlInstance;
        }


        private IEnumerable<MemberInfo> ExtractPageMembers(Type requestedPageType)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            IEnumerable<MemberInfo> pageMembers =
                from pageMember in requestedPageType.GetMembers(flags)
                where (pageMember.MemberType == MemberTypes.Property || pageMember.MemberType == MemberTypes.Field)
                select pageMember;
            return pageMembers;
        }

        private bool IsAnElement(MemberInfo pageMember)
        {
            return IsOfType(pageMember, typeof(IElement));
        }

        private bool IsAComplexControl(MemberInfo pageMember)
        {
            return IsOfType(pageMember, typeof(IContainer));
        }

        private bool IsAnElementsCollection(MemberInfo pageMember)
        {
            return pageMember.GetUnderlyingType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IElementsCollection<>));
        }

        private bool IsOfType(MemberInfo pageMember, Type expectedType)
        {
            return pageMember.GetUnderlyingType().GetInterfaces().Contains(expectedType);
        }
        #endregion
    }
}
