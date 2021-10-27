using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEquipmentSource
	{
		public AzurLaneEquipmentSource() { }
		public string EquipmentId { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public AzurLaneEquipmentSourceType Type { get; set; }
	}
}