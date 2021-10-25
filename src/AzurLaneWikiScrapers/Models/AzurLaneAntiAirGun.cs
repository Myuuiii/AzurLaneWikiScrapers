using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAntiAirGun
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public int Level { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int AntiAir { get; set; }
		/// <summary>
		/// Minimum damage per shell
		/// </summary>
		public int MinDamage { get; set; }

		/// <summary>
		/// Maximum damage per shell
		/// </summary>
		public int MaxDamage { get; set; }

		/// <summary>
		/// Damage boost in %
		/// </summary>
		public int OPSDamageBoost { get; set; }

		/// <summary>
		/// The minimum rate of fire on the gun in seconds
		/// </summary>
		public decimal MinRateOfFire { get; set; }

		/// <summary>
		/// The maximum rate of fire on the gun in seconds
		/// </summary>
		public decimal MaxRateOfFire { get; set; }
		public int Angle { get; set; }
		public int Range { get; set; }
		public AzurLaneAmmoType AmmoType { get; set; }
	}
}