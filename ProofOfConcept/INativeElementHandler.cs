using System.Collections.Generic;

namespace ProofOfConcept
{
    public interface INativeElementHandler
    {
        IElementSearchConfiguration SearchConfiguration { get; set; }

        dynamic NativeElement { get; set; }

        bool Displayed { get; }

        bool Exists { get; }

        bool Equals(IElement element);

        string GetAttribute(string attributeName);

        IEnumerable<IElement> GetChildren();

        void Click();
    }
}