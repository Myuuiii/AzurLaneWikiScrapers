using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzurLaneWikiScrapers;
using AzurLaneWikiScrapers.Models;
using Newtonsoft.Json;
using Spectre.Console;

namespace AzurLaneWikiScrapersConsole
{
	class Program
	{
		private static string _exportFolder = Environment.CurrentDirectory.ToString() + "./export/";
		private static int currentItem = 0;
		public static string[] ValidItems = new string[] { "--debug", "--ships-data", "--ships-images" };

		private static bool _downloadShipData = false;
		private static bool _downloadShipImages = false;
		private static bool _downloadShipUrls = false;

		public static List<AzurLaneShip> ships = new List<AzurLaneShip>();

		static void Main(string[] args)
		{
			if (!Directory.Exists(_exportFolder)) Directory.CreateDirectory(_exportFolder);
			if (!Directory.Exists(_exportFolder + "ships/")) Directory.CreateDirectory(_exportFolder + "ships/");
			if (!Directory.Exists(_exportFolder + "gallery/")) Directory.CreateDirectory(_exportFolder + "gallery/");

			AnsiConsole.Write(new Rule("Azur Lane Wiki Scraper").Alignment(Justify.Left));
			AzurLaneWikiScraper scrapers = new AzurLaneWikiScraper();

			if (args.Length != 0)
			{
				// Debugging option
				if (args.Contains("--debug"))
				{
					Console.Write("Attach your debugger and press enter to start!");
					Console.ReadLine();
				}

				// Separate options
				if (args.Contains("--ships-data")) _downloadShipData = true;
				if (args.Contains("--ships-images")) _downloadShipImages = true;
				if (args.Contains("--ships-urls")) _downloadShipUrls = true;

				// All / All - Excluded options
				if (args.Contains("--all"))
				{
					_downloadShipData = true;
					_downloadShipImages = true;
				}
			}

			#region Export Ship Urls
			AzurLaneShipSource[] shipSources = scrapers.UrlScraper.Execute();
			AnsiConsole.MarkupLine("[gray]Loaded ship sources[/]");
			if (_downloadShipUrls)
			{
				File.WriteAllText($"{_exportFolder}shipUrls.json", JsonConvert.SerializeObject(shipSources, Formatting.Indented));
				AnsiConsole.MarkupLine("[lime]Exported ship sources[/]");
			}
			#endregion

			#region Export ship data
			if (_downloadShipData)
			{
				currentItem = 1;
				AnsiConsole.Status().Start("Scraping Ships...", ctx =>
				{
					foreach (AzurLaneShipSource shipSource in shipSources)
					{
						ctx.Status("Scraping ships: " + currentItem + "/" + shipSources.Length + $" ([gray]{shipSource.Name}[/])");
						AzurLaneShip ship = scrapers.ShipsDataScraper.Execute(shipSource);
						ships.Add(ship);

						// Create the directory for the ship if it does not exist
						if (!Directory.Exists($"{_exportFolder}ships/{ship.Id}")) Directory.CreateDirectory($"{_exportFolder}ships/{ship.Id}");

						File.WriteAllText($"{_exportFolder}ships/{ship.Id}/data.json", JsonConvert.SerializeObject(ship, Formatting.Indented));
						currentItem++;
					}

					ctx.Status("[yellow]Exporting...[/]");
					File.WriteAllText($"{_exportFolder}ships.json", JsonConvert.SerializeObject(ships, Formatting.Indented));
				});
				AnsiConsole.MarkupLine("[lime]Exported Ships Data![/]");
			}
			#endregion

			#region Export ship images
			if (_downloadShipImages)
			{
				if (_downloadShipData == false && !File.Exists($"{_exportFolder}ships.json"))
				{
					AnsiConsole.MarkupLine("[red]You need to download ship data first![/]");
					return;
				}
				else if (_downloadShipData == false && File.Exists($"{_exportFolder}ships.json"))
				{
					ships = JsonConvert.DeserializeObject<List<AzurLaneShip>>(File.ReadAllText($"{_exportFolder}ships.json"));
				}

				currentItem = 1;
				AnsiConsole.Status().Start("Scraping Ship Images...", ctx =>
				{
					foreach (AzurLaneShip ship in ships)
					{
						ctx.Status("Scraping ship images: " + currentItem + "/" + shipSources.Length + $" ([gray]{ship.Name}[/])");

						// Create the directory for the ship if it does not exist
						if (!Directory.Exists($"{_exportFolder}ships/{ship.Id}")) Directory.CreateDirectory($"{_exportFolder}ships/{ship.Id}");

						scrapers.ShipsImagesScraper.Execute(ship, _exportFolder);
						currentItem++;
					}
				});
				AnsiConsole.MarkupLine("[lime]Exported Ship Images![/]");
			}
			#endregion

			AnsiConsole.MarkupLine("[red]--- All specified operations were executed[/]");
		}
	}
}