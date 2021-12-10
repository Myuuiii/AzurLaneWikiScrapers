using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEquipmentSource
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public AzurLaneEquipmentSource() { }
		public string Name { get; set; }
		public string Url { get; set; }
		public AzurLaneEquipmentSourceType Type { get; set; }
	}
}