using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipSkin
	{
		[JsonIgnore, Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string BackgroundUrl { get; set; }
		public string ChibiUrl { get; set; }
		public bool IsLive2D { get; set; }
		public string ObtainedFrom { get; set; }
	}
}