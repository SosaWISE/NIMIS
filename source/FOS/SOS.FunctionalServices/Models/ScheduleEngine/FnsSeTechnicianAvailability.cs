using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeTechnicianAvailability : IFnsSeTechnicianAvailability
	{
		#region .ctor
        public FnsSeTechnicianAvailability(SE_TechnicianAvailability technicianAvailability)
		{
            TechnicianAvailabilityID = technicianAvailability.TechnicianAvailabilityID;
            TechnicianId = technicianAvailability.TechnicianId;
            StartDateTime = technicianAvailability.StartDateTime;
            EndDateTime = technicianAvailability.EndDateTime;
		}


        public FnsSeTechnicianAvailability(
                long technicianAvailabilityID,
                string technicianId,
                DateTime startDateTime,
                DateTime endDateTime
            )
        {
            TechnicianAvailabilityID = technicianAvailabilityID;
            TechnicianId = technicianId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }



		#endregion .ctor

		#region Properties

        public long TechnicianAvailabilityID { get; set; }

        public string TechnicianId { get; set; }
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }


		#endregion Properties
	}
}
