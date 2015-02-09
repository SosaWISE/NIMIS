using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeTicket : IFnsSeTicket
	{
		#region .ctor
		public FnsSeTicket(SE_Ticket ticket)
		{
			TicketID = ticket.TicketID;
			AccountId = ticket.AccountId;
			//CustomerMasterFileId = ticket.CustomerMasterFileId;
			MonitoringStationNo = ticket.MonitoringStationNo;
			TicketTypeId = ticket.TicketTypeId;
			StatusCodeId = ticket.StatusCodeId;
			MoniConfirmation = ticket.MoniConfirmation;
			TechnicianId = ticket.TechnicianId;
			TripCharges = ticket.TripCharges;
			Appointment = ticket.Appointment;
			AgentConfirmation = ticket.AgentConfirmation;
			ExpirationDate = ticket.ExpirationDate;
			Notes = ticket.Notes;
		}


		public FnsSeTicket(SE_TicketsView ticket)
		{
			TicketID = ticket.TicketID;
			AccountId = ticket.AccountId;
			CustomerMasterFileId = ticket.CustomerMasterFileId;
			MonitoringStationNo = ticket.MonitoringStationNo;
			TicketTypeId = ticket.TicketTypeId;
			TicketTypeName = ticket.TicketTypeName;
			StatusCodeId = ticket.StatusCodeId;
			StatusCode = ticket.StatusCode;
			MoniConfirmation = ticket.MoniConfirmation;
			//TechnicianId = ticket.TechnicianId;
			TripCharges = ticket.TripCharges;
			Appointment = ticket.Appointment;
			AgentConfirmation = ticket.AgentConfirmation;
			ExpirationDate = ticket.ExpirationDate;
			Notes = ticket.Notes;
			IsTechEnRoute = ticket.IsTechEnRoute;
			IsTechDelayed = ticket.IsTechDelayed;
			IsTechCompleted = ticket.IsTechCompleted;
			CustomerFullName = ticket.CustomerFullName;
			Address = ticket.Address;
			CompleteAddress = ticket.CompleteAddress;
			PhoneHome = ticket.PhoneHome;
			PhoneMobile = ticket.PhoneMobile;
			// AppointmentDate = ticket.AppointmentDate;
			// BlockId = ticket.BlockId;
			//  TravelTime = ticket.TravelTime;
			//  ZipCode = ticket.ZipCode;
			//  MaxRadius = ticket.MaxRadius;
			//  Distance = ticket.Distance;
			//  StartTime = ticket.StartTime;
			//  EndTime = ticket.EndTime;
			ClosingNote = ticket.ClosingNote;
			ConfirmationNo = ticket.ConfirmationNo;

		}

		public FnsSeTicket(SE_AccountTicketsView ticket)
		{
			TicketID = ticket.TicketID;
			AccountId = ticket.AccountId;
			//CustomerMasterFileId = ticket.CustomerMasterFileId;
			MonitoringStationNo = ticket.MonitoringStationNo;
			TicketTypeId = ticket.TicketTypeId;
			TicketTypeName = ticket.TicketTypeName;
			StatusCodeId = ticket.StatusCodeId;
			StatusCode = ticket.StatusCode;
			TechnicianId = ticket.TechnicianId;
			TechnicianName = ticket.TechnicianName;
			MoniConfirmation = ticket.MoniConfirmation;
			TripCharges = ticket.TripCharges;
			Appointment = ticket.Appointment;
			AgentConfirmation = ticket.AgentConfirmation;
			ExpirationDate = ticket.ExpirationDate;
			Notes = ticket.Notes;
			IsTechEnRoute = ticket.IsTechEnRoute;
			IsTechDelayed = ticket.IsTechDelayed;
			IsTechCompleted = ticket.IsTechCompleted;
			ClosingNote = ticket.ClosingNote;
			ConfirmationNo = ticket.ConfirmationNo;

		}





		public FnsSeTicket(SE_ScheduleBlockTicketsView ticket)
		{
			TicketID = ticket.TicketID;
			AccountId = ticket.AccountId;
			CustomerMasterFileId = ticket.CustomerMasterFileId;
			MonitoringStationNo = ticket.MonitoringStationNo;
			TicketTypeId = ticket.TicketTypeId;
			TicketTypeName = ticket.TicketTypeName;
			Weight = ticket.Weight;
			StatusCodeId = ticket.StatusCodeId;
			StatusCode = ticket.StatusCode;
			MoniConfirmation = ticket.MoniConfirmation;
			TechnicianId = ticket.TechnicianId;
			TripCharges = ticket.TripCharges;
			Appointment = ticket.Appointment;
			AgentConfirmation = ticket.AgentConfirmation;
			ExpirationDate = ticket.ExpirationDate;
			Notes = ticket.Notes;
			IsTechEnRoute = ticket.IsTechEnRoute;
			IsTechDelayed = ticket.IsTechDelayed;
			IsTechCompleted = ticket.IsTechCompleted;
			CustomerFullName = ticket.CustomerFullName;
			Address = ticket.Address;
			CompleteAddress = ticket.CompleteAddress;
			PhoneHome = ticket.PhoneHome;
			PhoneMobile = ticket.PhoneMobile;
			AppointmentDate = ticket.AppointmentDate;
			BlockId = ticket.BlockId;
			TravelTime = ticket.TravelTime;
			ZipCode = ticket.ZipCode;
			MaxRadius = ticket.MaxRadius;
			Distance = ticket.Distance;
			StartTime = ticket.StartTime;
			EndTime = ticket.EndTime;
			ScheduleTicketId = ticket.ScheduleTicketId;
		}


		public FnsSeTicket(
				long ticketID,
				long accountId,
				long? customerMasterFileId,
				long? moniNumber,
				int ticketTypeId,
				int statusCodeId,
				string moniConfirmation,
				DateTime? techConfirmation,
				string technicianId,
				decimal? tripCharges,
				string appointment,
				string agentConfirmation,
				DateTime? expirationDate,
				string notes
			)
		{
			TicketID = ticketID;
			AccountId = accountId;
			CustomerMasterFileId = customerMasterFileId;
			MonitoringStationNo = moniNumber;
			TicketTypeId = ticketTypeId;
			StatusCodeId = statusCodeId;
			MoniConfirmation = moniConfirmation;
			TechConfirmation = techConfirmation;
			TechnicianId = technicianId;
			TripCharges = tripCharges;
			Appointment = appointment;
			AgentConfirmation = agentConfirmation;
			ExpirationDate = expirationDate;
			Notes = notes;
		}

		public FnsSeTicket(
			   long ticketID,
			   string confirmationNo,
			   string closingNote
		   )
		{
			TicketID = ticketID;
			ConfirmationNo = confirmationNo;
			ClosingNote = closingNote;
		}


		#endregion .ctor

		#region Properties

		public long TicketID { get; set; }

		public long AccountId { get; set; }

		public long? CustomerMasterFileId { get; set; }


		public long? MonitoringStationNo { get; set; }

		public int TicketTypeId { get; set; }

		public string TicketTypeName { get; set; }

		public double? Weight { get; set; }

		public int StatusCodeId { get; set; }

		public string StatusCode { get; set; }

		public string MoniConfirmation { get; set; }

		public DateTime? TechConfirmation { get; set; }

		public string TechnicianId { get; set; }

		public string TechnicianName { get; set; }
		public decimal? TripCharges { get; set; }

		public string Appointment { get; set; }

		public string AgentConfirmation { get; set; }

		public DateTime? ExpirationDate { get; set; }

		public string Notes { get; set; }

		public bool? IsTechEnRoute { get; set; }
		public bool? IsTechDelayed { get; set; }
		public bool? IsTechCompleted { get; set; }

		public string ClosingNote { get; set; }

		public string ConfirmationNo { get; set; }

		//customer info
		public string CustomerFullName { get; set; }
		public string Address { get; set; }
		public string StreetAddress { get; set; }

		public string CityStateZip { get; set; }

		public string CompleteAddress { get; set; }
		public string County { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }


		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneMobile { get; set; }



		//SeScheduleTickets
		public long? BlockId { get; set; }
		public DateTime? AppointmentDate { get; set; }
		public int? TravelTime { get; set; }

		//SeScheduleBlock
		public string ZipCode { get; set; }
		public double? MaxRadius { get; set; }
		public double? Distance { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }


		public long? ScheduleTicketId { get; set; }

		#endregion Properties
	}
}
