﻿using CountLinks.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CountLinks.Program
{
    internal class CopyingLinkForProduct
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://loveit.com.ua");
        }

        [Test]
        public void Test1()
        {
            var productPage = new ProductPageObject(driver);
/*            productPage.WriteLinksIntoFile();*/
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}
