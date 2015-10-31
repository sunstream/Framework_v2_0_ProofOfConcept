using System;
using System.Diagnostics;
using System.Threading;
using ProofOfConcept.Configuration;

namespace ProofOfConcept.Services
{
    public class NavigationService : DriverService
    {
        public NavigationService(IDriverDecorator driver)
            : base(driver)
        {
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
                if (stopwatch.Elapsed >= SettingsService.TimeoutSettings.PageTimeout) break;

            }
            return urlValid;
        }

        public void Stop()
        {
            Driver.Stop();
        }

        
    }
}
