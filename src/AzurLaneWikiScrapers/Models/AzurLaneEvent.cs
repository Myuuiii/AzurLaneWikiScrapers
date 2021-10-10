namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneEvent
	{
		public string Name { get; set; }
		public string JpPeriod { get; set; }
		public string CnPeriod { get; set; }
		public string EnPeriod { get; set; }
		public string Notes { get; set; }
		public bool IsLimited { get; set; }
		public string BannerUrl { get; set; }
	}
}