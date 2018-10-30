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
            string chromeDriver, firefoxDriver, edgeDriver, ieDriver, screenshotPath;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var mainPathWindows = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

                chromeDriver = mainPathWindows + ConfigurationManager.AppSetting["Selenium:PathDriverChrome"];
                edgeDriver = mainPathWindows + ConfigurationManager.AppSetting["Selenium:PathDriverEdge"];
                firefoxDriver = mainPathWindows + Path.GetFullPath(ConfigurationManager.AppSetting["Selenium:PathDriverFirefox"]);
                ieDriver = mainPathWindows + ConfigurationManager.AppSetting["Selenium:PathDriverIE"];

                screenshotPath = mainPathWindows + ConfigurationManager.AppSetting["Selenium:PathToScreenshots"];
            } else {
                var mainPathLinux = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));

                chromeDriver = mainPathLinux + ConfigurationManager.AppSetting["Selenium:PathDriverChrome"].Replace("\\", "/");
                edgeDriver = mainPathLinux + ConfigurationManager.AppSetting["Selenium:PathDriverEdge"].Replace("\\", "/");
                firefoxDriver = mainPathLinux + ConfigurationManager.AppSetting["Selenium:PathDriverFirefox"].Replace("\\", "/");
                ieDriver = mainPathLinux + ConfigurationManager.AppSetting["Selenium:PathDriverIE"].Replace("\\", "/");

                screenshotPath = mainPathLinux + ConfigurationManager.AppSetting["Selenium:PathToScreenshots"].Replace("\\", "/");
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
