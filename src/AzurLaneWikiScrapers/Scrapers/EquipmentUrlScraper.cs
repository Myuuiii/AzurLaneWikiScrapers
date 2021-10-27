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

			/// <summary>
			/// Destroyer ships
			/// </summary>
			htmlDoc.LoadHtml(new WebClient().DownloadString("https://azurlane.koumakan.jp/List_of_Destroyer_Guns#Max_Rarity"));
			tableNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[5]/div[1]/div/div[4]/table/tbody");

			foreach (HtmlNode row in tableNode.Descendants("tr").Skip(1))
			{
				AzurLaneEquipmentSource source = new AzurLaneEquipmentSource();
				if (row.Descendants("td").Count() == 0) { continue; }

				source.Name = row.Descendants("td").First().InnerText.Replace("\n", "").Trim();
				source.Url = "https://azurlane.koumakan.jp" + row.Descendants("td").First().Descendants("a").First().Attributes["href"].Value;
				source.Type = Enums.AzurLaneEquipmentSourceType.DestroyerGun;

				if (sources.Any(s => s.Name == source.Name)) { continue; }

				sources.Add(source);
			}

			return sources.ToArray();
		}
	}
}