namespace AzurLaneWikiScrapers
{
	public class AzurLaneWikiScraper
	{
		public AzurLaneWikiScraper() { }

		public Scrapers.ShipUrlScraper UrlScraper = new Scrapers.ShipUrlScraper();
		public Scrapers.ShipsDataScraper ShipsDataScraper = new Scrapers.ShipsDataScraper();
		public Scrapers.ShipsImagesScraper ShipsImagesScraper = new Scrapers.ShipsImagesScraper();
	}
}