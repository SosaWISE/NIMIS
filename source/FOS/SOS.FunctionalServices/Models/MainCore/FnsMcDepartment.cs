using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Models.MainCore
{
	public class FnsMcDepartment : IFnsMcDepartment
	{
		#region ctor

		public FnsMcDepartment(MC_Department department)
		{
			DepartmentID = department.DepartmentID;
			DepartmentName = department.DepartmentName;
		}

		#endregion ctor

		#region Properties
		public string DepartmentID { get; private set; }
		public string DepartmentName { get; private set; }
		#endregion Properties
	}
}
