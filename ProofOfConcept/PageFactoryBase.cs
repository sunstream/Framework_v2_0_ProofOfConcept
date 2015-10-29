using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using System.Reflection;

namespace ProofOfConcept
{
    public interface IPageFactory
    {
        TPageType Create<TPageType>(IElement containerElement = null) where TPageType : IContainer, new();
    }

    public class PageFactoryBase : IPageFactory

    {
        //LoginPage -> T == LoginPage
        //NativeElementType: Selenium -> IWebElement
        //IElementSearchConfiguration<IWebElement>
        public TPageType Create<TPageType>(IElement containerElement = null) where TPageType : IContainer, new()
        {
            TPageType result = new TPageType();
            Type requestedPageType = typeof(TPageType);

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
                   //3.2. Get attributes: findBy (mandatory), filters/SearchConfiguration's (optional)
                    var attributes = pageMember.GetCustomAttributes(false);
                    //read element type
                    //read element technology

                    FindByAttribute locatorAttribute = 
                        attributes.FirstOrDefault(item => item.GetType() == typeof (FindByAttribute)) as FindByAttribute;
                    if (locatorAttribute != null)
                    {
                        FindBy locator = locatorAttribute.FindBy;
                        IList<FilterByAttribute> filterAttributes =
                            attributes.Where(item => item.GetType() == typeof (FilterByAttribute))
                                .Cast<FilterByAttribute>()
                                .ToList();
                        FilterBy[] filters = filterAttributes.Select(filterAttribute => filterAttribute.FilterBy).ToArray();
                        IElement parentElement = containerElement;

                        IElementSearchConfiguration searchConfiguration = DependencyManager.Kernel.Get<IElementSearchConfiguration>(DependencyManager.Tool.ToString());
                        searchConfiguration.FindBy(locator).FilterBy(filters).From(parentElement);

                        IElement elementInstance = DependencyManager.Kernel.Get<IElement>(DependencyManager.Tool.ToString());
                        elementInstance.SearchConfiguration = searchConfiguration;

                        Type resultingElementType = pageMember.DeclaringType;


                    }


                    //3.3. Specify a parent element if a page has one.
                    //3.4. Setup a lazy proxy to the page element using attributes data. (see SeleniumHQ / PageFactory for examples; InvocationHandler in Java).
                    //3.5. Cast the resulting object to requested type.
                }
            }
            return result;
        }

        //public T GetContainer<T>(IElement containerElement) where T : IContainer, new()
        //{
        //    return Create<T>(containerElement);
        //}
        
        #region Private Methods

        private IContainer InitializeComplexControl(MemberInfo pageMember)
        {
            IElement containerElementForComplexControl = GetContainerElementFromAttributes(pageMember);
            Type complexControlType = pageMember.DeclaringType;
            MethodInfo getPageMethod = (typeof(PageFactoryBase)).GetMethod("Create").MakeGenericMethod(complexControlType);
            object[] getPageMethodArguments = { containerElementForComplexControl };
            IContainer complexControlInstance = (IContainer)getPageMethod.Invoke(null, getPageMethodArguments);

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


//public interface ISearchConfiguration<T>
//{
//    T Get();
//}

//public interface IWebElement
//{
//    string Text { get; }
//}

//public interface ICodedUIElement
//{
//    string Text { get; }
//}

//public class SeleniumSearchConfiguration : ISearchConfiguration<IWebElement>
//{

//    public IWebElement Get()
//    {
//        throw new NotImplementedException();
//    }
//}

//public class CodedUISearchConfigutration : ISearchConfiguration<ICodedUIElement>
//{

//    public ICodedUIElement Get()
//    {
//        throw new NotImplementedException();
//    }
//}

//class X
//{

//    public void Method()
//    {
//        ISearchConfiguration<IWebElement> x;
//        x = new SeleniumSearchConfiguration();
//    }

//}