using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ProofOfConcept.Configuration;
using ProofOfConcept.Services;

namespace ProofOfConcept.ComponentTests.TestTemplates
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("ProofOfConcept.Selenium.dll")]
    public class UiTestDriverSharedBetweenTests
    {
        [ClassInitialize]
        public static void ShareDriverBetweenSessions(TestContext context)
        {
            NavigationService navigationService = DependencyManager.Kernel.Get<NavigationService>();
            navigationService.ShareDriverBetweenTests();
        }

        [ClassCleanup]
        public static void KillDriverIfLaunched(TestContext context)
        {
            DependencyManager.Kernel.Get<NavigationService>().Stop();
        }
    }
}