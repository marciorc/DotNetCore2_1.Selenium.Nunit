using DotNetCore2_1.Selenium.Nunit.GetSettings;
using NUnit.Framework;
using System;
using System.Linq;

namespace DotNetCore2_1.Selenium.Nunit.WebDriverSetup
{
    public class Setup
    {
        // URL configurada no appsettings.json
        readonly string url = ConfigurationManager.AppSetting["Selenium:Url"];
        
        // Timeout configurado no appsettings.json
        readonly int timeout = Convert.ToInt32(ConfigurationManager.AppSetting["Selenium:Timeout"]);

        [SetUp]
        public void Initialize()
        {
            BrowserOptions.SelectBrowser(Browser.Chrome);
            PropertiesCollection.Driver.Manage().Cookies.DeleteAllCookies();
            PropertiesCollection.Driver.Navigate().GoToUrl(url);
            //PropertiesCollection.Drive
            PropertiesCollection.DefaultTimeout = timeout;
            PropertiesCollection.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout);
            PropertiesCollection.Driver.SwitchTo().Window(PropertiesCollection.Driver.WindowHandles.Last());
        }

        [TearDown]
        public static void CleanUp()
        {
            PropertiesCollection.Driver.Quit();
            PropertiesCollection.Driver = null;
        }
    }
}
