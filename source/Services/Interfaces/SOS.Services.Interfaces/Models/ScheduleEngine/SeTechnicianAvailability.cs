using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.ScheduleEngine
{
    public class SeTechnicianAvailability : ISeTechnicianAvailability
	{
        public long TechnicianAvailabilityID { get; set; }

        public string TechnicianId { get; set; }
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

     
	}

    public interface ISeTechnicianAvailability
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
