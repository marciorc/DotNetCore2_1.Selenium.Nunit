using DotNetCore2_1.Selenium.Nunit.WebDriverSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace DotNetCore2_1.Selenium.Nunit.Utils
{
    public static class Helper
    {
        public static void FindNameInElementsAndClick(IList<IWebElement> webElements, string text)
        {
            String[] allText = new String[webElements.Count];
            int i = 0;
            foreach (IWebElement element in webElements)
            {
                allText[i++] = element.Text;

                // Tratamento de string
                // Ex.: ("\"Dados\""), realiza o replace para ("Dados")
                if (element.Text.Equals(text.Replace("\"", "")))
                {
                    element.Click();
                }
            }
        }

        public static IWebElement GetElement(By by)
        {
            IWebElement webElement = PropertiesCollection.Driver.FindElement(by);
            return webElement;
        }

        public static string GetElementAttribute(By by, string attribute)
        {
            IWebElement webElement = PropertiesCollection.Driver.FindElement(by);
            return webElement.GetAttribute(attribute);
        }

        public static string GetText(By by)
        {
            IWebElement webElement = PropertiesCollection.Driver.FindElement(by);
            return webElement.Text;
        }

        public static void SelectOptionByIndex(IWebElement webElement, int index)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByIndex(index);
        }

        public static void SelectOptionByText(IWebElement webElement, string text)
        {
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByText(text);
        }

        public static void SetText(By by, string text)
        {
            IWebElement webElement = PropertiesCollection.Driver.FindElement(by);
            webElement.SendKeys(text);
        }
                
        public static void Submit(By by)
        {
            IWebElement webElement = PropertiesCollection.Driver.FindElement(by);
            if (!(PropertiesCollection.Driver is InternetExplorerDriver))
                webElement.Submit();
            else
                webElement.SendKeys(Keys.Enter);
        }
    }
}