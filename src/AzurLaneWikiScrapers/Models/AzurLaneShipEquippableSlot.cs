using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipEquippableSlot
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public int Slot { get; set; }
		public int MaxEfficiency { get; set; }
		public int MinEfficiency { get; set; }
		public string Type { get; set; }
		public int Max { get; set; }
		public int MaxRetrofit { get; set; }
	}
}