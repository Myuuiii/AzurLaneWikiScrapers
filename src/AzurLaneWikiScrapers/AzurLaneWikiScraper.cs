namespace AzurLaneWikiScrapers
{
	public class AzurLaneWikiScraper
	{
		public AzurLaneWikiScraper() { }

		/// <summary>
		/// Scraper used for retrieving the links of ships
		/// </summary>
		public Scrapers.ShipUrlScraper UrlScraper = new Scrapers.ShipUrlScraper();

		/// <summary>
		/// Scraper used for retrieving ship data
		/// </summary>
		public Scrapers.ShipsDataScraper ShipsDataScraper = new Scrapers.ShipsDataScraper();

		/// <summary>
		/// Scraper used for retrieving ship images
		/// </summary>
		public Scrapers.ShipsImagesScraper ShipsImagesScraper = new Scrapers.ShipsImagesScraper();


		/// <summary>
		/// Scraper used for retrieving event data
		/// </summary>
		public Scrapers.EventsScraper EventsScraper = new Scrapers.EventsScraper();

		/// <summary>
		/// Scraper used for retrieving event images
		/// </summary>
		public Scrapers.EventImagesScraper EventImagesScraper = new Scrapers.EventImagesScraper();

		/// <summary>
		/// Scraper used for retrieving equipment urls
		/// </summary>
		/// <returns></returns>
		public Scrapers.EquipmentUrlScraper EquipmentUrlScraper = new Scrapers.EquipmentUrlScraper();

		/// <summary>
		/// Scraper used for retrieving equipment data
		/// </summary>
		/// <returns></returns>
		public Scrapers.EquipmentDataScraper EquipmentDataScraper = new Scrapers.EquipmentDataScraper();
	}
}