using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipLimitBreak
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public int Level { get; set; }
		public string Info { get; set; }
	}
}