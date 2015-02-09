using System;

namespace SOS.Services.Interfaces.Models
{
	public interface ICaAppointmentModel
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
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
	}

	public class CaAppointmentModel : ICaAppointmentModel
	{
		#region Implementation of ICaAppointmentModel

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
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		#endregion Implementation of ICaAppointmentModel
	}

	public interface ICaFullCalendarAppointmentModel
	{
// ReSharper disable InconsistentNaming
		int id { get; set; }
		string AppointmentTypeId { get; set; }
		int DealerUserId { get; set; }
		int DealerId { get; set; }
		long? LeadId { get; set; }
		long? CustomerId { get; set; }
		long? CustomerMasterFileId { get; set; }
		string title { get; set; }
		string Description { get; set; }
		bool allDay { get; set; }
		DateTime start { get; set; }
		DateTime end { get; set; }
		string url { get; set; }
		string RemindMeMediaTypeId { get; set; }
		string RemindMeDelayTypeId { get; set; }
		string className { get; set; }
		bool editable { get; set; }
// ReSharper restore InconsistentNaming
	}

	public class CaFullCalenderAppointmentModel : ICaFullCalendarAppointmentModel
	{
		#region Implementation of ICaFullCalendarAppointmentModel

		public int id { get; set; }
		public string AppointmentTypeId { get; set; }
		public int DealerUserId { get; set; }
		public int DealerId { get; set; }
		public long? LeadId { get; set; }
		public long? CustomerId { get; set; }
		public long? CustomerMasterFileId { get; set; }
		public string title { get; set; }
		public string Description { get; set; }
		public bool allDay { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public string url { get; set; }
		public string RemindMeMediaTypeId { get; set; }
		public string RemindMeDelayTypeId { get; set; }
		public string className { get; set; }
		public bool editable { get; set; }

		#endregion Implementation of ICaFullCalendarAppointmentModel
	}
}