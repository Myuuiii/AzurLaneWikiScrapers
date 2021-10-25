using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLanePlane
	{
		public string Name { get; set; }
		public AzurLanePlaneType Type { get; set; }
		public int Level { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int Aviation { get; set; }
		public int Speed { get; set; }

		public int[] HealthScale { get; set; }

		/// <summary>
		/// Minimum damage dealt per torpedo (if type is torpedo bomber)
		/// </summary>
		public int MinDamage { get; set; }

		/// <summary>
		/// Maximum damage dealt per torpedo (if type is torpedo bomber)
		/// </summary>
		public int MaxDamage { get; set; }

		public decimal MinRateOfFire { get; set; }
		public decimal MaxRateOfFire { get; set; }

		public int? TorpedoCount { get; set; }
		public decimal DodgeLimit { get; set; }
		public int CrashDamage { get; set; }

		public string AAGuns { get; set; }
		public string Ordnance { get; set; }
	}
}