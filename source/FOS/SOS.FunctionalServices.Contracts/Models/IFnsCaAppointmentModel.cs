using System;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsCaAppointmentModel
	{
		long AppointmentID { get; set; }
		string AppointmentTypeId { get; set; }
		int DealerUserId { get; set; }
		int DealerId { get; set; }
		long? LeadId { get; set; }
		long? CustomerId { get; set; }
		long? CustomerMasterFileId { get; set; }
		string Title { get; set; }
		string Description { get; set; }
		string Url { get; set; }
		bool AllDay { get; set; }
		DateTime StartDateTime { get; set; }
		DateTime EndDateTime { get; set; }
		string RemindMeMediaTypeId { get; set; }
		string RemindMeDelayTypeId { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
	}
}