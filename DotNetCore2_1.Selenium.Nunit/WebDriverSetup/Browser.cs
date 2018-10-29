using DotNetCore2_1.Selenium.Nunit.GetSettings;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DotNetCore2_1.Selenium.Nunit.WebDriverSetup
{
    public class BrowserOptions
    {
        
        public static void SelectBrowser(Browser browser)
        {
            var mainPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            string chromeDriver = mainPath + ConfigurationManager.AppSetting["Selenium:PathDriverChrome"];
            string firefoxDriver = mainPath + Path.GetFullPath(ConfigurationManager.AppSetting["Selenium:PathDriverFirefox"]);
            string edgeDriver = mainPath + ConfigurationManager.AppSetting["Selenium:PathDriverEdge"];
            string ieDriver = mainPath + ConfigurationManager.AppSetting["Selenium:PathDriverIE"];

            string screenshotPath = mainPath + ConfigurationManager.AppSetting["Selenium:PathToScreenshots"];

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //string[] chromeDriverPath = chromeDriver.Split(Path.AltDirectorySeparatorChar);
                //chromeDriver = chromeDriverPath[0];
                //string[] edgeDriverPath = edgeDriver.Split(Path.AltDirectorySeparatorChar);
                //edgeDriver = edgeDriverPath[0];
                //string[] firefoxDriverPath = firefoxDriver.Split(Path.AltDirectorySeparatorChar);
                //firefoxDriver = firefoxDriverPath[0];
                //string[] ieDriverPath = ieDriver.Split(Path.AltDirectorySeparatorChar);
                //ieDriver = ieDriverPath[0];
            }

            switch (browser)
            {
                case Browser.Chrome:
                    ChromeOptions optionChrome = new ChromeOptions();
                    // optionChrome.AddArgument("--headless");
                    var firingDriver = new EventFiringWebDriver(new ChromeDriver(chromeDriver, optionChrome));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.Edge:
                    firingDriver = new EventFiringWebDriver(new EdgeDriver(edgeDriver));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.Firefox:
                    FirefoxOptions optionFirefox = new FirefoxOptions();
                    // optionFirefox.AddArgument("--headless");
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(firefoxDriver);
                    firingDriver = new EventFiringWebDriver(new FirefoxDriver(service, optionFirefox));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
                case Browser.IE:
                    Environment.SetEnvironmentVariable("webDriver.ie.Driver", ieDriver);
                    firingDriver = new EventFiringWebDriver(new InternetExplorerDriver(ieDriver));
                    firingDriver.ExceptionThrown += TakeScreenshotOnException;
                    PropertiesCollection.Driver = firingDriver;
                    break;
            }

            void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
            {
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy-hh_mm_ss");
                PropertiesCollection.Driver.TakeScreenshot().SaveAsFile(screenshotPath +
                    "\\Exception-" + timestamp + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
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
