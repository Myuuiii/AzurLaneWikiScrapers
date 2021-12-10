using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipSkill
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Description { get; set; }
	}
}