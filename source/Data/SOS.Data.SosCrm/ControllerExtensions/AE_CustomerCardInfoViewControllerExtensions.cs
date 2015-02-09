//using AR = SOS.Data.SosCrm.AE_CustomerCardInfoView;
//using ARCollection = SOS.Data.SosCrm.AE_CustomerCardInfoViewCollection;
//using ARController = SOS.Data.SosCrm.AE_CustomerCardInfoViewController;
//
//namespace SOS.Data.SosCrm.ControllerExtensions
//{
//// ReSharper disable once InconsistentNaming
//	public static class AE_CustomerCardInfoViewControllerExtensions
//	{
//		public static AR ByCMFID(this ARController cntlr, long cmfid, string gpEmployeeId)
//		{
//			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerCardInfoByCMFID(cmfid));
//		}
//
//		public static AR ByAccountId(this ARController cntlr, long accountId, string gpEmployeeId)
//		{
//			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerCardInfoByAccountId(accountId));
//		}
//	}
//}
