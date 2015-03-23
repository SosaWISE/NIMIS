using System;

namespace SOS.FunctionalServices.Contracts.Models.Connext
{
	public interface IFnsCxAppointment
	{
		long AppointmentID { get; }
		long ContactId { get; }
		DateTime AppointmentDate { get; }
		string Note { get; }
		DateTime CreatedOn { get; }
		string CreatedBy { get; }

	}
}