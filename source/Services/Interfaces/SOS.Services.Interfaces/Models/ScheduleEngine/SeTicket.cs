using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeTicket : ISeTicket
	{
        public long TicketID { get; set; }

        public long AccountId { get; set; }

        public long? CustomerMasterFileId { get; set; }

        public long? MonitoringStationNo { get; set; }

        public int TicketTypeId {get; set;}

        public string TicketTypeName { get; set; }

        public double? Weight { get; set; }

        public int StatusCodeId {get;set;}

        public string StatusCode { get; set; }

        public string MoniConfirmation {get;set;}

        public DateTime? TechConfirmation {get;set;}
        
		public string TechnicianId {get;set;}

        public string TechnicianName { get; set; }
		
        public decimal? TripCharges {get;set;}
	
        public string Appointment {get;set;}

        public string AgentConfirmation {get;set;}

        public DateTime? ExpirationDate {get;set;}

        public string Notes { get; set; }

        public bool? IsTechEnRoute { get; set; }
        public bool? IsTechDelayed { get; set; }
        public bool? IsTechCompleted { get; set; }

        public string ConfirmationNo { get; set; }

        public string ClosingNote { get; set; }

        //customer info
        public string CustomerFullName { get; set; }

        public string PhoneHome { get; set; }
        public string PhoneMobile { get; set; }
        public string Address { get; set; }

        public string CompleteAddress { get; set; }
        public string StreetAddress { get; set; }
        public string CityStateZip { get; set; }

        public string County { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }


        public float Latitude { get; set; }
        public float Longitude { get; set; }

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

	}

    public interface ISeTicket
	{

        [DataMember]
        long TicketID { get; set; }
        
        [DataMember]
        long AccountId { get; set; }

        [DataMember]
        long? CustomerMasterFileId { get; set; }

        [DataMember]
        long? MonitoringStationNo { get; set; }

        [DataMember]
        int TicketTypeId { get; set; }

        [DataMember]
        string TicketTypeName { get; set; }

        [DataMember]
        double? Weight { get; set; }



        [DataMember]
        int StatusCodeId { get; set; }

        [DataMember]
        string StatusCode { get; set; }

        [DataMember]
        string MoniConfirmation { get; set; }

        [DataMember]
        DateTime? TechConfirmation { get; set; }

        [DataMember]
        string TechnicianId { get; set; }

        [DataMember]
        decimal? TripCharges { get; set; }

        [DataMember]
        string Appointment { get; set; }

        [DataMember]
        string AgentConfirmation { get; set; }

        [DataMember]
         DateTime? ExpirationDate { get; set; }

        [DataMember]
         string Notes { get; set; }

        [DataMember]
         bool? IsTechEnRoute { get; set; }
        
        [DataMember]
         bool? IsTechDelayed { get; set; }
        [DataMember]
         bool? IsTechCompleted { get; set; }

        [DataMember]
        long? ScheduleTicketId { get; set; }
    
	}
}
