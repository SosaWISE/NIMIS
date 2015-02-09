using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeTicketType
	{
        [DataMember]
        int TicketTypeID { get; set; }

        [DataMember]
        string TicketTypeName { get; set; }

     
       
	}
}