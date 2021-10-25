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

		public int[] Health { get; set; }

		/// <summary>
		/// Damage dealt per torpedo (if type is torpedo bomber)
		/// </summary>
		public int[] Damage { get; set; }

		/// <summary>
		/// The rate of fire of the plane
		/// </summary>
		public decimal RateOfFire { get; set; }

		public int? TorpedoCount { get; set; }
		public decimal DodgeLimit { get; set; }
		public int CrashDamage { get; set; }

		public string AAGuns { get; set; }
		public string Ordnance { get; set; }
	}
}