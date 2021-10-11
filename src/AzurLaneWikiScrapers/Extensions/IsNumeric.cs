namespace AzurLaneWikiScrapers
{
	public static partial class Extensions
	{
		public static bool IsNumeric(this string s)
		{
			foreach (char c in s)
			{
				if (!char.IsDigit(c))
					return false;
			}
			return true;
		}
	}
}