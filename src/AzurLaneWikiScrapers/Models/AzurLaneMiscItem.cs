namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneMiscItem
	{
		public AzurLaneMiscItem() { }
		public AzurLaneMiscItem(string name, string url)
		{
			Name = name;
			Url = url;
		}

		public string Name { get; set; }
		public string Url { get; set; }
	}
}