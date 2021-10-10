using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneGun
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public int Firepower { get; set; }
		public int AntiAir { get; set; }

		public string Rounds { get; set; }
		public double RoundsPerSecond { get; set; }

		public int DamagePerShot { get; set; }
		public int DamageCoefficient { get; set; }
		public double VolleyTime { get; set; }
		public double Reload { get; set; }

		public double RawDPS { get; set; }
		public double LightArmorDPS { get; set; }
		public double MediumArmorDPS { get; set; }
		public double HeavyArmorDPS { get; set; }

		public int Range { get; set; }
		public int Spread { get; set; }
		public int Angle { get; set; }
		public string Attribute { get; set; }
		public AzurLaneAmmoType AmmoType { get; set; }
		public string Nation { get; set; }
	}
}