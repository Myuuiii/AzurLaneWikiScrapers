using System.Collections.Generic;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEquipmentCollection
	{
		public List<AzurLaneGun> ShipGuns { get; set; } = new List<AzurLaneGun>();
		public List<AzurLaneTorpedo> ShipTorpedoes { get; set; } = new List<AzurLaneTorpedo>();
		public List<AzurLanePlane> Planes { get; set; } = new List<AzurLanePlane>();
		public List<AzurLaneAntiAirGun> AntiAirGuns { get; set; } = new List<AzurLaneAntiAirGun>();
		public List<AzurLaneAuxiliary> Auxiliary { get; set; } = new List<AzurLaneAuxiliary>();
		public List<AzurLaneCargo> Cargo { get; set; } = new List<AzurLaneCargo>();
		// Antisubmarine
	}
}