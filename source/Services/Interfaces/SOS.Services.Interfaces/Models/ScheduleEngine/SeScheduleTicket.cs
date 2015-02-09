using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeScheduleTicket : IFnsScheduleTicket
	{
        public long ScheduleTicketID { get; set; }

        public long TicketId { get; set; }

        public long BlockId { get; set; }


        public DateTime AppointmentDate { get; set; }



        public int TravelTime { get; set; }
	}

    public interface IFnsScheduleTicket
	{

        [DataMember]
        long ScheduleTicketID { get; set; }
        
        [DataMember]
        long TicketId { get; set; }

        [DataMember]
        long BlockId { get; set; }

        [DataMember]
        DateTime AppointmentDate { get; set; }

 

        [DataMember]
        int TravelTime { get; set; }

    
	}
}
