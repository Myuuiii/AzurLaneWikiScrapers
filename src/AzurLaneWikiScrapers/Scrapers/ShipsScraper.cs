using System;
using System.Net;
using AzurLaneWikiScrapers.Models;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class ShipsScraper
	{
		/// <summary>
		/// Retrieve a <see cref="AzurLaneShip"/> object
		/// </summary>
		/// <param name="shipUrl">url of the ship of which to retrieve the <see cref-"AzurLaneShip"/> object</param>
		/// <returns><see cref="AzurLaneShip"/></returns>
		public AzurLaneShip Execute(string shipUrl)
		{
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(new WebClient().DownloadString(shipUrl));

			AzurLaneShip ship = new AzurLaneShip();


			#region Get Base Ship Information
			#endregion


			#region Get Ship Stats
			#endregion


			#region Get Ship Skins
			#endregion


			#region Get Ship Limit Breaks
			#endregion


			#region Get Ship Skills 
			#endregion


			#region Get Ship Gear
			#endregion


			#region Get Ship Gallery
			#endregion


			#region Get Ship Quotes
			#endregion


			return ship;
		}
	}
}