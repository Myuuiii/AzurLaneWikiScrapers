using System;
using System.Collections.Generic;
using System.IO;
using AzurLaneWikiScrapers;
using AzurLaneWikiScrapers.Models;
using Newtonsoft.Json;
using Spectre.Console;

namespace AzurLaneWikiScrapersConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			AnsiConsole.Write(new Rule("Azur Lane Wiki Scraper"));
			AzurLaneWikiScraper scrapers = new AzurLaneWikiScraper();

			#region Export Ship Urls
			string[] urls = scrapers.UrlScraper.Execute();
			File.WriteAllText("shipUrls.json", JsonConvert.SerializeObject(urls, Formatting.Indented));
			#endregion

			#region Export Ships
			List<AzurLaneShip> ships = new List<AzurLaneShip>();
			foreach (string shipUrl in urls)
				ships.Add(scrapers.ShipsScraper.Execute(shipUrl));
			File.WriteAllText("ships.json", JsonConvert.SerializeObject(ships, Formatting.Indented));
			#endregion
		}
	}
}
