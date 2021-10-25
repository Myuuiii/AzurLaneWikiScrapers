using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AzurLaneWikiScrapers.Models;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class EventsScraper
	{
		/// <summary>
		/// Retrive a list of <see cref="AzurLaneEvent"/> objects
		/// </summary>
		public AzurLaneEvent[] Execute()
		{
			HtmlDocument eventsDoc = new HtmlDocument();
			List<AzurLaneEvent> events = new List<AzurLaneEvent>();

			eventsDoc.LoadHtml(new WebClient().DownloadString("https://azurlane.koumakan.jp/Events"));

			/// <summary>
			/// Retrieves all the limited events
			/// </summary>
			HtmlNode tbodyNode = eventsDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[5]/div[1]/table/tbody");
			foreach (HtmlNode tableRow in tbodyNode.Descendants("tr").Skip(1))
			{
				HtmlNode[] tableColumns = tableRow.Descendants("td").ToArray();
				AzurLaneEvent eventData = new AzurLaneEvent();
				eventData.Name = tableColumns.First().InnerText.Replace("\n", "").Trim();


				switch (tableColumns.Count())
				{
					case 5:
						if (!string.IsNullOrEmpty(tableColumns[1].InnerText.Replace("\n", "").Trim()) && tableColumns[1].InnerText.Replace("\n", "").Trim() != "--")
							eventData.JpPeriod = tableColumns[1].InnerText.Replace("\n", "").Trim();
						if (!string.IsNullOrEmpty(tableColumns[2].InnerText.Replace("\n", "").Trim()) && tableColumns[2].InnerText.Replace("\n", "").Trim() != "--")
							eventData.CnPeriod = tableColumns[2].InnerText.Replace("\n", "").Trim();
						if (!string.IsNullOrEmpty(tableColumns[3].InnerText.Replace("\n", "").Trim()) && tableColumns[3].InnerText.Replace("\n", "").Trim() != "--")
							eventData.EnPeriod = tableColumns[3].InnerText.Replace("\n", "").Trim();
						break;
					case 4:
						if (!string.IsNullOrEmpty(tableColumns[1].InnerText.Replace("\n", "").Trim()) && tableColumns[1].InnerText.Replace("\n", "").Trim() != "--")
						{
							if (tableColumns[1].Attributes.Contains("colspan"))
							{
								eventData.JpPeriod = eventData.CnPeriod = tableColumns[1].InnerText.Replace("\n", "").Trim();
							}
							else
							{
								eventData.JpPeriod = tableColumns[1].InnerText.Replace("\n", "").Trim();
							}
						}
						if (!string.IsNullOrEmpty(tableColumns[2].InnerText.Replace("\n", "").Trim()) && tableColumns[2].InnerText.Replace("\n", "").Trim() != "--")
						{
							if (tableColumns[2].Attributes.Contains("colspan"))
							{
								eventData.JpPeriod = eventData.CnPeriod = tableColumns[2].InnerText.Replace("\n", "").Trim();
							}
							else
							{
								eventData.JpPeriod = tableColumns[2].InnerText.Replace("\n", "").Trim();
							}
						}
						break;
					case 3:
						if (!string.IsNullOrEmpty(tableColumns[1].InnerText.Replace("\n", "").Trim()) && tableColumns[1].InnerText.Replace("\n", "").Trim() != "--")
						{
							eventData.JpPeriod = eventData.CnPeriod = eventData.EnPeriod = tableColumns[1].InnerText.Replace("\n", "").Trim();
						}
						break;
				}

				eventData.Notes = tableColumns.Last().InnerText.Replace("\n", "").Trim();
				eventData.IsLimited = true;
				events.Add(eventData);
			}

			HtmlNode paragraphNode = eventsDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[5]/div[1]/p[3]");
			foreach (HtmlNode imgNode in paragraphNode.Descendants("a"))
			{
				AzurLaneEvent eventData = new AzurLaneEvent();
				eventData.Name = imgNode.Attributes["title"].Value.Replace("\n", "").Trim();
				eventData.BannerUrl = imgNode.Descendants("img").First().Attributes["srcset"].Value.Split(' ').Skip(imgNode.Descendants("img").First().Attributes["srcset"].Value.Split(' ').Count() - 2).First();
				events.Add(eventData);
			}
			return events.ToArray();
		}
	}
}