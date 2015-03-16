using System;
using AR = SOS.Data.SosCrm.MS_AccountCreditsAndInstallsView;
using ARCollection = SOS.Data.SosCrm.MS_AccountCreditsAndInstallsViewCollection;
using ARController = SOS.Data.SosCrm.MS_AccountCreditsAndInstallsViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountCreditsAndInstallsViewControllerExtensions
	{
		public static ARCollection GetByOfficeIdAndRepId(this ARController cntlr, int? officeId, string salesRepId, DateTime begindate, DateTime endDate,
			string gpEmployeeId)
		{
			return
				cntlr.LoadCollection(SosCrmDataStoredProcedureManager.MS_AccountCreditsAndInstallsBySalesRepByDate(officeId, salesRepId, 
					begindate, endDate, gpEmployeeId));
		}
	}
}
