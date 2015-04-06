using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Funding
{
	public interface IFnsFeCriteria
	{
		[DataMember]
		int CriteriaID { get; set; }

		[DataMember]
		string PurchaserId { get; set; }

		[DataMember]
		string PurchaserName { get; set; }

		[DataMember]
		string CriteriaName { get; set; }

		[DataMember]
		string Description { get; set; }

		[DataMember]
		string FilterString { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }
	}
}