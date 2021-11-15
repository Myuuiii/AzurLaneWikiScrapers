using System;
using AzurLaneWikiScrapers.Models;

namespace AzurLaneWikiScrapers.Scrapers
{
	public class EquipmentDataScraper
	{
		public AzurLaneEquipmentCollection Execute(AzurLaneEquipmentCollection collection, AzurLaneEquipmentSource source)
		{
			switch (source.Type)
			{
				case Enums.AzurLaneEquipmentSourceType.DestroyerGun:
				case Enums.AzurLaneEquipmentSourceType.BattleshipGun:
				case Enums.AzurLaneEquipmentSourceType.LightCruiserGun:
				case Enums.AzurLaneEquipmentSourceType.HeavyCruiserGun:
				case Enums.AzurLaneEquipmentSourceType.LargeCruiserGun:
					collection.ShipGuns.Add(HandleGun(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.ShipTorpedo:
					collection.ShipTorpedoes.Add(HandleTorpedo(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.FighterPlane:
				case Enums.AzurLaneEquipmentSourceType.DiveBomberPlane:
				case Enums.AzurLaneEquipmentSourceType.TorpedoBomberPlane:
					collection.Planes.Add(HandlePlane(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.AntiAirGun:
					collection.AntiAirGuns.Add(HandleAntiAirGun(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.Auxiliary:
					collection.Auxiliary.Add(HandleAuxiliary(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.Cargo:
					collection.Cargo.Add(HandleCargo(source));
					break;
				case Enums.AzurLaneEquipmentSourceType.AntiSubmarine:
					collection.AntiSubmarines.Add(HandleAntiSubmarine(source));
					break;
			}

			return collection;
		}

		private AzurLaneGun HandleGun(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLaneTorpedo HandleTorpedo(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLanePlane HandlePlane(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLaneAntiAirGun HandleAntiAirGun(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLaneAuxiliary HandleAuxiliary(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLaneCargo HandleCargo(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}

		private AzurLaneAntiSubmarine HandleAntiSubmarine(AzurLaneEquipmentSource source)
		{
			throw new NotImplementedException();
		}
	}
}