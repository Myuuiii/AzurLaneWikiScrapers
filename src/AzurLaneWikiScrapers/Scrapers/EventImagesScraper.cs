using System.Net;
using System.Text.RegularExpressions;
using AzurLaneWikiScrapers.Models;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class EventImagesScraper
	{
		public void Execute(AzurLaneEvent eventData, string exportFolder)
		{
			string fileRegexPattern = @"[a-zA-Z0-9-. ]*";

			if (!string.IsNullOrWhiteSpace(eventData.BannerUrl))
			{
				var formattedBannerName = Regex.Match($"{eventData.Name}", fileRegexPattern);
				try
				{
					new WebClient().DownloadFile(eventData.BannerUrl, $"{exportFolder}/events/{formattedBannerName}.png");
				}
				catch
				{
					new WebClient().DownloadFile(eventData.BannerUrl, $"{exportFolder}/events/{formattedBannerName}.png");
				}
			}
		}
	}
}