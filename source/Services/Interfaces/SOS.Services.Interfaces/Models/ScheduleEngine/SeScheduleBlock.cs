using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeScheduleBlock : IFnsScheduleBlock
	{

        public long BlockID { get; set; }

        public string Block { get; set; }

   
        public string ZipCode { get; set; }


        public double? MaxRadius { get; set; }

     
        public double? Distance { get; set; }

        public int? AvailableSlots { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string TechnicianId { get; set; }

        public string TechnicianName { get; set; }

        public bool? IsTechConfirmed { get; set; }


        public double? BlockLatitude { get;set; }
        public double? BlockLongitude { get; set; }
        public double? TicketLongitude { get; set; }
        public double? TicketLatitude { get; set; }

        public long? CurrentTicketId { get; set; }

        public bool? IsRed { get; set; }

        public DateTime? DateTechConfirmed { get; set; }

        public string Color { get; set; }

        public bool IsBlocked { get; set; }

        public int? NoOfTickets { get; set; }


        public List<SeTicket> TicketList { get; set; }
	}

    public interface IFnsScheduleBlock
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
        DateTime? DateTechConfirmed { get; set; }

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
        bool? IsRed { get; set; }

        [DataMember]
        bool IsBlocked { get; set; }

        [DataMember]
        string Color { get; set; }

        [DataMember]
        int? NoOfTickets { get; set; }
	}
}
