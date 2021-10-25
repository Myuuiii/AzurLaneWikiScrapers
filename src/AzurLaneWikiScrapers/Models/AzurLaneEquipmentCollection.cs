using System.Collections.Generic;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEquipmentCollection
	{
		public List<AzurLaneGun> ShipGuns { get; set; } = new List<AzurLaneGun>();
		public List<AzurLaneTorpedo> ShipTorpedoes { get; set; } = new List<AzurLaneTorpedo>();
		// Planes
		// Seaplanes
		// AntiAirGuns
		// Auxiliary
		// Cargo
		// Antisubmarine
	}
}