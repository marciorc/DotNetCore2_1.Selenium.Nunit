using DotNetCore2_1.Selenium.Nunit.EntityAndFactories;
using DotNetCore2_1.Selenium.Nunit.PageObjects;
using DotNetCore2_1.Selenium.Nunit.Utils;
using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DotNetCore2_1.Selenium.Nunit.TestCases
{
    [TestFixture]
    public class Search : Setup
    {
        public SearchEntity busca;
        public Search() => busca = new SearchFactory().DataSearch;

        [Test]
        public void SearchWord()
        {
            HomePage homePage = new HomePage();
            WaitUntil.ElementVisible(By.Name(homePage.InpSearch.GetAttribute("name")));

            homePage.InpSearch.SendKeys(busca.Search);
            homePage.InpSearch.SendKeys(Keys.Enter);

            // Ou utilize o método criado no PageObject da homePage
            // homePage.SearchAWord(busca.Search);

            SearchResultPage resultPage = new SearchResultPage();
            WaitUntil.ElementVisible(By.Id(resultPage.Container.GetAttribute("id")));

            Assert.IsTrue(resultPage.Repositories.Text.Contains("Repositories"));
        }
    }
}
