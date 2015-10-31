using System;
using System.Threading;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Selenium.Behaviors
{
    public class SeleniumTextFieldBehavior : ITextFieldBehavior
    {
        public ElementBase Element { get; set; }
        private IWebElement NativeElement
        {
            get
            {
                return ((IWebElement)Element.NativeElement);
            }
        }
        public SeleniumTextFieldBehavior(ElementBase element)
        {
            Element = element;
        }

        public void SetText(string textValue)
        {
            NativeElement.Clear();
            NativeElement.SendKeys(textValue);
//            //TODO: replace with flexible wait until field is populated
//            Thread.Sleep(500);
        }

        public string GetText()
        {
            return Element.GetAttribute("value");
        }

        public void AppendText(string textValue)
        {
            NativeElement.Click();
            NativeElement.SendKeys(Keys.End);
            NativeElement.SendKeys(textValue);
//            //TODO: replace with flexible wait until field is populated
//            Thread.Sleep(500);
        }

        public void Clear()
        {
            NativeElement.Clear();
        }
    }

    
    
}




