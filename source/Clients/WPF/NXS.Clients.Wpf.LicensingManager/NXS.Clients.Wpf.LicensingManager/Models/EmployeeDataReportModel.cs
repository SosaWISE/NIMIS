using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Validation;

namespace NXS.Clients.Wpf.LicensingManager.Models
{
	public class EmployeeDataReportModel : ModelBase
	{
		#region Properties
		public ValidatedInput<string> FirstName { get; private set; }
		public ValidatedInput<string> LastName { get; private set; }
		public ValidatedListInput<string, GPDepartmentModel> CorpDepartment { get; private set; }
		public ValidatedListInput<int?, GeneralModel> ActiveOption { get; private set; }
		#endregion Properties

		#region Constructors
		public EmployeeDataReportModel()
			: base()
		{
			AddInput(FirstName = new ValidatedInput<string>() 
			{
 				Validator = StringLengthValidator.Create(0, 50)
			});
			AddInput(LastName = new ValidatedInput<string>()
			{
				Validator = StringLengthValidator.Create(0, 50)
			});
			this.AddInput(CorpDepartment = new ValidatedListInput<string, GPDepartmentModel>((item, id) => item.DepartmentID.Value == id)
			{
				List = GlobalCache.GPCorpDepartments.List,
				SelectedValuePath = "DepartmentID.Value",
				DisplayMemberPath = "DeptName.Value",
				CanClear = true
			});
			this.AddInput(ActiveOption = new ValidatedListInput<int?, GeneralModel>((item, id) => item.ID.Value == id)
			{
				Validator = MandatoryObjectValidator<int?>.Create(),
				List = GlobalCache.ActiveReasons.List,
				SelectedValuePath = "ID.Value",
				DisplayMemberPath = "ItemName.Value",
			});

			RunValidation();
			Clean();
		}
		#endregion Constructors

		#region Methods
		#endregion Methods
	}
}
