using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAuxiliary
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int[] OPSDamageBoost { get; set; }

		public int[] Health { get; set; }
		public int[] Firepower { get; set; }
		public int[] AntiAir { get; set; }
		public int[] Torpedo { get; set; }
		public int[] Aviation { get; set; }
		public int[] Reload { get; set; }
		public int[] Evasion { get; set; }
		public int[] AntiSubmarine { get; set; }
		public int[] Oxygen { get; set; }
		public int[] Accuracy { get; set; }
		public int[] Speed { get; set; }

		public string Notes { get; set; }
	}
}