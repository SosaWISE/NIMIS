using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeScheduleBlock
	{
        [DataMember]
        long BlockID { get; set; }

        [DataMember]
        string Block { get; set; }

        [DataMember]
        string ZipCode { get; set; }

        [DataMember]
        double? MaxRadius { get; set; }

        [DataMember]
        double? Distance { get; set; }


        [DataMember]
        int? AvailableSlots { get; set; }

        [DataMember]
        DateTime? StartTime { get; set; }

        [DataMember]
        DateTime? EndTime { get; set; }

        [DataMember]
        string TechnicianId { get; set; }

        [DataMember]
        string TechnicianName { get; set; }

        [DataMember]
        bool? IsTechConfirmed { get; set; }

        [DataMember]
        DateTime? DateTechConfirmed  { get; set; }


        [DataMember]
        double? BlockLatitude { get; set; }
        [DataMember]
        double? BlockLongitude { get; set; }
        [DataMember]
        double? TicketLongitude { get; set; }
        [DataMember]
        double? TicketLatitude { get; set; }
        [DataMember]
        long? CurrentTicketId { get; set; } 




        [DataMember]
        bool? IsRed  { get; set; }


        [DataMember]
        bool IsBlocked { get; set; }

        [DataMember]
        string Color { get; set; }

        [DataMember]
        int? NoOfTickets { get; set; }

   
        [DataMember]
        List<IFnsSeTicket> TicketList { get; set; }
	}
}