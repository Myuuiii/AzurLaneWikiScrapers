namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneBarrageRound
	{
		public AzurLaneBarrageRound() { }
		public AzurLaneBarrageRound(string type, double dmgL, double dmgM, double dmgH, string note)
		{
			this.Type = type;
			this.DmgL = dmgL;
			this.DmgM = dmgM;
			this.DmgH = dmgH;
			this.Note = note;
		}

		public string Type { get; set; }
		public double DmgL { get; set; }
		public double DmgM { get; set; }
		public double DmgH { get; set; }
		public string Note { get; set; }
	}
}