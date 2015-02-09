using AR = SOS.Data.SosCrm.MS_EquipmentsView;
using ARCollection = SOS.Data.SosCrm.MS_EquipmentsViewCollection;
using ARController = SOS.Data.SosCrm.MS_EquipmentsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{

	// ReSharper disable once InconsistentNaming
	public static class MS_EquipmentsViewControllerExtensions
	{
		public static ARCollection GetEquipmentList(this ARController oCntlr)
		{
			/** Return result. */
			return oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentList());
		}

		public static ARCollection GetEquipmentExistingList(this ARController cntlr)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentExistings());
		}

		public static ARCollection FrequentlyInstalledEquipmentGet(this ARController cntlr)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_EquipmentMostFrequents());
		}


		public static AR LoadByPrimaryKey(this ARController cntlr, string equipmentID)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.EquipmentID, equipmentID);
			return cntlr.LoadSingle(qry);
		}
		public static AR ByPartNumber(this ARController cntlr, string partNumber)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.GPItemNmbr, partNumber);
			return cntlr.LoadSingle(qry);
		}
		public static AR ByBarcode(this ARController cntlr, string barcode)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.MS_EquipmentByBarcode(barcode));
		}

	}
}
