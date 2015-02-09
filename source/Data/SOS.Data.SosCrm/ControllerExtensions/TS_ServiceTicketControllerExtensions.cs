using SOS.Lib.Util;
using System;
using AR = SOS.Data.SosCrm.TS_ServiceTicket;
using ARCollection = SOS.Data.SosCrm.TS_ServiceTicketCollection;
using ARController = SOS.Data.SosCrm.TS_ServiceTicketController;

namespace SOS.Data.SosCrm
{
	public static class TS_ServiceTicketControllerExtensions
	{
		public static ARCollection ByCurrentAppointmentId(this ARController cntlr, long appointmentId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.CurrentAppointmentId, appointmentId);
			return cntlr.LoadCollection(qry);
        }
	}
}
