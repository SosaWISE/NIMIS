using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuTechnician
	{
        [DataMember]
        string TechnicianId { get; set; }
        [DataMember]
        string FullName { get; set; }  // Used to post a Tech to an account.
        [DataMember]
        string TechFirstName { get; set; }
        [DataMember]
        string TechLastName { get; set; }
        [DataMember]
        DateTime? TechBirthDate { get; set; }
        [DataMember]
        int TechSeasonId { get; set; }
        [DataMember]
        string TechSeasonName { get; set; }
	}
}
