using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneTorpedo
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public int Level { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public AzurLaneNation Nation { get; set; }
		public bool IsSubmarineTorpedo { get; set; }

		public int Torpedo { get; set; }

		/// <summary>
		/// Damage per torpedo
		/// </summary>
		public int[] Damage { get; set; }

		public int TorpedoCount { get; set; }
		public int Spread { get; set; }
		public int Angle { get; set; }

		public int TorpedoRange { get; set; }
		public int FiringRange { get; set; }

		public int Coefficient { get; set; } = 100;
		public AzurLaneAmmoType AmmoType { get; set; }
		public string Characteristic { get; set; }

		public string ObtainedFrom { get; set; }
		public string AnimationUrl { get; set; }
		public string Notes { get; set; }
	}
}