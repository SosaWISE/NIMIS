using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsConnextSalesRepInfo
	{
		int UserID { get;  }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
        string PhotoURL { get; }
		long? MLMDepth { get; }
		bool? ManagerHasOwnTeam { get; }
	    string RegionName { get; }
        string OfficeName { get; }
		string TeamName { get; }
	    long? CurrentNationalRank { get;  }
        long? PreviousNationalRank { get; }
        long? CurrentRegionalRank { get;  }
	    long? PreviousRegionalRank { get;  }
	    long? CurrentOfficeRank { get;  }
	    long? PreviousOfficeRank { get;  }
	    long? CurrentTeamRank { get;  }
	    long? PreviousTeamRank { get;  }
        DateTime? StartDate { get;  }
		string Email { get;  }
	}
}