using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace DotNetCore2_1.Selenium.Nunit.Utils
{
    static class WaitUntil
    {
        public static void AjaxFinished()
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout))
            {
                PollingInterval = TimeSpan.FromMilliseconds(PropertiesCollection.PollingInterval)
            };
            IJavaScriptExecutor jsScript = PropertiesCollection.Driver as IJavaScriptExecutor;
            wait.Until(driver =>
            {
                bool isAjaxFinished = (bool)jsScript.ExecuteScript("return jQuery.active == 0");
                Thread.Sleep(100);
                return isAjaxFinished;
            });
        }
   
        public static IWebElement ElementVisible(By elementLocator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(PropertiesCollection.PollingInterval)
                };
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
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(PropertiesCollection.PollingInterval)
                };
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
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(PropertiesCollection.PollingInterval)
                };
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Alert não foi encontrado");
                throw;
            }
        }
    }

    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(By by)
        {
            if (PropertiesCollection.DefaultTimeout > 0)
            {
                WebDriverWait wait = new WebDriverWait(PropertiesCollection.Driver, TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(PropertiesCollection.PollingInterval)
                };
                return wait.Until(drv => drv.FindElement(by));
            }
            return PropertiesCollection.Driver.FindElement(by);
        }
    }
}
