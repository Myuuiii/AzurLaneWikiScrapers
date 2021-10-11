using System.Net;
using HtmlAgilityPack;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShipDownloadModel
	{
		public AzurLaneShipDownloadModel(AzurLaneShipSource source)
		{
			Source = source;
			HtmlDoc = new HtmlDocument();
			this.HtmlDoc.LoadHtml(new WebClient().DownloadString(source.Url));
		}
		public AzurLaneShipSource Source { get; set; }
		public HtmlDocument HtmlDoc { get; set; }
	}
}