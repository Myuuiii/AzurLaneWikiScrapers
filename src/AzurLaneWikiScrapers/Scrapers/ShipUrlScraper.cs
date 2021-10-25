using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AzurLaneWikiScrapers.Models;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class ShipUrlScraper
	{
		/// <summary>
		/// Retrieve all the <see cref="AzurLaneShipSource"/>s from the Azur Lane Wiki
		/// </summary>
		/// <returns></returns>
		public AzurLaneShipSource[] Execute()
		{
			// This is the list that we will return later converted to a string[]
			List<AzurLaneShipSource> sources = new List<AzurLaneShipSource>();

			// Download the HTML contents of the overview page
			String shipsListPageContents = new WebClient().DownloadString("https://azurlane.koumakan.jp/List_of_Ships");

			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(shipsListPageContents);

			IEnumerable<HtmlNode> tableNodes = document.DocumentNode.Descendants("table");
			List<HtmlNode> tbodyNodes = new List<HtmlNode>();


			foreach (var tableNode in tableNodes.Take(tableNodes.Count() - 1))
			{
				foreach (var tbodyNode in tableNode.Descendants("tbody"))
				{
					foreach (var trNode in tbodyNode.Descendants("tr").Skip(1))
					{
						AzurLaneShipSource source = new AzurLaneShipSource();
						source.Url = "https://azurlane.koumakan.jp" + trNode.Descendants("td").First().Descendants("a").First().Attributes["href"].Value;
						source.ShipId = trNode.Descendants("td").First().Descendants("a").First().InnerText.Replace("\n", "");
						source.Name = trNode.Descendants("td").Skip(1).First().InnerText.Replace("\n", "");
						sources.Add(source);
					}
				}
			}

			return sources.ToArray();
		}
	}
}