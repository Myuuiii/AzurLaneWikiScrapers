using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneBarrage
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string IconUrl { get; set; }
		public string ImageUrl { get; set; }
		public AzurLaneHull Hull { get; set; }
		public string[] Ships { get; set; }
		public AzurLaneBarrageRound[] Rounds { get; set; }
	}
}