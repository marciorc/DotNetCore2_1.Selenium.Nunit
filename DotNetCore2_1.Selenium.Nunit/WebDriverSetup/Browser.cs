using DotNetCore2_1.Selenium.Nunit.GetSettings;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;

namespace DotNetCore2_1.Selenium.Nunit.WebDriverSetup
{
    public class BrowserOptions
    {
        
        public static void SelectBrowser(Browser browser)
        {
            string firefoxDriver = ConfigurationManager.AppSetting["Selenium:PathDriverFirefox"];
            string chromeDriver = ConfigurationManager.AppSetting["Selenium:PathDriverChrome"];
            string ieDriver = ConfigurationManager.AppSetting["Selenium:PathDriverIE"];
            string edgeDriver = ConfigurationManager.AppSetting["Selenium:PathDriverEdge"];

            void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
            {
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy-hh_mm_ss");
                var pathString = System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Screenshots");
                PropertiesCollection.Driver.TakeScreenshot().SaveAsFile(Directory.GetCurrentDirectory() + "\\Screenshots" +
                    "\\Exception-" + timestamp + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
            }

            switch (browser)
            {
                case Browser.Firefox:
                    FirefoxOptions optionFirefox = new FirefoxOptions();
                    // optionFirefox.AddArgument("--headless");
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(firefoxDriver);
                    var firingDriver = new EventFiringWebDriver(new FirefoxDriver(service, optionFirefox));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.Chrome:
                    ChromeOptions optionChrome = new ChromeOptions();
                    // optionChrome.AddArgument("--headless");
                    firingDriver = new EventFiringWebDriver(new ChromeDriver(chromeDriver, optionChrome));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.IE:
                    Environment.SetEnvironmentVariable("webDriver.ie.Driver", ieDriver);
                    firingDriver = new EventFiringWebDriver(new InternetExplorerDriver(ieDriver));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.Edge:
                    firingDriver = new EventFiringWebDriver(new EdgeDriver(edgeDriver));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
            }
        }
    }

    public enum Browser
    {
        Firefox,
        Chrome,
        IE,
        Edge
    }
}
