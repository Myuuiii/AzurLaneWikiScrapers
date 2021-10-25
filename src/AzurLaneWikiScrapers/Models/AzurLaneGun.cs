using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneGun
	{
		public string Name { get; set; }

		/// <summary>
		/// The weapon's type, this is displayed as a hull type
		/// </summary>
		public string Type { get; set; }
		public int Level { get; set; }
		public AzurLaneNation Nation { get; set; }
		public AzurLaneRarity Rarity { get; set; }

		public int Firepower { get; set; }
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

		public int Spread { get; set; }
		public int Angle { get; set; }

		public int FiringRange { get; set; }
		public int ShellRange { get; set; }

		public int Volley { get; set; }
		public decimal VolleyTime { get; set; }
		public int Coefficient { get; set; }
		public AzurLaneAmmoType AmmoType { get; set; }
		public string Characteristic { get; set; }

		public string ObtainedFrom { get; set; }
		public string AnimationUrl { get; set; }
		public string Notes { get; set; }
	}
}