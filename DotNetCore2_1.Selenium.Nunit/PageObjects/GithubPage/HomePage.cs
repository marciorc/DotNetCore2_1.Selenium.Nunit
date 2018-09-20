using OpenQA.Selenium;
using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;

namespace DotNetCore2_1.Selenium.Nunit.PageObjects
{
    public partial class HomePage
    {
        IWebDriver _driver;

        public HomePage() => _driver = PropertiesCollection.Driver;

        // Elements

        public IWebElement InpSearch => _driver.FindElement(By.Name("q"));
    }

    public partial class HomePage
    {
        // Actions

        public void SearchAWord(string word)
        {
            InpSearch.SendKeys(word);
            InpSearch.SendKeys(Keys.Enter);
        }
    }
}

