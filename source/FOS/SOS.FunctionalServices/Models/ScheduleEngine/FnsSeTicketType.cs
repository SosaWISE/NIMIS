using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeTicketType : IFnsSeTicketType
	{
		#region .ctor
        public FnsSeTicketType(SE_TicketType ticketType)
		{
            TicketTypeID = ticketType.TicketTypeID;
            TicketTypeName = ticketType.TicketTypeName;

		}

		#endregion .ctor

		#region Properties
   
		public int TicketTypeID { get; set; }
        public string TicketTypeName { get; set; }

		#endregion Properties
	}
}
