using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System.IO;
using CountLinks.PageObjects;

namespace LinksProjects.StepDefinitions
{
    [Binding]
    public class TotalAmountLinksStepDefinitions
    {
        private IWebDriver driver;
        private ProductPageObject productPage;
        private int linksOnPageCount;
        private int linksInFileCount;

        [Given(@"User is on the website ""(.*)"" with key word ""(.*)""")]
        public void GivenUserIsOnTheWebsiteWithKeyWord(string url, string keyWord)
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl(url);
            productPage = new ProductPageObject(driver);
            productPage.product = keyWord;
            productPage.WriteLinksIntoFile(productPage.product);
        }

        [When(@"User count the total amount of links on the page")]
        public void WhenUserCountTheTotalAmountOfLinksOnThePage()
        {
            linksOnPageCount = productPage.CountLinks();
        }

        [When(@"User count the total amount of links in the file")]
        public void WhenUserCountTheTotalAmountOfLinksInTheFile()
        {
            linksInFileCount = productPage.CountFileLines(productPage.filePath);
        }

        [Then(@"User compare the total amount of links with the number of links in the file ""(.*)""")]
        public void ThenUserCompareTheTotalAmountOfLinksWithTheNumberOfLinksInTheFile(string filePath)
        {
            Assert.That(linksOnPageCount, Is.EqualTo(linksInFileCount));
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            productPage = new ProductPageObject(driver);
            File.WriteAllText(productPage.filePath, string.Empty);
        }


        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
