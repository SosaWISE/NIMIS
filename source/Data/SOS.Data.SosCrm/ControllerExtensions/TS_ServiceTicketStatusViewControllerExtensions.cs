namespace SOS.Data.SosCrm
{
	using SOS.Lib.Util;
	using System;
	using AR = TS_ServiceTicketStatusView;
	using ARCollection = TS_ServiceTicketStatusViewCollection;
	using ARController = TS_ServiceTicketStatusViewController;

	public static class TS_ServiceTicketStatusViewControllerExtensions
	{
		public static ARCollection LoadAll(this ARController cntlr)
		{
			return cntlr.LoadCollection(AR.Query());
		}
		public static ARCollection ForTech(this ARController cntlr, int techId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.TechId, techId);
			return cntlr.LoadCollection(qry);
		}
		public static AR LoadByPrimaryKey(this ARController cntlr, long id)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.ID, id);
			return cntlr.LoadSingle(qry);
		}

		public static ARCollection ForAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId)
				.WHERE(AR.Columns.IsDeleted, false);
			return cntlr.LoadCollection(qry);
		}

		private static SubSonic.Query TechApptsQry(int techId)
		{
			return AR.Query()
				.WHERE(AR.Columns.AppointmentId, SubSonic.Comparison.IsNot, null)
				.WHERE(AR.Columns.TechId, techId)
				.WHERE(AR.Columns.IsDeleted, false)
				.WHERE(AR.Columns.StatusCodeId, SubSonic.Comparison.GreaterOrEquals, 1)
				.WHERE(AR.Columns.StatusCodeId, SubSonic.Comparison.LessOrEquals, 7);
		}
		public static ARCollection AppointmentsForTechId(this ARController cntlr, int techId, DateTime start, DateTime end)
		{
			var qry = TechApptsQry(techId)
				// narrow by date
				.WHERE(AR.Columns.StartOn, SubSonic.Comparison.GreaterOrEquals, start)
				.WHERE(AR.Columns.StartOn, SubSonic.Comparison.LessOrEquals, end);
			return cntlr.LoadCollection(qry);
		}
		public static AR OverlappingAppointment(this ARController cntlr, TS_Appointment excludeAppt)
		{
			return cntlr.OverlappingAppointment(excludeAppt.ID, excludeAppt.TechId, excludeAppt.StartOn, excludeAppt.EndOn);
		}
		public static AR OverlappingAppointment(this ARController cntlr, long apptId, int techId, DateTime startOn, DateTime endOn)
		{
			var qry = TechApptsQry(techId)
				// exclude the appointment
				.WHERE(AR.Columns.AppointmentId, SubSonic.Comparison.NotEquals, apptId)
				// A and B overlap if:
				//  - A.StartOn comes before B.EndOn 
				.WHERE(AR.Columns.StartOn, SubSonic.Comparison.LessThan, endOn)
				//  - and A.EndOn comes after B.StartOn
				.WHERE(AR.Columns.EndOn, SubSonic.Comparison.GreaterThan, startOn);

			qry.Top = "1";
			return cntlr.LoadSingle(qry);
			//qry.SelectList = "COUNT(*)";
			//return SOS.Data.Extensions.StoredProcedureExtensions.ExecuteScalar<int>(qry.ExecuteScalar());
		}
	}
}
