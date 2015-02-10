using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Wcf.Crm.Helper
{
	public static class CaModelHelper
	{
		public static CaFullCalenderAppointmentModel CastToCaFullCalendarAppointmentModel(IFnsCaAppointmentModel oFnsAppointment)
		{
			/** Initialize. */
			var oResult = new CaFullCalenderAppointmentModel
			{
				id = (int) oFnsAppointment.AppointmentID,
				AppointmentTypeId = oFnsAppointment.AppointmentTypeId,
				DealerUserId = oFnsAppointment.DealerUserId,
				DealerId = oFnsAppointment.DealerId,
				LeadId = oFnsAppointment.LeadId,
				CustomerId = oFnsAppointment.CustomerId,
				CustomerMasterFileId = oFnsAppointment.CustomerMasterFileId,
				title = oFnsAppointment.Title,
				Description = oFnsAppointment.Description,
				url = oFnsAppointment.Url,
				allDay = oFnsAppointment.AllDay,
				start = oFnsAppointment.StartDateTime,
				end = oFnsAppointment.EndDateTime,
				RemindMeMediaTypeId = oFnsAppointment.RemindMeMediaTypeId,
				RemindMeDelayTypeId = oFnsAppointment.RemindMeDelayTypeId

			};

			/** Return result. */
			return oResult;
		}
	}
}