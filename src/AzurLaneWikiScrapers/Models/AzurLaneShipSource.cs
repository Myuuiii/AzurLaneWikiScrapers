namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipSource
	{
		public AzurLaneShipSource() { }
		public AzurLaneShipSource(string shipId, string url)
		{
			this.ShipId = shipId;
			this.Url = url;
		}
		public string ShipId { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
	}
}