using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProofOfConcept
{
    public class ElementFactory : IPageFactory
    {
        public IDriverDecorator DriverDecorator { get; set; }
        //private Type _requestedPageType;

        public T GetPage<T>(IElement containerElement = null) where T : IContainer, new()
        {
            T result = new T();

            Type requestedPageType = typeof(T);
            IEnumerable<MemberInfo> pageMembers = ExtractPageMembers(requestedPageType);

            foreach (MemberInfo pageMember in pageMembers)
            {
                if (IsAComplexControl(pageMember))
                {
                    IContainer complexControlInstance = InitializeComplexControl(pageMember);
                    switch (pageMember.MemberType)
                    {
                        case MemberTypes.Field: ((FieldInfo)pageMember).SetValue(result, complexControlInstance); break;
                        case MemberTypes.Property: ((PropertyInfo)pageMember).SetValue(result, complexControlInstance); break;
                    }

                }
                if (IsAnElement(pageMember))
                {

                   //3.2. Get attributes: locator (mandatory), filters/SearchConfiguration's (optional)
	               //3.3. Specify a parent element if a page has one.
	               //3.4. Setup a lazy proxy to the page element using attributes data. (see SeleniumHQ / PageFactory for examples; InvocationHandler in Java).
	               //3.5. Cast the resulting object to requested type.
                }
            }
            return result;
        }

        public T GetContainer<T>(IElement containerElement) where T : IContainer, new()
        {
            return GetPage<T>(containerElement);
        }
        
        #region Private Methods

        private IContainer InitializeComplexControl(MemberInfo pageMember)
        {
            IElement containerElementForComplexControl = GetContainerElementFromAttributes(pageMember);
            Type complexControlType = pageMember.DeclaringType;
            MethodInfo getPageMethod = GetType().GetMethod("GetPage").MakeGenericMethod(complexControlType);
            object[] getPageMethodArguments = { containerElementForComplexControl };
            IContainer complexControlInstance = (IContainer)getPageMethod.Invoke(this, getPageMethodArguments);

            return complexControlInstance;
        }

        private IElement GetContainerElementFromAttributes(MemberInfo pageMember)
        {
            return null;
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
                    String.Format("Failed to extract type information for page member {0}", pageMember.Name), e);
            }
        }
        #endregion
    }
}
