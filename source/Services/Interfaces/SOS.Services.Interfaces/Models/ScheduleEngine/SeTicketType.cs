using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeTicketType : ITicketType
	{
        public int TicketTypeID { get; set; }

        public string TicketTypeName { get; set; }

     
	}

    public interface ITicketType
	{

        [DataMember]
        int TicketTypeID { get; set; }

        [DataMember]
        string TicketTypeName { get; set; }

    
	}
}
