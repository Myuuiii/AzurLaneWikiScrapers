using System;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipConstructionValues
	{
		public TimeSpan ConstructionTime { get; set; }
		public string LightAvailability { get; set; }
		public string HeavyAvailability { get; set; }
		public string SpecialAvailability { get; set; }
		public string LimitedAvailability { get; set; }
		public string Exchange { get; set; }
	}
}