using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneAntiSubmarine
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Type { get; set; }

		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }

		public int AntiSubmarine { get; set; }
		public int Accuracy { get; set; }

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

		/// <summary>
		/// Frequency in seconds
		/// </summary>
		public int PingFrequency { get; set; }

		public int[] PlaneHealth { get; set; }
		public int Speed { get; set; }
		public int DodgeLimit { get; set; }
		public int CrashDamage { get; set; }

		public int AreaOfAttack { get; set; }
		public int Range { get; set; }
		public int Volley { get; set; }
		public int Coefficient { get; set; }

		public string Ordnance { get; set; }
	}
}