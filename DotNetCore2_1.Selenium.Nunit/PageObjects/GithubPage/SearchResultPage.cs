using OpenQA.Selenium;
using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;

namespace DotNetCore2_1.Selenium.Nunit.PageObjects
{
    public partial class SearchResultPage
    {
        IWebDriver _driver;

        public SearchResultPage() => _driver = PropertiesCollection.Driver;

        // Elements
        public IWebElement Container => _driver.FindElement(By.Id("js-pjax-container"));

        public IWebElement Repositories => _driver.FindElement(By.CssSelector(".menu-item.selected"));
    }

    public partial class SearchResultPage
    {
        // Actions

    }
}
