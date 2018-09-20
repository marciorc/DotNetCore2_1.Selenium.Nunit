using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace DotNetCore2_1.Selenium.Nunit.Utils
{
    static class WaitUntil
    {
        public static IWebElement ElementVisible(By elementLocator)
        {
            try
            {
                var wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Elemento: '" + elementLocator + "' não foi encontrado.");
                throw;
            }
        }

        public static bool ElementNotVisible(By elementLocator)
        {
            try
            {
                var wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout));
                Thread.Sleep(500);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Elemento: '" + elementLocator + "' permanece visivel.");
                throw;
            }
        }

        public static void ElementDesappear(By elementLocator)
        {
            ElementVisible(elementLocator);
            ElementNotVisible(elementLocator);
        }

        public static IAlert AlertIsVisible()
        {
            try
            {
                var wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Alert não foi encontrado");
                throw;
            }
        }

        public static class WebDriverExtensions
        {
            public static IWebElement FindElement(By by)
            {
                if (PropertiesCollection.DefaultTimeout > 0)
                {
                    var wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout));
                    return wait.Until(drv => drv.FindElement(by));
                }
                return PropertiesCollection.Driver.FindElement(by);
            }
        }
    }
}
