using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeTicketStatusCode : IFnsSeTicketStatusCode
	{
		#region .ctor
        public FnsSeTicketStatusCode(SE_TicketStatusCode ticketStatusCode)
		{
            StatusCodeID = ticketStatusCode.StatusCodeID;
            StatusCode = ticketStatusCode.StatusCode;

		}

		#endregion .ctor

		#region Properties
   
		public int StatusCodeID { get; set; }
        public string StatusCode { get; set; }

		#endregion Properties
	}
}
