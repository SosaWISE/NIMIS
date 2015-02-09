using SOS.Lib.Util;
using SubSonic;
using System;
using System.Collections.Generic;
using AR = SOS.Data.SosCrm.TS_ServiceTicketSkills_Map;
using ARCollection = SOS.Data.SosCrm.TS_ServiceTicketSkills_MapCollection;
using ARController = SOS.Data.SosCrm.TS_ServiceTicketSkills_MapController;

namespace SOS.Data.SosCrm
{
	public static class TS_ServiceTicketSkills_MapControllerExtensions
	{
		public static int DeleteAllForServiceTicket(this ARController cntlr, long serviceTicketId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.ServiceTicketId, serviceTicketId);
			return DataService.ExecuteQuery(qry.BuildDeleteCommand());
		}
	}
}
