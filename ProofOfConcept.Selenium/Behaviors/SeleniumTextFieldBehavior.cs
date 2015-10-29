using System;
using Ninject;
using OpenQA.Selenium;
using ProofOfConcept.Behaviors;

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
        }

        public void Clear()
        {
            NativeElement.Clear();
        }
    }

    
    
}




