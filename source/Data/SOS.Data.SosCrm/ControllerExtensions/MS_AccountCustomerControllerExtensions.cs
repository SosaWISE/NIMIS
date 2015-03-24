//using AR = SOS.Data.SosCrm.MS_AccountCustomer;
//using ARCollection = SOS.Data.SosCrm.MS_AccountCustomerCollection;
//using ARController = SOS.Data.SosCrm.MS_AccountCustomerController;

//namespace SOS.Data.SosCrm.ControllerExtensions
//{
//// ReSharper disable once InconsistentNaming
//	public static class MS_AccountCustomerControllerExtensions
//	{
//		public static ARCollection GetByAccountId(this ARController cntlr, long accountId)
//		{
//			var qry = AR.Query()
//				.WHERE(AR.Columns.AccountId, accountId);

//			return cntlr.LoadCollection(qry);

//		}
//	}
//}
