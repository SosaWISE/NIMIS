using System;
using System.Collections.Generic;
using System.Linq;
using SOS.Lib.Util;
using SubSonic;
using AR = NXS.Data.GreatPlains.MonitoringContractsView;
using ARCollection = NXS.Data.GreatPlains.MonitoringContractsViewCollection;
using ARController = NXS.Data.GreatPlains.MonitoringContractsViewController;

namespace NXS.Data.GreatPlains.ControllerExtensions
{
	public static class MonitoringContractControllerExtensions
	{
		public static AR GetMonitoringContract(this ARController controller, string customerNumber)
		{
			Query qry = AR.Query()
							.WHERE(AR.Columns.CustomerNumber, customerNumber);

			return controller.LoadSingle(qry);
		}
	}
}