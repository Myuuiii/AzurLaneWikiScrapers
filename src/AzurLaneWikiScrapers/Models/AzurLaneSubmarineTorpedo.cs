using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneSubmarineTorpedo
	{
		public string name { get; set; }
		public string IconUrl { get; set; }
		public string TorpedoPower { get; set; }
		public int TorpedoCount { get; set; }
		public int SingleTorpedoDamage { get; set; }
		public double Reload { get; set; }
		public double SurfaceDPS_LightArmor { get; set; }
		public double SurfaceDPS_MediumArmor { get; set; }
		public double SurfaceDPS_HeavyArmor { get; set; }

		public int Range { get; set; }
		public int Spread { get; set; }
		public int Angle { get; set; }
		public string Attribute { get; set; }
		public AzurLaneNation Nation { get; set; }
	}
}