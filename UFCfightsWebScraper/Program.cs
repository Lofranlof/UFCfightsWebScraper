using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Net.Http;


namespace WebScraper
{ 
    class Program
    {
       // A basic web scraper for the main event of an upcoming UFC tournament
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            String url = "https://www.ufc.com/events";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tournamentNameElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-hero--full__headline-prefix']");
            var tournamentName = tournamentNameElement.InnerText.Trim();
            Console.WriteLine("Tournament: " + tournamentName);

            var mainEventElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-hero--full__headline is-large-text']"); //Will be null during UFC Fight Nights
            if (mainEventElement != null )
            {
                var mainEvent = System.Web.HttpUtility.HtmlDecode(mainEventElement.InnerText.Trim());
                Console.WriteLine("Main Event: " + mainEvent);
            }
            else
            {
                mainEventElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-hero--full__headline is-medium-text']");
                var mainEvent = System.Web.HttpUtility.HtmlDecode(mainEventElement.InnerText.Trim());
                Console.WriteLine("Main Event: " + mainEvent);
            }
        }
    }
}