using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuTeamLocation
	{
		[DataMember]
		int TeamLocationID { get; set; }
		[DataMember]
		string City { get; set; }
	
	}
}
