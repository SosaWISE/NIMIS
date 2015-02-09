using System;
using AR = SOS.Data.SosCrm.CA_Appointment;
using ARCollection = SOS.Data.SosCrm.CA_AppointmentCollection;
using ARController = SOS.Data.SosCrm.CA_AppointmentController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class CA_AppointmentControllerExtensions
	{
		public static ARCollection GetByUserIdAndDateRange (this ARController oCntlr, int nUserId, DateTime dStartDate, DateTime dEndDate)
		{
			/** Initialize. */
			ARCollection oResult = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.CA_AppointmentGetByUserIdAndDateRange(nUserId, dStartDate, dEndDate));

			/** Return result. */
			return oResult;
		}
	}
}
