using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipStats
	{
		public int Luck { get; set; }
		public AzurLaneArmor Armor { get; set; }
		public int Speed { get; set; }
		public int Health { get; set; }
		public int Firepower { get; set; }
		public int AntiAir { get; set; }
		public int Torpedo { get; set; }
		public int Evasion { get; set; }
		public int Aviation { get; set; }
		public int OilConsumption { get; set; }
		public int Reload { get; set; }
		public int AntiSubmarine { get; set; }
		public int Oxygen { get; set; }
		public int Ammunition { get; set; }
		public int Accuracy { get; set; }
		public string HuntingRange { get; set; } = "";
	}
}