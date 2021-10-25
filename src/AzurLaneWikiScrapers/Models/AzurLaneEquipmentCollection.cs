using System.Collections.Generic;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEquipmentCollection
	{
		public List<AzurLaneGun> ShipGuns { get; set; } = new List<AzurLaneGun>();
		public List<AzurLaneTorpedo> ShipTorpedoes { get; set; } = new List<AzurLaneTorpedo>();
		public List<AzurLanePlane> Planes { get; set; } = new List<AzurLanePlane>();
		// AntiAirGuns
		// Auxiliary
		// Cargo
		// Antisubmarine
	}
}