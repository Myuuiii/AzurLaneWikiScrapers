using HtmlAgilityPack;

namespace AzurLaneWikiScrapers
{
	public partial class Functions
	{
		/// <summary>
		/// Translate the XPath in case the ship page has a note which changes the structure
		/// Please do only provide XPaths of ships that do not have a note
		/// </summary>
		public static HtmlNode GetXPathNode(HtmlDocument document, string xPath, bool hasNote)
		{
			if (hasNote)
			{
				return document.DocumentNode.SelectSingleNode(xPath.Replace("/html/body/div[3]/div[3]/div[5]/div[1]/div[2]", "/html/body/div[3]/div[3]/div[5]/div[1]/div[3]"));
			}
			else
			{
				return document.DocumentNode.SelectSingleNode(xPath);
			}
		}
	}
}