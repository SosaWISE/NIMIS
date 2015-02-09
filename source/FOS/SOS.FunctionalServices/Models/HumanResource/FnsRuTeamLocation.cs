using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuTeamLocation : IFnsRuTeamLocation
	{
		#region .ctor

		public FnsRuTeamLocation(RU_TeamLocation ruTeamLocation)
		{
            TeamLocationID = ruTeamLocation.TeamLocationID;
            City = ruTeamLocation.City;

		}
		#endregion .ctor

		#region Properties

		public int TeamLocationID { get; set; }
		public string City { get; set; }
		

		#endregion Properties
	}
}
