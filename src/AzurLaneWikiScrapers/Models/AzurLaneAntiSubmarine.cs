using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAntiSubmarine
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public int Stars { get; set; }
		public int AntiSubmarine { get; set; }
		public int Accuracy { get; set; }
		public int Damage { get; set; }
		public double Reload { get; set; }
		public double DPS { get; set; }
		public int Range { get; set; }
		public int AoERadius { get; set; }
		public AzurLaneAntiSubmarineType Type { get; set; }
		public string Notes { get; set; }
	}
}