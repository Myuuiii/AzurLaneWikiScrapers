using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AzurLaneWikiScrapers.Models;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class ShipsImagesScraper
	{
		/// <summary>
		/// Download all the images belonging to a ship to the specified folder
		/// </summary>
		/// <param name="ship">Ship object of which to download the images</param>
		/// <param name="exportFolder">Folder to save the images to</param>
		public void Execute(AzurLaneShip ship, string exportFolder)
		{
			string fileRegexPattern = @"[a-zA-Z0-9-. ]*";

			// Download skin images
			if (ship.Skins != null)
			{
				foreach (AzurLaneShipSkin skin in ship.Skins)
				{
					if (skin.ImageUrl != null)
					{
						var formattedSkinName = Regex.Match($"{skin.Name}", fileRegexPattern);
						try
						{
							new WebClient().DownloadFile(skin.ImageUrl, $"{exportFolder}/ships/{ship.Id}/" + formattedSkinName.Value + ".png");
						}
						catch
						{
							new WebClient().DownloadFile(skin.ImageUrl, $"{exportFolder}/ships/{ship.Id}/" + formattedSkinName.Value + ".png");
						}
					}
				}
			}


			// Download gallery images
			if (ship.GalleryItems != null)
			{
				for (int i = 0; i < ship.GalleryItems.Count(); i++)
				{
					try
					{
						new WebClient().DownloadFile(ship.GalleryItems[i].Url, $"{exportFolder}/gallery/{string.Join('_', ship.GalleryItems[i].Url.Split('/').Skip(ship.GalleryItems[i].Url.Split('/').Count() - 3).Take(3))}");
					}
					catch
					{
						new WebClient().DownloadFile(ship.GalleryItems[i].Url, $"{exportFolder}/gallery/{string.Join('_', ship.GalleryItems[i].Url.Split('/').Skip(ship.GalleryItems[i].Url.Split('/').Count() - 3).Take(3))}");
					}
				}
			}


			// Download thumbnail 
			try
			{
				new WebClient().DownloadFile(ship.ThumbnailUrl, $"{exportFolder}/ships/{ship.Id}/THUMBNAIL.png");
			}
			catch
			{
				new WebClient().DownloadFile(ship.ThumbnailUrl, $"{exportFolder}/ships/{ship.Id}/THUMBNAIL.png");
			}
		}
	}
}