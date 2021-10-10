using System;
using AzurLaneWikiScrapers;

namespace AzurLaneWikiScrapersConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			AzurLaneWikiScraper scrapers = new AzurLaneWikiScraper();

			string[] urls = scrapers.UrlScraper.Execute();
		}
	}
}
