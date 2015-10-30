using System;
using System.Threading;

namespace ProofOfConcept.Services
{
    public class NavigationService
    {
        private readonly IDriverDecorator _driver;

        public NavigationService(IDriverDecorator driverDecorator)
        {
            this._driver = driverDecorator;
        }

        public void NavigateTo(string url)
        {
            _driver.NavigateTo(url);
        }

        public void VerifyPageUrl(string expectedUrl)
        {
            //TODO: REMOVE TEMPORARY STUB!
            Thread.Sleep(3000);
            if (String.Compare(_driver.GetCurrentUrl(), expectedUrl, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                throw new Exception("Invalid URL (custom assert, just for demo purposes).");
            }
        }
    }
}
