using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProofOfConcept
{
    public class ElementFactory : IPageFactory
    {
        public IDriverDecorator DriverDecorator { get; set; }
        private Type _requestedPageType;

        public T GetPage<T>(IElement containerElement = null) where T : IContainer, new()
        {
            _requestedPageType = typeof (T);
            T resultingContainer = new T();
            
            IEnumerable<MemberInfo> pageMembers = ExtractPageMembers();
            foreach (MemberInfo pageMember in pageMembers)
            {
                if (IsAComplexControl(pageMember))
                {

                    // get parent element locator attribute
                    //if exists, get a parent element
                    //IElement containerElementForComplexControl = GetContainerFromAttributes(pageMember);
                    IElement containerElementForComplexControl = null;
                    Type complexControlType = pageMember.DeclaringType;
                    MethodInfo getPageMethod = this.GetType().GetMethod("GetPage").MakeGenericMethod(complexControlType);
                    object[] getPageMethodArguments = { containerElementForComplexControl };

                    IContainer complexControlInstance = (IContainer)getPageMethod.Invoke(this, getPageMethodArguments);

                    switch (pageMember.MemberType)
                    {
                        case MemberTypes.Field: ((FieldInfo)pageMember).SetValue(resultingContainer, complexControlInstance); break;
                        case MemberTypes.Property: ((PropertyInfo)pageMember).SetValue(resultingContainer, complexControlInstance); break;
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
            return resultingContainer;
        }

        public T GetContainer<T>(IElement containerElement) where T : IContainer, new()
        {
            return GetPage<T>(containerElement);
        }
        
        #region Private Methods
        private IEnumerable<MemberInfo> ExtractPageMembers()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            IEnumerable<MemberInfo> pageMembers =
                from pageMember in _requestedPageType.GetMembers(flags)
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
                    String.Format("Failed to extract type information for page member {0} in page {1}", pageMember.Name,
                        _requestedPageType.Name), e);
            }
        }
        #endregion
    }
}
