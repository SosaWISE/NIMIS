using SOS.Data.SosCrm;
using System;

namespace SOS.FunctionalServices.Models.TicketService
{
	public class ServiceTicket
	{
		public long ID { get; set; }
		//public DateTime CreatedOn { get; set; }
		//public string CreatedBy { get; set; }
		//public DateTime ModifiedOn { get; set; }
		//public string ModifiedBy { get; set; }
		public bool IsDeleted { get; set; }
		public int Version { get; set; }
		public int ServiceTypeId { get; set; }
		public long AccountId { get; set; }
		// current appointment is not set by editing the service ticket
		//public long? CurrentAppointmentId { get; set; }
		public string Notes { get; set; }
		public string AppendNotes { get; set; }
		public long? MSTicketNum { get; set; }

		public string CompletedNote { get; set; }
		public DateTime? CompletedOn { get; set; }
		public string MSConfirmation { get; set; }
		public string DealerConfirmation { get; set; }

		public long? AppointmentId { get; set; }
		public int? TechId { get; set; }
		public DateTime? StartOn { get; set; }
		public DateTime? EndOn { get; set; }
		public int? TravelTime { get; set; }
		public DateTime? TechEnRouteOn { get; set; }

		public long CustomerMasterFileId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string TechGPEmployeeID { get; set; }

		public ServiceTicket() { }

		public ServiceTicket(TS_ServiceTicketStatusView item)
		{
			this.ID = item.ID;
			//this.CreatedOn = item.CreatedOn;
			//this.CreatedBy = item.CreatedBy;
			//this.ModifiedOn = item.ModifiedOn;
			//this.ModifiedBy = item.ModifiedBy;
			this.IsDeleted = item.IsDeleted;
			this.Version = item.Version;
			this.ServiceTypeId = item.ServiceTypeId;
			this.AccountId = item.AccountId;
			//this.CurrentAppointmentId = item.CurrentAppointmentId;
			this.Notes = item.Notes;
			this.MSTicketNum = item.MSTicketNum;
			
			this.CompletedNote = item.CompletedNote;
			this.CompletedOn = item.CompletedOn;
			this.MSConfirmation = item.MSConfirmation;
			this.DealerConfirmation = item.DealerConfirmation;

			this.AppointmentId = item.AppointmentId;
			this.TechId = item.TechId;
			this.StartOn = item.StartOn;
			this.EndOn = item.EndOn;
			this.TravelTime = item.TravelTime;
			this.TechEnRouteOn = item.TechEnRouteOn;

			this.CustomerMasterFileId = item.CustomerMasterFileId;
			this.FirstName = item.FirstName;
			this.MiddleName = item.MiddleName;
			this.LastName = item.LastName;
			this.StreetAddress = item.StreetAddress;
			this.StreetAddress2 = item.StreetAddress2;
			this.City = item.City;
			this.StateId = item.StateId;
			this.PostalCode = item.PostalCode;
			this.Latitude = item.Latitude;
			this.Longitude = item.Longitude;
			this.TechGPEmployeeID = item.TechGPEmployeeID;
		}

		public void ToDb(TS_ServiceTicket item)//, TS_Appointment appt)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			SOS.Data.VersionException.CheckVersions(item.Version, this.Version);
			item.Version++; // increment version

			//item.ID = this.ID;
			// don't copy over record keeping fields
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			item.IsDeleted = this.IsDeleted;

			item.ServiceTypeId = this.ServiceTypeId;
			item.AccountId = this.AccountId;
			// current appointment is not set by editing the service ticket
			//item.CurrentAppointmentId = this.CurrentAppointmentId;
			{ // append notes
				item.Notes += "\n" + (this.AppendNotes ?? "").Trim();
				item.Notes = item.Notes.Trim();
			}
			item.MSTicketNum = this.MSTicketNum;

			//item.CompletedNote = this.CompletedNote;
			//item.CompletedOn = this.CompletedOn;
			//item.MSConfirmation = this.MSConfirmation;
			//item.DealerConfirmation = this.DealerConfirmation;

			//item.CustomerMasterFileId = this.CustomerMasterFileId;
			//item.FirstName = this.FirstName;
			//item.MiddleName = this.MiddleName;
			//item.LastName = this.LastName;
			//item.StreetAddress = this.StreetAddress;
			//item.StreetAddress2 = this.StreetAddress2;
			//item.City = this.City;
			//item.StateId = this.StateId;
			//item.PostalCode = this.PostalCode;
			//item.Latitude = this.Latitude;
			//item.Longitude = this.Longitude;
			//appt.TechGPEmployeeID = this.TechGPEmployeeID;
		}
		public void ToDbAppt(TS_Appointment item)
		{
			if (this.AppointmentId.HasValue && item.ID != this.AppointmentId.Value)
				throw new Exception("IDs don't match");
			else if (!this.AppointmentId.HasValue && item.ID != 0)
				throw new Exception("IDs don't match");
			//item.ID = this.AppointmentId.Value;
			item.TechId = this.TechId.Value;
			item.StartOn = this.StartOn.Value;
			item.EndOn = this.EndOn.Value;
			item.TravelTime = this.TravelTime.Value;
			item.TechEnRouteOn = this.TechEnRouteOn;
		}
	}
}
