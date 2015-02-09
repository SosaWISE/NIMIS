using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeTicketStatusCode
	{
        [DataMember]
        int StatusCodeID { get; set; }

        [DataMember]
        string StatusCode { get; set; }

     
       
	}
}