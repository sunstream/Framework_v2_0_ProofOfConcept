using System;
using System.Threading;

namespace ProofOfConcept.Services
{
    public class NavigationService
    {
        //TODO: driver must be private. Provide a list of methods to manipulate driver instead.
        public readonly IDriverDecorator Driver;

        public NavigationService(IDriverDecorator driverDecorator)
        {
            this.Driver = driverDecorator;
        }

        public TDriverType GetDriver<TDriverType>() where TDriverType : class
        {
            return Driver.GetDriver<TDriverType>();
        }

        public void NavigateTo(string url)
        {
            Driver.NavigateTo(url);
        }

        public void VerifyPageUrl(string expectedUrl)
        {
            //TODO: REMOVE TEMPORARY STUB!
            Thread.Sleep(3000);
            if (String.Compare(Driver.GetCurrentUrl(), expectedUrl, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                throw new Exception("Invalid URL (custom assert, just for demo purposes).");
            }
        }
    }
}
