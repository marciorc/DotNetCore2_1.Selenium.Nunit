using DotNetCore2_1.Selenium.Nunit.GetSettings;
using NUnit.Framework;
using System;

namespace DotNetCore2_1.Selenium.Nunit.WebDriverSetup
{
    public class Setup
    {
        // URL configurada no appsettings.json
        readonly string url = ConfigurationManager.AppSetting["Selenium:Url"];
        
        // Timeout configurado no appsettings.json
        readonly int timeout = Convert.ToInt32(ConfigurationManager.AppSetting["Selenium:Timeout"]);

        // PollingInterval configurado no appsettings.json
        readonly int PollingInterval = Convert.ToInt32(ConfigurationManager.AppSetting["Selenium:PollingInterval"]);


        [SetUp]
        public void Initialize()
        {
            // Seta o driver escolhido
            BrowserOptions.SelectBrowser(Browser.Firefox);
            PropertiesCollection.Driver.Manage().Cookies.DeleteAllCookies();

            // Seta para o driver principal as propriedades configuradas no appsettings.json
            PropertiesCollection.DefaultTimeout = timeout;
            PropertiesCollection.PollingInterval = PollingInterval;

            // Inicia a navegação
            PropertiesCollection.Driver.Navigate().GoToUrl(url);

            // Seta a espera implícita
            PropertiesCollection.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(PropertiesCollection.DefaultTimeout);
        }

        [OneTimeTearDown]
        public static void CleanUp()
        {
            PropertiesCollection.Driver.Quit();
            PropertiesCollection.Driver = null;
        }
    }
}
