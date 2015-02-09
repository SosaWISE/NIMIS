using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.MainCore
{
	public interface IFnsMcDepartment
	{
		[DataMember]
		string DepartmentID { get; }
 
		[DataMember]
		string DepartmentName { get; }
	}
}