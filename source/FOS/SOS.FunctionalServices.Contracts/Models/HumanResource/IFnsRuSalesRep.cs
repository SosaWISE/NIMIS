using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuSalesRep
	{
		[DataMember]
		string SalesRepId { get; set; }
		[DataMember]
		string FullName { get; set; }  // Used to post a Tech to an account.
		[DataMember]
		string RepFirstName { get; set; }
		[DataMember]
		string RepLastName { get; set; }
		[DataMember]
		DateTime? RepBirthDate { get; set; }
		[DataMember]
		int RepSeasonId { get; set; }
		[DataMember]
		string RepSeasonName { get; set; }
		 
	}
}