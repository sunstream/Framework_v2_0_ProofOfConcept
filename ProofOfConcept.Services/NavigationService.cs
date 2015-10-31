using System;
using System.Diagnostics;
using System.Threading;

namespace ProofOfConcept.Services
{
    public class NavigationService : DriverService
    {
        public NavigationService(IDriverDecorator driver)
            : base(driver)
        {
        }

        public TDriverType GetDriver<TDriverType>() where TDriverType : class
        {
            return Driver.GetDriver<TDriverType>();
        }

        public void NavigateTo(string url)
        {
            Driver.NavigateTo(url);
        }

        public string GetCurrentUrl()
        {
            return Driver.GetCurrentUrl();
        }

        public bool PageUrlEquals(string expectedUrl)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool urlValid = false;
            while (!urlValid)
            {
                urlValid = String.Compare(Driver.GetCurrentUrl(), expectedUrl, StringComparison.InvariantCultureIgnoreCase) == 0;
                if (stopwatch.Elapsed >= Settings.TimeoutSettings.PageTimeout) break;

            }
            return urlValid;
        }

        public void Stop()
        {
            Driver.Stop();
        }

        
    }
}
