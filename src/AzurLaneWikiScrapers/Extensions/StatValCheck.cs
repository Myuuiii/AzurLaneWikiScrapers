namespace AzurLaneWikiScrapers
{
	public partial class Extensions
	{
		public static string StatValCheck(this string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return "0";
			else return input;
		}
	}
}