using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAntiAirGun
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public int Firepower { get; set; }
		public int AntiAir { get; set; }
		public int Accuracy { get; set; }
		public int Damage { get; set; }
		public double Reload { get; set; }
		public double AADPS { get; set; }
		public int Range { get; set; }
		public AzurLaneNation Nation { get; set; }
	}
}