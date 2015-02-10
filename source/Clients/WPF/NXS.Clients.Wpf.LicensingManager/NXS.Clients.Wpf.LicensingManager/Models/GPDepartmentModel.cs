using NXS.Data.GreatPlains;
using NXS.Framework.Wpf.Mvvm;
using System.Collections.Generic;
using System.Data;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class GPDepartmentModel : ModelBase
	{
		#region Properties
		public ObservableValueContainer<string> DepartmentID { get; private set; }
		public ObservableValueContainer<string> DeptName { get; private set; }
		#endregion Properties

		#region Constructors
		public GPDepartmentModel()
		{
			DepartmentID = new ObservableValueContainer<string>();
			DeptName = new ObservableValueContainer<string>();
		}
		#endregion Constructors

		#region Methods
		public static List<GPDepartmentModel> GetEmployeeDepartments()
		{
			var list = new List<GPDepartmentModel>();

			DataTable table = GreatPlainsStoredProcedureManager.GetCorpDepartments().GetDataSet().Tables[0];

			foreach (DataRow item in table.Rows)
			{
				var model = new GPDepartmentModel();
				model.DepartmentID.Value = item["JOBTITLE"].ToString();
				model.DeptName.Value = item["DSCRIPTN"].ToString();
				list.Add(model);
			}

			return list;
		}
		#endregion Methods
	}
}
