using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using System.Reflection;

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
                    IElement elementInstance = GetElementByAttributes(pageMember, commonContainerElement);
                    switch (pageMember.MemberType)
                        {
                            case MemberTypes.Field: ((FieldInfo)pageMember).SetValue(requestedPage, elementInstance); break;
                            case MemberTypes.Property: ((PropertyInfo)pageMember).SetValue(requestedPage, elementInstance); break;
                        }
                    }
                }
            return requestedPage;
        }

        #region Private Methods

        private IElement GetElementByAttributes(MemberInfo pageMember, IElement parentElement)
        {
            var pageMemberAttributes = pageMember.GetCustomAttributes(false);
            FindBy locator = GetLocator(pageMemberAttributes);
            if (locator != null)
            {
                FilterBy[] filters = GetFilters(pageMemberAttributes);
                IElementSearchConfiguration searchConfiguration =
                    DependencyManager.Kernel.Get<IElementSearchConfiguration>(DependencyManager.Tool.ToString());
                searchConfiguration.FindBy(locator).FilterBy(filters).From(parentElement);

                IElement elementInstance = DependencyManager.Kernel.Get<IElement>(DependencyManager.Tool.ToString());
                elementInstance.SearchConfiguration = searchConfiguration;
                return elementInstance;
            }
            return null;
        }

        private FindBy GetLocator(object[] pageMemberAttributes)
        {
            FindByAttribute locatorAttribute =
                        pageMemberAttributes.FirstOrDefault(item => item.GetType() == typeof(FindByAttribute)) as FindByAttribute;
            return locatorAttribute?.FindBy;
        }

        private FilterBy[] GetFilters(object[] pageMemberAttributes)
        {
            IList<FilterByAttribute> filterAttributes =
                            pageMemberAttributes.Where(item => item.GetType() == typeof(FilterByAttribute)).Cast<FilterByAttribute>().ToList();
            FilterBy[] filters = filterAttributes.Select(filterAttribute => filterAttribute.FilterBy).ToArray();
            return filters;
        }

        private IContainer InitializeComplexControl(MemberInfo pageMember, IElement commonContainerElement)
        {
            IElement containerElementForComplexControl = GetElementByAttributes(pageMember, commonContainerElement);
            Type complexControlType = pageMember.DeclaringType;
            MethodInfo getPageMethod = (typeof(PageFactoryBase)).GetMethod("Create").MakeGenericMethod(complexControlType);
            object[] getPageMethodArguments = { containerElementForComplexControl };
            IContainer complexControlInstance = (IContainer)getPageMethod.Invoke(null, getPageMethodArguments);

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
            return IsOfType(pageMember, typeof (IElement));
        }

        private bool IsAComplexControl(MemberInfo pageMember)
        {
            return IsOfType(pageMember, typeof (IContainer));
        }

        private bool IsOfType(MemberInfo pageMember, Type expectedType)
        {
            try
            {
                return pageMember.DeclaringType.IsAssignableFrom(expectedType);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(
                    string.Format("Failed to extract type information for page member {0}", pageMember.Name), e);
            }
        }
        #endregion
    }
}