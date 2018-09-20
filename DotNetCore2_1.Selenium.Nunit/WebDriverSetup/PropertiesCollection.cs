using OpenQA.Selenium;

namespace DotNetCore2_1.Selenium.Nunit.WebDriverSetup
{
    class PropertiesCollection
    {
        public static IWebDriver Driver { get; set; }
        public static int DefaultTimeout { get; set; }
    }
}
