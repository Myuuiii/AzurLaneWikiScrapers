using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipQuote
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public string Skin { get; set; }
		public string Event { get; set; }
		public string AudioUrl { get; set; }
		public string EnTranscription { get; set; }
		public string CnTranscription { get; set; }
		public string JpTranscription { get; set; }
	}
}