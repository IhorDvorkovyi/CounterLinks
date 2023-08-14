using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CountLinks.PageObjects
{
    class ProductPageObject
    {
        private IWebDriver _webDriver;


        public ProductPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string filePath = "D:\\links.txt";
        public string product = "серце";
        public int TotalLink = 0;
        public string pagerLink = ".b-pager__link";
        public string productLink = "//a[@class='cs-image-holder__image-link']";

        public int CountFileLines(string filePath)
        {

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не знайдено.", filePath);
            }
            string fileContent = File.ReadAllText(filePath);
            int lineCount = (fileContent.Split('\n').Length)-1;

            return lineCount;
        }
        public int CountLinks()
        {
            _webDriver.Navigate().GoToUrl($"https://loveit.com.ua/ua/site_search/page_1?search_term={product}");
            var pagerLinks = _webDriver.FindElements(By.CssSelector(pagerLink));
            int AmountOfPages = int.Parse(pagerLinks[pagerLinks.Count - 2].Text);
            for (int i = 1; i <= AmountOfPages; i++)
            {
                _webDriver.Navigate().GoToUrl($"https://loveit.com.ua/ua/site_search/page_{i}?search_term={product}");
                var productLinks = _webDriver.FindElements(By.XPath(productLink));

                foreach (var link in productLinks)
                {
                    TotalLink++;
                }
            }
            return TotalLink;
        }
        public void WriteLinksIntoFile(string keyword)
        {
            _webDriver.Navigate().GoToUrl($"https://loveit.com.ua/ua/site_search/page_1?search_term={keyword}");
            var pagerLinks = _webDriver.FindElements(By.CssSelector(pagerLink));
            int AmountOfPages = int.Parse(pagerLinks[pagerLinks.Count - 2].Text);

            for (int i = 1; i <= AmountOfPages; i++)
            {
                _webDriver.Navigate().GoToUrl($"https://loveit.com.ua/ua/site_search/page_{i}?search_term={keyword}");
                var productLinks = _webDriver.FindElements(By.XPath(productLink));

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (var link in productLinks)
                    {
                        string productLink = link.GetAttribute("href");
                        writer.WriteLine(productLink);
                    }
                }
            }
        }

    }
}
