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
		private const string _exportFolder = "./export/";
		private static int currentItem = 0;

		static void Main(string[] args)
		{
			if (!Directory.Exists(_exportFolder))
				Directory.CreateDirectory(_exportFolder);

			AnsiConsole.Write(new Rule("Azur Lane Wiki Scraper").Alignment(Justify.Left));
			AzurLaneWikiScraper scrapers = new AzurLaneWikiScraper();

			if (args.Length != 0)
			{
				if (args[0] == "--debug")
				{
					Console.Write("Attach your debugger and press enter to start!");
					Console.ReadLine();
				}
			}

			#region Export Ship Urls
			AzurLaneShipSource[] shipSources = scrapers.UrlScraper.Execute();
			File.WriteAllText($"{_exportFolder}shipUrls.json", JsonConvert.SerializeObject(shipSources, Formatting.Indented));
			#endregion

			#region Export Ships
			List<AzurLaneShip> ships = new List<AzurLaneShip>();
			currentItem = 1;
			AnsiConsole.Status().Start("Scraping Ships...", ctx =>
			{
				foreach (AzurLaneShipSource shipSource in shipSources)
				{
					ctx.Status("Scraping ships: " + currentItem + "/" + shipSources.Length);
					ships.Add(scrapers.ShipsScraper.Execute(shipSource));
					currentItem++;
				}

				ctx.Status("[yellow]Exporting...[/]");
				File.WriteAllText($"{_exportFolder}ships.json", JsonConvert.SerializeObject(ships, Formatting.Indented));
			});
			AnsiConsole.MarkupLine("[lime]Exported Ships![/]");
			#endregion
		}
	}
}
