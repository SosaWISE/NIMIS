using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.Licensing
{
	public interface IFnsLmSalesRepRequirementsView
	{
		[DataMember]
		int RequirementID { get; }
		[DataMember]
		string RequirementTypeName { get; }
		[DataMember]
		string LocationTypeName { get; }
		[DataMember]
		string RequirementName { get; }
		[DataMember]
		int LockID { get; }
		[DataMember]
		string LockTypeName { get; }
		[DataMember]
		string CallCenterMessage { get; }
		[DataMember]
		string Status { get; }
		[DataMember]
		int? LicenseID { get; }
	}
}