using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeScheduleTicket
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