using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipSource
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public AzurLaneShipSource() { }
		public string ShipId { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
	}
}