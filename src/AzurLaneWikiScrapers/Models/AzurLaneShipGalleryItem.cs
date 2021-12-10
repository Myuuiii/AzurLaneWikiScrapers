using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipGalleryItem
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public string InternalFileName { get; set; }
	}
}