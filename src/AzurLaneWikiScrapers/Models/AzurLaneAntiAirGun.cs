using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAntiAirGun
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Type { get; set; }
		public int Level { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int AntiAir { get; set; }

		/// <summary>
		/// Damage per shell
		/// </summary>
		public int[] Damage { get; set; }

		/// <summary>
		/// Damage boost in %
		/// </summary>
		public int OPSDamageBoost { get; set; }

		/// <summary>
		/// Rate of fire on the gun in seconds
		/// </summary>
		public decimal[] RateOfFire { get; set; }

		public int Angle { get; set; }
		public int Range { get; set; }
		public AzurLaneAmmoType AmmoType { get; set; }
	}
}