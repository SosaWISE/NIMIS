using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsVendorAlarmComPackage
	{
		[DataMember]
		string AlarmComPackageID { get; set; }
		
		[DataMember]
		int AlarmComAccountId { get; set; }
		
		[DataMember]
		string PackageName { get; set; }

		[DataMember]
		bool DefaultValue { get; set; }

		[DataMember]
		bool IsActive { get; set; }

		[DataMember]
		bool IsDeleted { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }

		[DataMember]
		DateTime DexRowTs { get; set; }
	}
}