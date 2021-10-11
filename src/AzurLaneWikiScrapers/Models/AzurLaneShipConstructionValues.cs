using System;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipConstructionValues
	{
		public TimeSpan ConstructionTime { get; set; }
		public bool LightAvailable { get; set; }
		public bool HeavyAvailable { get; set; }
		public bool SpecialAvailable { get; set; }
		public bool LimitedAvailable { get; set; }
		public bool Exchange { get; set; }
	}
}