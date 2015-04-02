using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Licensing
{
	public class LmSalesRepRequirement : ILmSalesRepRequirement
	{
		public int RequirementID { get; set; }
		public string RequirementTypeName { get; set; }
		public string LocationTypeName { get; set; }
		public string RequirementName { get; set; }
		public int LockID { get; set; }
		public string LockTypeName { get; set; }
		public string CallCenterMessage { get; set; }
		public string Status { get; set; }
		public int? LicenseID { get; set; }
	}

	public interface ILmSalesRepRequirement
	{
		#region Properties
		[DataMember]
		int RequirementID { get; set; }
		[DataMember]
		string RequirementTypeName { get; set; }
		[DataMember]
		string LocationTypeName { get; set; }
		[DataMember]
		string RequirementName { get; set; }
		[DataMember]
		int LockID { get; set; }
		[DataMember]
		string LockTypeName { get; set; }
		[DataMember]
		string CallCenterMessage { get; set; }
		[DataMember]
		string Status { get; set; }
		[DataMember]
		int? LicenseID { get; set; }
		#endregion Properties
	}
}
