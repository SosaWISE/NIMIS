using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsCaAppointmentModel : IFnsCaAppointmentModel
	{
		#region .ctor

		public FnsCaAppointmentModel(CA_Appointment oAppointment)
		{
			AppointmentID = oAppointment.AppointmentID;
			AppointmentTypeId = oAppointment.AppointmentTypeId;
			DealerUserId = oAppointment.DealerUserId;
			DealerId = oAppointment.DealerId;
			LeadId = oAppointment.LeadId;
			CustomerId = oAppointment.CustomerId;
			CustomerMasterFileId = oAppointment.CustomerMasterFileId;
			Title = oAppointment.Title;
			Description = oAppointment.Description;
			Url = oAppointment.Url;
			AllDay = oAppointment.AllDay;
			StartDateTime = oAppointment.StartDateTime;
			EndDateTime = oAppointment.EndDateTime;
			RemindMeMediaTypeId = oAppointment.RemindMeMediaTypeId;
			RemindMeDelayTypeId = oAppointment.RemindMeDelayTypeId;
			IsActive = oAppointment.IsActive;
			IsDeleted = oAppointment.IsDeleted;
			ModifiedOn = oAppointment.ModifiedOn;
			ModifiedBy = oAppointment.ModifiedBy;
			CreatedOn = oAppointment.CreatedOn;
			CreatedBy = oAppointment.CreatedBy;
		}

		#endregion .ctor

		#region Implementation of IFnsCaAppointmentModel

		public long AppointmentID { get; set; }
		public string AppointmentTypeId { get; set; }
		public int DealerUserId { get; set; }
		public int DealerId { get; set; }
		public long? LeadId { get; set; }
		public long? CustomerId { get; set; }
		public long? CustomerMasterFileId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
		public bool AllDay { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public string RemindMeMediaTypeId { get; set; }
		public string RemindMeDelayTypeId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Implementation of IFnsCaAppointmentModel
	}
}