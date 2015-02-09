using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeScheduleTicket : IFnsSeScheduleTicket
	{
		#region .ctor
        public FnsSeScheduleTicket(SE_ScheduleTicket scheduleTicket)
		{
            ScheduleTicketID= scheduleTicket.ScheduleTicketID;
            TicketId=scheduleTicket.TicketId;
            BlockId = scheduleTicket.BlockId;
            AppointmentDate=scheduleTicket.AppointmentDate;
            TravelTime = scheduleTicket.TravelTime;
    
		}


        public FnsSeScheduleTicket(
                    long ticketId,
                    long blockId,
                    DateTime appointmentDate,
                    int travelTime

            )
        {
            TicketId = ticketId;
            BlockId = blockId;
            AppointmentDate = appointmentDate;
            TravelTime = travelTime;
        }



		#endregion .ctor

		#region Properties
        public long ScheduleTicketID { get; set; }

        public long TicketId { get; set; }

        public long BlockId { get; set; }

        public DateTime AppointmentDate { get; set; }


        public int TravelTime { get; set; }

		#endregion Properties
	}
}
