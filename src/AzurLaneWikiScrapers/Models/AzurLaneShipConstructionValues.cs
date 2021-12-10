using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipConstructionValues
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public TimeSpan ConstructionTime { get; set; }
		public bool LightAvailable { get; set; }
		public bool HeavyAvailable { get; set; }
		public bool SpecialAvailable { get; set; }
		public bool LimitedAvailable { get; set; }
		public bool Exchange { get; set; }
	}
}