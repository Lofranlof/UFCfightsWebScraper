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
            Console.WriteLine("Tournament:\t" + tournamentName);

            var mainEventElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-hero--full__headline is-large-text']"); //Will be null during UFC Fight Nights
            if (mainEventElement != null ) {
                var mainEvent = System.Web.HttpUtility.HtmlDecode(mainEventElement.InnerText.Trim());
                Console.WriteLine("Main Event:\t" + mainEvent);
            }
            else {
                mainEventElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-hero--full__headline is-medium-text']");
                var mainEvent = System.Web.HttpUtility.HtmlDecode(mainEventElement.InnerText.Trim());
                Console.WriteLine("Main Event:\t" + mainEvent);
            }

            var additionalInfoElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-card-event--result__date tz-change-data']");
            var additionalInfo = additionalInfoElement.InnerText.Replace("/ Основной кард", "").Trim().Trim();

            var locationInfoElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='field field--name-location field--type-address field--label-hidden field__item']");
            var locationInfo = locationInfoElement.InnerText.Replace("\n", " ").Trim();


            Console.WriteLine("Date & Time:\t" + additionalInfo);
            Console.WriteLine("Location:\t" + locationInfo + '\n' + '\n');
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

    }
}