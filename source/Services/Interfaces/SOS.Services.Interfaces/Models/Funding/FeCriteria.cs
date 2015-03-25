using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Funding
{
	public class FeCriteria : IFeCriteria
	{
		public int CriteriaID { get; set; }
		public string PurchaserId { get; set; }
		public string PurchaserName { get; set; }
		public string CriteriaName { get; set; }
		public string Description { get; set; }
		public string FilterString { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
	}

	public interface IFeCriteria
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
