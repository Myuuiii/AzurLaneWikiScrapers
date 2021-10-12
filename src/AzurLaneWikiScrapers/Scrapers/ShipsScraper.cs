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

			HtmlDocument galleryHtmlDoc = new HtmlDocument();
			galleryHtmlDoc.LoadHtml(new WebClient().DownloadString(shipSource.Url + "/Gallery"));


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

			// Get thumbnail ur; 
			ship.ThumbnailUrl = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/img", shipHasNote).Attributes["src"].Value.Replace("\n", "").Replace(" ", "");

			// Get a ships construction values
			HtmlNode constructionTimeNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[1]/td[1]", shipHasNote);
			string constructionTimeNodeText = constructionTimeNode.InnerText.Replace("\n", "");
			string[] constructText = null;
			ship.ConstructionValues = new AzurLaneShipConstructionValues();
			if (constructionTimeNodeText != "Cannot be constructed" && constructionTimeNodeText.Split(':').Length == 3)
			{
				constructText = constructionTimeNode.InnerText.Replace("\n", "").Split(':');
				ship.ConstructionValues.ConstructionTime = new TimeSpan(Convert.ToInt32(constructText[0]), Convert.ToInt32(constructText[1]), Convert.ToInt32(constructText[2]));
			}
			if (Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[1]", shipHasNote) != null)
			{
				ship.ConstructionValues.LightAvailable = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[1]", shipHasNote).InnerText.Replace("\n", "").Contains("✓");
				ship.ConstructionValues.HeavyAvailable = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[2]", shipHasNote).InnerText.Replace("\n", "").Contains("✓");
				ship.ConstructionValues.SpecialAvailable = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[3]", shipHasNote).InnerText.Replace("\n", "").Contains("✓");
				ship.ConstructionValues.LimitedAvailable = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[4]", shipHasNote).InnerText.Replace("\n", "").Contains("✓");
				ship.ConstructionValues.Exchange = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[2]/table/tbody/tr[4]/td[5]", shipHasNote).InnerText.Replace("\n", "").Contains("✓");
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
			List<AzurLaneShipSkin> skins = new List<AzurLaneShipSkin>();


			HtmlNode tabberNode = galleryHtmlDoc.DocumentNode.Descendants().First(n => n.HasClass("tabber"));

			foreach (HtmlNode tab in tabberNode.ChildNodes.Where(n => n.Name == "div"))
			{
				AzurLaneShipSkin skin = new AzurLaneShipSkin();
				HtmlNode[] skinImageDescendants = tab.Descendants("img").ToArray();

				skin.Name = tab.Attributes["title"].Value.Replace("\n", "");
				switch (skinImageDescendants.Count())
				{
					case 1:
						skin.ImageUrl = skinImageDescendants[0].Attributes["src"].Value;
						break;
					case 2:
						skin.ImageUrl = skinImageDescendants[0].Attributes["src"].Value;
						skin.BackgroundUrl = skinImageDescendants[1].Attributes["src"].Value;
						break;
					case 3:
					default:
						skin.ChibiUrl = skinImageDescendants[0].Attributes["src"].Value;
						skin.ImageUrl = skinImageDescendants[1].Attributes["src"].Value;
						skin.BackgroundUrl = skinImageDescendants[2].Attributes["src"].Value;
						break;
				}

				HtmlNode skintableDescendants = tab.Descendants("table").First();
				HtmlNode[] tablevalues = skintableDescendants.Descendants("td").ToArray();

				skin.ObtainedFrom = tablevalues[0].InnerText;
				if (tablevalues[1].InnerHtml == "Yes") skin.IsLive2D = true;

				skins.Add(skin);
			}
			ship.Skins = skins.ToArray();
			#endregion

			#region Get Ship Gallery
			List<AzurLaneShipGalleryItem> galleryItems = new List<AzurLaneShipGalleryItem>();

			try
			{
				HtmlNode artWorkgalleryNode = galleryHtmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[3]/div[5]/div[1]/div[3]");
				if (artWorkgalleryNode != null)
				{
					HtmlNode[] artWorkFrameNodes = artWorkgalleryNode.ChildNodes.Where(n => n.Name == "div").ToArray();

					foreach (var artworkFrameNode in artWorkFrameNodes)
					{
						AzurLaneShipGalleryItem item = new AzurLaneShipGalleryItem();

						HtmlNode imageNode = artworkFrameNode.Descendants("img").First();
						HtmlNode descriptionNode = artworkFrameNode.Descendants("div").Skip(1).First();

						item.Description = descriptionNode.InnerText;

						String[] urlParts = imageNode.Attributes["src"].Value.Replace("thumb/", "").Split('/');
						item.Url = String.Join('/', urlParts.Take(urlParts.Count() - 1));

						galleryItems.Add(item);
					}
				}
			}
			catch { }
			ship.GalleryItems = galleryItems.ToArray();
			#endregion


			#region Get Ship Misc Info
			HtmlNode miscInfoNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[2]/td[2]", shipHasNote);
			HtmlNode[] anchorNodes = miscInfoNode.ChildNodes.Where(n => n.Name == "a").ToArray();
			foreach (HtmlNode anchorNode in anchorNodes)
			{
				if (anchorNode.Attributes["href"].Value.StartsWith("/Artists"))
					ship.Artist = anchorNode.InnerText.Replace("\n", "");

				if (anchorNode.Attributes.Contains("title"))
				{
					if (anchorNode.Attributes["title"].Value == "Pixiv")
					{
						ship.Pixiv = anchorNode.Attributes["href"].Value;
					}
					else if (anchorNode.Attributes["title"].Value == "Twitter")
					{
						ship.Twitter = anchorNode.Attributes["href"].Value;
					}
				}
				else
				{
					ship.Web = anchorNode.Attributes["href"].Value;
				}
			}

			HtmlNode voiceActorNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[4]/table/tbody/tr[1]/td[2]", shipHasNote);
			ship.VoiceActor = voiceActorNode.InnerText.Replace("\n", "").Replace("Play", "").Trim();
			#endregion


			#region Get Ship Limit Breaks & Skills
			HtmlNode limitBreakSkillNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/table", shipHasNote);
			HtmlNode[] limitBreakSkillRows = limitBreakSkillNode.Descendants("tr").Skip(1).ToArray();


			List<AzurLaneShipLimitBreak> limitBreaks = new List<AzurLaneShipLimitBreak>();
			List<AzurLaneShipSkill> skills = new List<AzurLaneShipSkill>();

			// First 2 columns are limit breaks
			// Other 3 columns are skills
			foreach (HtmlNode limitBreakSkillRow in limitBreakSkillRows)
			{
				HtmlNode[] limitBreakSkillRowColumns = limitBreakSkillRow.Descendants("td").ToArray();

				if (!string.IsNullOrWhiteSpace(limitBreakSkillRowColumns[0].InnerText.Replace("\n", "").Trim()))
				{
					AzurLaneShipLimitBreak limitBreak = new AzurLaneShipLimitBreak();
					limitBreak.Level = limitBreaks.Count + 1;
					limitBreak.Info = limitBreakSkillRowColumns[0].InnerText.Replace("\n", "").Trim();
					limitBreaks.Add(limitBreak);
				}

				if (limitBreakSkillRowColumns[1].Descendants("img").Count() == 1)
				{
					// Skill at this level
					AzurLaneShipSkill skill = new AzurLaneShipSkill();
					skill.Name = limitBreakSkillRowColumns[2].ChildNodes.First(n => n.Name == "b").InnerText.Replace("\n", "").Trim();
					skill.Description = limitBreakSkillRowColumns[3].InnerText.Replace("\n", "").Trim();
					skill.IconUrl = limitBreakSkillRowColumns[1].Descendants("img").First().Attributes["src"].Value;
					skills.Add(skill);
				}
			}

			ship.LimitBreaks = limitBreaks.ToArray();
			ship.Skills = skills.ToArray();
			#endregion


			#region Get ship enhance & scrap values
			HtmlNode enhanceScrapNode = Functions.GetXPathNode(htmlDoc, "/html/body/div[3]/div[3]/div[5]/div[1]/div[2]/div[1]/div[1]/table[3]/tbody", shipHasNote);

			HtmlNode enhanceValuesNode = enhanceScrapNode.Descendants("td").First();
			HtmlNode[] enhanceValuesImageNodes = enhanceValuesNode.Descendants("img").ToArray();
			HtmlNode[] rawTextNodes = enhanceValuesNode.Descendants("#text").Where(n => !n.InnerText.Contains("\n")).ToArray();

			ship.EnhanceValues = new AzurLaneShipEnhanceValues();
			{
				int currentIndex = 0;
				foreach (HtmlNode enValNode in enhanceValuesImageNodes)
				{
					if (enValNode.Attributes["alt"].Value.Contains("Firepower"))
					{
						ship.EnhanceValues.Firepower = Convert.ToInt32(rawTextNodes[currentIndex].InnerText.Trim());
					}
					else if (enValNode.Attributes["alt"].Value.Contains("Torpedo"))
					{
						ship.EnhanceValues.Torpedo = Convert.ToInt32(rawTextNodes[currentIndex].InnerText.Trim());
					}
					else if (enValNode.Attributes["alt"].Value.Contains("Aviation"))
					{
						ship.EnhanceValues.Aviation = Convert.ToInt32(rawTextNodes[currentIndex].InnerText.Trim());
					}
					else if (enValNode.Attributes["alt"].Value.Contains("Reload"))
					{
						ship.EnhanceValues.Reload = Convert.ToInt32(rawTextNodes[currentIndex].InnerText.Trim());
					}

					currentIndex++;
				}
			}

			#endregion

			#region Get ship scrap values
			#endregion

			#region Get Ship Gear
			#endregion

			#region Get Ship Quotes
			#endregion

			return ship;
		}
	}
}