using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeTicketStatusCode : ISeTicketStatusCode
	{
        public int StatusCodeID { get; set; }

        public string StatusCode { get; set; }

     
	}

    public interface ISeTicketStatusCode
	{

        [DataMember]
        int StatusCodeID { get; set; }

        [DataMember]
        string StatusCode { get; set; }

    
	}
}
