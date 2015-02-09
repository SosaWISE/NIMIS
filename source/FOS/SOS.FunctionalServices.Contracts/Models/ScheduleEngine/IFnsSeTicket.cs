using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeTicket
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
        string TechnicianName { get; set; }

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
        string ConfirmationNo { get; set; }


        [DataMember]
        string ClosingNote { get; set; }


        //customer info
         [DataMember]
        string CustomerFullName { get; set; }
         [DataMember]
        string Address { get; set; }
         [DataMember]
         string StreetAddress { get; set; }
         [DataMember]
         string CityStateZip { get; set; }
         [DataMember]
         string County { get; set; }
         [DataMember]
         string State { get; set; }
         [DataMember]
         string PostalCode { get; set; }
         [DataMember]
         string CompleteAddress { get; set; }

         [DataMember]
         float Latitude { get; set; }

         [DataMember]
         float Longitude { get; set; }


         [DataMember]
         string PhoneHome { get; set; }
         [DataMember]
         string PhoneMobile { get; set; }

        //SeScheduleTickets
         [DataMember]
        long? BlockId { get; set; }
         [DataMember]
        DateTime? AppointmentDate { get; set; }
        [DataMember]
        int? TravelTime { get; set; }

        //SeScheduleBlock
         [DataMember]
        string ZipCode { get; set; }
         [DataMember]
        double? MaxRadius { get; set; }
         [DataMember]
        double? Distance { get; set; }
         [DataMember]
        DateTime? StartTime { get; set; }
         [DataMember]
        DateTime? EndTime { get; set; }

         [DataMember]
         long? ScheduleTicketId { get; set; }

	}
}