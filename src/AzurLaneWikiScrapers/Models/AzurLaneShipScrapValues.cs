using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipScrapValues
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public int Coins { get; set; }
		public int Oil { get; set; }
		public int Medals { get; set; }
		public int SpecializedCore { get; set; }
	}
}