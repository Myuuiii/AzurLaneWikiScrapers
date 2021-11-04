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

		private static bool _downloadShipData = false;
		private static bool _downloadShipImages = false;
		private static bool _downloadShipUrls = false;
		private static bool _downloadEventsData = false;
		private static bool _downloadEventsImages = false;
		private static bool _downloadEquipmentUrls = false;

		public static List<AzurLaneShip> ships = new List<AzurLaneShip>();

		static void Main(string[] args)
		{
			if (!Directory.Exists(_exportFolder)) Directory.CreateDirectory(_exportFolder);
			if (!Directory.Exists(_exportFolder + "ships/")) Directory.CreateDirectory(_exportFolder + "ships/");
			if (!Directory.Exists(_exportFolder + "gallery/")) Directory.CreateDirectory(_exportFolder + "gallery/");
			if (!Directory.Exists(_exportFolder + "events/")) Directory.CreateDirectory(_exportFolder + "events/");

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

				if (args.Contains("--events-data")) _downloadEventsData = true;
				if (args.Contains("--events-images")) _downloadEventsImages = true;

				if (args.Contains("--equipment-urls")) _downloadEquipmentUrls = true;

				// Section options
				if (args.Contains("--ships")) _downloadShipData = _downloadShipImages = _downloadShipUrls = true;
				if (args.Contains("--events")) _downloadEventsData = _downloadEventsImages = true;
				if (args.Contains("--equipment")) _downloadEquipmentUrls = true;

				// All / All - Excluded options
				if (args.Contains("--all"))
				{
					_downloadShipData = true;
					_downloadShipImages = true;
					_downloadShipUrls = true;

					_downloadEventsData = true;
					_downloadEventsImages = true;

					_downloadEquipmentUrls = true;
				}
				if (args.Contains("--all-noimg"))
				{
					_downloadShipData = true;
					_downloadShipUrls = true;

					_downloadEventsData = true;

					_downloadEquipmentUrls = true;
				}
				if (args.Contains("--all-nodata"))
				{
					_downloadShipImages = true;
					_downloadEventsImages = true;
				}
			}

			/// <summary>
			/// Export ship urls
			/// </summary>
			AzurLaneShipSource[] shipSources = scrapers.UrlScraper.Execute();
			AnsiConsole.MarkupLine("[gray]Loaded ship sources[/]");
			AzurLaneEquipmentSource[] equipmentSources = scrapers.EquipmentUrlScraper.Execute();
			AnsiConsole.MarkupLine("[gray]Loaded equipment sources[/]");

			/// <sumamry>
			/// Export ship urls
			/// </summary>
			if (_downloadShipUrls)
			{
				File.WriteAllText($"{_exportFolder}shipUrls.json", JsonConvert.SerializeObject(shipSources, Formatting.Indented));
				AnsiConsole.MarkupLine("[lime]Exported ship sources[/]");
			}

			/// <summary>
			/// Export ship data
			/// </summary>
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

			/// <summary>
			/// Export ship images
			/// </summary>
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

			/// <summary>
			/// Export events data
			/// </summary>
			if (_downloadEventsData)
			{
				AnsiConsole.Status().Start("Scraping Events...", ctx =>
				{
					AzurLaneEvent[] events = scrapers.EventsScraper.Execute();
					File.WriteAllText($"{_exportFolder}/events.json", JsonConvert.SerializeObject(events, Formatting.Indented));
				});
				AnsiConsole.MarkupLine("[lime]Exported Event Data![/]");
			}

			/// <summary>
			/// Export events images
			/// </summary>
			if (_downloadEventsImages)
			{
				AnsiConsole.Status().Start("Scraping Event Images...", ctx =>
				{
					AzurLaneEvent[] events = scrapers.EventsScraper.Execute();
					currentItem = 1;
					foreach (AzurLaneEvent eventData in events)
					{
						ctx.Status("Scraping event images: " + currentItem + "/" + events.Length + $" ([gray]{eventData.Name}[/])");
						scrapers.EventImagesScraper.Execute(eventData, _exportFolder);
						currentItem++;
					}
				});
				AnsiConsole.MarkupLine("[lime]Exported Event Images![/]");
			}

			/// <summary>
			/// Export equipment urls
			/// </summary>
			if (_downloadEquipmentUrls)
			{
				AnsiConsole.Status().Start("Scraping Equipment Urls...", ctx =>
				{
					AzurLaneEquipmentSource[] equipmentSources = scrapers.EquipmentUrlScraper.Execute();
					File.WriteAllText($"{_exportFolder}equipmentUrls.json", JsonConvert.SerializeObject(equipmentSources, Formatting.Indented));
				});
			}
		}
	}
}