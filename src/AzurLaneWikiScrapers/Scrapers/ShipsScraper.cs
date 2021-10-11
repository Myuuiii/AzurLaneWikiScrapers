using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AzurLaneWikiScrapers.Enums;
using AzurLaneWikiScrapers.Models;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class ShipsScraper
	{
		/// <summary>
		/// Retrieve a <see cref="AzurLaneShip"/> object
		/// </summary>
		/// <param name="shipUrl">ShipSource of which to retrieve the <see cref-"AzurLaneShip"/> object</param>
		/// <returns><see cref="AzurLaneShip"/></returns>
		public AzurLaneShip Execute(AzurLaneShipSource shipSource)
		{
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(new WebClient().DownloadString($"https://azurlane.koumakan.jp/w/index.php?title={shipSource.Url.Split('/').Last()}&mobileaction=toggle_view_desktop"));

			AzurLaneShip ship = new AzurLaneShip();
			bool shipHasNote = false;


			#region Check if ship has a note
			shipHasNote = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[5]/div[1]").Descendants("div").First().HasClass("hatnote");
			#endregion


			#region Get Base Ship Information

			// Get ship id
			ship.Id = shipSource.ShipId;

			// Get ship name
			ship.Name = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"firstHeading\"]").InnerText;

			// Get a ships construction time
			HtmlNode constructionTimeNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[1]/td[1]", shipHasNote);
			string constructionTimeNodeText = constructionTimeNode.InnerText.Replace("\n", "");
			string[] constructText = null;
			ship.ConstructionValues = new AzurLaneShipConstructionValues();
			if (constructionTimeNodeText != "Cannot be constructed" && constructionTimeNodeText.Split(':').Length == 3)
			{
				constructText = constructionTimeNode.InnerText.Replace("\n", "").Split(':');
				ship.ConstructionValues.ConstructionTime = new TimeSpan(Convert.ToInt32(constructText[0]), Convert.ToInt32(constructText[1]), Convert.ToInt32(constructText[2]));
			}

			// Get ship rarity and stars
			HtmlNode rarityNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[2]/td[1]", shipHasNote);
			ship.Rarity = (AzurLaneRarity)Enum.Parse(typeof(AzurLaneRarity), rarityNode.InnerText.Replace("★", "").Replace("\n", "").Replace(" ", ""));
			ship.Stars = Regex.Matches(rarityNode.InnerText, "★").Count;

			// Get ship hull
			HtmlNode hullNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[3]/td", shipHasNote);
			ship.Hull = (AzurLaneHull)Enum.Parse(typeof(AzurLaneHull), hullNode.InnerText.Replace("\n", "").Replace(" ", "").Split('&')[0]);
			#endregion


			#region Get Ship Stats
			List<AzurLaneShipStats> stats = new List<AzurLaneShipStats>();
			HtmlNode statsNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[2]/table/tbody", shipHasNote);
			HtmlNode[] statRows = statsNode.ChildNodes.Where(n => n.Name == "tr").Skip(1).Take(statsNode.ChildNodes.Where(n => n.Name == "tr").Skip(1).Count() - 1).ToArray();

			AzurLaneArmor shipArmor = AzurLaneArmor.Light;
			for (int i = 0; i < statRows.Count(); i++)
			{
				HtmlNode statRow = statRows[i];

				int skipModifier = 0;

				if (i == 0) skipModifier = 1;

				AzurLaneShipStats stat = new AzurLaneShipStats();

				// Stats that do not need skipModifier
				stat.Health = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(1).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Firepower = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(2).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Torpedo = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(3).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Aviation = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(4).First().InnerText.Replace("\n", "").StatValCheck());
				stat.AntiAir = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(5).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Reload = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(6).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Evasion = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(7).First().InnerText.Replace("\n", "").StatValCheck());

				if (i == 0)
				{
					shipArmor = (AzurLaneArmor)Enum.Parse(typeof(AzurLaneArmor), statRow.ChildNodes.Where(n => n.Name == "td").Skip(8).First().InnerText.Replace("\n", "").Replace(" ", ""));
				}
				stat.Armor = shipArmor;

				// Stats that need skipModifier
				stat.Speed = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(8 + skipModifier).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Accuracy = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(9 + skipModifier).First().InnerText.Replace("\n", "").StatValCheck());
				stat.Luck = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(10 + skipModifier).First().InnerText.Replace("\n", "").StatValCheck());
				stat.AntiSubmarine = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(11 + skipModifier).First().InnerText.Replace("\n", "").StatValCheck());
				stat.OilConsumption = Convert.ToInt32(statRow.ChildNodes.Where(n => n.Name == "td").Skip(12 + skipModifier).First().InnerText.Replace("\n", "").StatValCheck());

				stats.Add(stat);
			}

			if (stats.Count == 4)
			{
				ship.BaseStats = stats.Last();
				ship.Level100Stats = stats.Skip(2).First();
				ship.Level120Stats = stats.Skip(1).First();
				ship.Level125Stats = stats.First();
			}
			else if (stats.Count == 7)
			{
				ship.BaseStats = stats.Last();
				ship.Level100Stats = stats.Skip(5).First();
				ship.Level100RetrofitStats = stats.Skip(4).First();
				ship.Level120Stats = stats.Skip(3).First();
				ship.Level120RetrofitStats = stats.Skip(2).First();
				ship.Level125Stats = stats.Skip(1).First();
				ship.Level125RetrofitStats = stats.First();
			}
			else throw new Exception("Too many or too little statistics received");
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