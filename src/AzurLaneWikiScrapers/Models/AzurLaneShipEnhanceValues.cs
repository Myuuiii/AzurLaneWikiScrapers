using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipEnhanceValues
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public int Firepower { get; set; }
		public int Torpedo { get; set; }
		public int Aviation { get; set; }
		public int Reload { get; set; }
	}
}