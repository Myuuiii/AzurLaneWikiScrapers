using AzurLaneWikiScrapers.Enums;

namespace AzurLaneWikiScrapers.Models
{
	public class AzurLaneShip
	{
		public string Id { get; set; }

		public string Name { get; set; }
		public AzurLaneRarity Rarity { get; set; }
		public int Stars { get; set; }
		public AzurLaneNation Nation { get; set; }
		public AzurLaneHull Hull { get; set; }

		public string ThumbnailUrl { get; set; }
		public AzurLaneShipSkin[] Skins { get; set; }
		public AzurLaneShipSkill[] Skills { get; set; }
		public AzurLaneShipLimitBreak[] LimitBreaks { get; set; }
		public AzurLaneShipGalleryItem[] GalleryItems { get; set; }
		public AzurLaneShipEquippableSlot[] EquippableSlots { get; set; }
		public AzurLaneShipQuote[] Quotes { get; set; }

		public AzurLaneShipStats BaseStats { get; set; }
		public AzurLaneShipStats Level100Stats { get; set; }
		public AzurLaneShipStats Level100RetrofitStats { get; set; }
		public AzurLaneShipStats Level120Stats { get; set; }
		public AzurLaneShipStats Level120RetrofitStats { get; set; }
		public AzurLaneShipStats Level125Stats { get; set; }
		public AzurLaneShipStats Level125RetrofitStats { get; set; }

		public AzurLaneShipEnhanceValues EnhanceValues { get; set; }
		public AzurLaneShipScrapValues ScrapValues { get; set; }
		public AzurLaneShipConstructionValues ConstructionValues { get; set; }

		public string Artist { get; set; }
		public string Pixiv { get; set; }
		public string Twitter { get; set; }
		public string Web { get; set; }
		public string VoiceActor { get; set; }
	}
}