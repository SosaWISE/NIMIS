using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuTechnician : IRuTechnician
	{
		#region Properties
        public string TechnicianId { get; set; }
        public string FullName { get; set; }
        public string TechFirstName { get; set; }
        public string TechLastName { get; set; }
        public DateTime? TechBirthDate { get; set; }
        public int TechSeasonId { get; set; }
        public string TechSeasonName { get; set; }
		
		#endregion Properties
	}

    public interface IRuTechnician
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
