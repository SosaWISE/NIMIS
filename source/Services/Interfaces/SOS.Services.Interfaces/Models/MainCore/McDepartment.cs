using System.Runtime.Serialization;


namespace SOS.Services.Interfaces.Models.MainCore
{
	public class McDepartment
	{
		[DataMember]
		public string DepartmentID { get; set; }

		[DataMember]
		public string DepartmentName { get; set; }
	}
}
