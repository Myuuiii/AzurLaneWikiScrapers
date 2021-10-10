using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLanePlane
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public int Aviation { get; set; }

		// public string Ordnance { get; set; }
		// public string Guns { get; set; }

		public double Reload { get; set; }
		public double SurfaceDPSLightArmor { get; set; }
		public double SurfaceDPSMediumArmor { get; set; }
		public double SurfaceDPSHeavyArmor { get; set; }
		public double AntiAirDPS { get; set; }
		public double AntiAirBurst { get; set; }
		public int CrashSpeed { get; set; }
		public int CrashDamage { get; set; }
		public AzurLaneNation Nation { get; set; }
	}
}