using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.ScheduleEngine
{
	public interface IFnsSeTechnicianAvailability
	{
        [DataMember]
        long TechnicianAvailabilityID { get; set; }

        [DataMember]
        string TechnicianId { get; set; }

        [DataMember]
        DateTime StartDateTime { get; set; }

        [DataMember]
        DateTime EndDateTime { get; set; }
       
	}
}