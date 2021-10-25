using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneCargo
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Type { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int[] Health { get; set; }
		public int OPSDamageBoost { get; set; }
	}
}