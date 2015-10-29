using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;
using ProofOfConcept.Selenium.Behaviors;

//namespace ProofOfConcept.Selenium
//{
    //public interface INativeElementAspectsHandler
    //{
    //    bool Exists();
    //    bool Equals(IElement element);
    //    bool Displayed();
    //    dynamic WebElement { get; set; }
    //}


    
    //public class ElementBase : IElement
    //{
    //    private INativeElementAspectsHandler nativeElementAspectsHandler =
    //        DependencyManager.Kernel.Get<INativeElementAspectsHandler>(DependencyManager.Tool);
    //    public bool Exists()
    //    {
    //        return nativeElementAspectsHandler.Exists();
    //    }
    //}

    //public class SeleniumNativeAspectsHandler : INativeElementAspectsHandler
    //{
    //    private IWebElement _webElement;
    //    public bool Exists()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Equals(IElement element)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Displayed()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public dynamic WebElement
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }
    //}

    //public class SomeControl : ElementBase, ITextEditable
    //{
    //    private readonly ITextBehaviour _textBehaviour;
        
    //    public SomeControl(ITextBehaviour textBehaviour)
    //    {
    //        _textBehaviour = textBehaviour;
    //    }
    //    public void SetText(string textValue)
    //    {
    //        _textBehaviour.SetText(textValue);
    //    }

    //    public void AppendText(string textValue)
    //    {
    //         _textBehaviour.AppendText(textValue);
    //    }

    //    public void Clear()
    //    {
    //        _textBehaviour.Clear();
    //    }

    //    bool Exists()
    //    {
    //        return _smth.Exists();
    //    }

    //}


//    public class SeleniumElement : IElement, INativeElementAspectsHandler
//    {
//        public OpenQA.Selenium.IWebElement WebElement
//        {
//            get
//            {
//                if (_webElement == null || !Exists || IsNewLookupAlwaysRequired )
//                {
//                    if (SearchConfiguration == null)
//                    {
//                        throw new Exception("No element, no search criteria");
//                    }
//                    _webElement = ((SeleniumElementFinder)SearchConfiguration).GetNativeElement();
//                }
//                return _webElement;
//            }
//            set { _webElement = value; }
//        }

//        public IElementSearchConfiguration SearchConfiguration { get; set; }

//        private OpenQA.Selenium.IWebElement _webElement;

//        private bool IsNewLookupAlwaysRequired
//        {
//            get
//            {
//                return SearchConfiguration != null && !SearchConfiguration.IsCachingAllowed;
//            }
//        }
        
//        public bool Exists
//        {
//            get
//            {
//                bool exists = false;
//                if (WebElement != null)
//                {
//                    try
//                    {
//                        WebElement.GetAttribute("innerHTML");
//                        exists = true;
//                    }
//                    catch (StaleElementReferenceException) {}
//                }
//                return exists;
//            }
//        }

//        public bool Equals(IElement element)
//        {
//            return DependencyManager.Kernel.Get<INativeElementAspectsHandler>().Equals(element);

//            if (element.GetType().IsAssignableFrom(typeof (SeleniumElement)))
//            {
//                return this.WebElement.IsEqualTo(((SeleniumElement) element).WebElement);
//            }
//            return false;
//        }

//        public bool Displayed
//        {
//            get { return WebElement.Displayed; }
//        }

//        public bool MatchesFilter(FilterBy filterBy)
//        {
//            return filterBy.Check(this);
//        }

//        public bool MatchesAllFilters(params FilterBy[] filtersBy)
//        {
//            return filtersBy.All(searchFilter => searchFilter.Check(this));
//        }

//        public string GetAttribute(string attributeName)
//        {
//            return WebElement.GetAttribute(attributeName);
//        }

//        public IEnumerable<IElement> GetChildren()
//        {
//            FindBy childrenLocator = new FindBy(How.Xpath, "./*");
//            IElementSearchConfiguration childrenSearchConfiguration = new SeleniumElementFinder(childrenLocator, new SeleniumLocatorTransformer());
//            return childrenSearchConfiguration.FindAll();

//        }

//        public void Click()
//        {
//            WebElement.Click();
//        }

//    }

    

//    public class SeleniumElementState : INativeElementHandler
//    {
//        public bool Displayed
//        {
//            get { throw new System.NotImplementedException(); }
//        }

//        public bool Exists
//        {
//            get { throw new System.NotImplementedException(); }
//        }

//        public bool Equals(IElement element)
//        {
//            throw new System.NotImplementedException();
//        }

//        public string GetAttribute(string attributeName)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<IElement> GetChildren()
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}


