using System.Collections.Generic;
using System.Linq;
using System.Net;
using AzurLaneWikiScrapers.Models;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class EquipmentUrlScraper
	{
		public AzurLaneEquipmentSource[] Execute()
		{
			List<AzurLaneEquipmentSource> sources = new List<AzurLaneEquipmentSource>();

			// We will be reusing this variable for all the categories
			HtmlDocument htmlDoc = new HtmlDocument();
			HtmlNode tableNode;

			List<(string, string)> xPaths = new List<(string, string)>
			{
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[4]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Destroyer_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Light_Cruiser_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Heavy_Cruiser_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Large_Cruiser_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Battleship_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Torpedoes"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Submarine_Torpedoes"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Fighters"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Dive_Bombers"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_Torpedo_Bombers"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Seaplanes"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_AA_Guns"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Auxiliary_Equipment"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody","https://azurlane.koumakan.jp/wiki/List_of_Cargo"),
				("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[1]/table/tbody", "https://azurlane.koumakan.jp/wiki/List_of_ASW_Equipment")
			};

			/// <summary>
			/// Destroyer ships
			/// </summary>


			for (int index = 0; index < xPaths.Count; index++)
			{
				htmlDoc.LoadHtml(new WebClient().DownloadString(xPaths[index].Item2));
				tableNode = htmlDoc.DocumentNode.SelectSingleNode(xPaths[index].Item1);
				foreach (HtmlNode row in tableNode.Descendants("tr").Skip(1))
				{
					AzurLaneEquipmentSource source = new AzurLaneEquipmentSource();
					if (row.Descendants("td").Count() == 0) { continue; }

					source.Name = row.Descendants("td").First().InnerText.Replace("\n", "").Trim();
					source.Url = "https://azurlane.koumakan.jp" + row.Descendants("td").First().Descendants("a").First().Attributes["href"].Value;

					switch (index)
					{
						case 1: source.Type = Enums.AzurLaneEquipmentSourceType.DestroyerGun; break;
						case 2: source.Type = Enums.AzurLaneEquipmentSourceType.LightCruiserGun; break;
						case 3: source.Type = Enums.AzurLaneEquipmentSourceType.HeavyCruiserGun; break;
						case 4: source.Type = Enums.AzurLaneEquipmentSourceType.LargeCruiserGun; break;
						case 5: source.Type = Enums.AzurLaneEquipmentSourceType.BattleshipGun; break;
						case 6: source.Type = Enums.AzurLaneEquipmentSourceType.ShipTorpedo; break;
						case 7: source.Type = Enums.AzurLaneEquipmentSourceType.SubmarineTorpedo; break;
						case 8: source.Type = Enums.AzurLaneEquipmentSourceType.FighterPlane; break;
						case 9: source.Type = Enums.AzurLaneEquipmentSourceType.DiveBomberPlane; break;
						case 10: source.Type = Enums.AzurLaneEquipmentSourceType.TorpedoBomberPlane; break;
						case 11: source.Type = Enums.AzurLaneEquipmentSourceType.Seaplane; break;
						case 12: source.Type = Enums.AzurLaneEquipmentSourceType.AntiAirGun; break;
						case 13: source.Type = Enums.AzurLaneEquipmentSourceType.Auxiliary; break;
						case 14: source.Type = Enums.AzurLaneEquipmentSourceType.Cargo; break;
						case 15: source.Type = Enums.AzurLaneEquipmentSourceType.AntiSubmarine; break;
					}

					if (sources.Any(s => s.Name == source.Name)) { continue; }

					sources.Add(source);
				}
			}



			return sources.ToArray();
		}
	}
}