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
		/// Retrieve all the ship urls
		/// </summary>
		/// <returns><see cref="string[]" /></returns>
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
						HtmlNode td = trNode.Descendants("td").First();
						HtmlNode a = td.Descendants("a").First();
						String anchorAttributeValue = a.Attributes["href"].Value;
						source.Url = "https://azurlane.koumakan.jp" + anchorAttributeValue;
						source.ShipId = a.InnerText.Replace("\n", "");
						sources.Add(source);
					}
				}
			}

			return sources.ToArray();
		}
	}
}