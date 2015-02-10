using System;
using NXS.Data.GreatPlains;
using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;
using System.Windows.Input;
using NXS.Clients.Wpf.LicensingManager.Models;
using System.Collections.ObjectModel;
using System.Data;
using OfficeOpenXml;
using System.IO;
using SOS.Lib.Util;

namespace NXS.Clients.Wpf.LicensingManager.ViewModels
{
	public class EmployeeDataReportViewModel : CloseableViewModel
	{
		#region Properties
		private RelayCommand<object> _loadReportCommand;
		private RelayCommand<object> _clearCommand;
		private RelayCommand<object> _exportToExcelCommand;

		public EmployeeDataReportModel Model { get; private set; }
		public ObservableCollection<EmployeeDataModel> List { get; private set; }
		public ObservableValueContainer<bool> IsLoading { get; private set; }

		public ICommand LoadReportCommand
		{
			get { return _loadReportCommand ?? (_loadReportCommand = new RelayCommand<object>(param => LoadReport(), param => Model.IsValid)); }
		}
		public ICommand ClearCommand
		{
			get { return _clearCommand ?? (_clearCommand = new RelayCommand<object>(param => ClearSearchOptions())); }
		}
		public ICommand ExportToExcelCommand
		{
			get { return _exportToExcelCommand ?? (_exportToExcelCommand = new RelayCommand<object>(param => ExportToExcel(), param => List.Count > 0)); }
		}
		#endregion Properties

		#region Constructors
		public EmployeeDataReportViewModel(ParameterDictionary args)
			: base(args)
		{
			DisplayName = ActionNamesSimple.EmployeeInfo;
			IsLoading = new ObservableValueContainer<bool>();
			Model = new EmployeeDataReportModel();
			List = new ObservableCollection<EmployeeDataModel>();
		}
		#endregion Constructors

		#region Methods
		public void LoadReport()
		{
			IsLoading.Value = true;
			List.Clear();

// ReSharper disable once RedundantTypeArgumentsOfMethod
			AsyncHelper.LoadAndTransformDataAsync<DataRow, EmployeeDataModel>(
				delegate
				{
					bool? inactive = null;
					switch(Model.ActiveOption.Value)
					{
						case 2:
							inactive = false;
							break;
						case 3:
							inactive = true;
							break;
					}
 					string firstName = Model.FirstName.Value == null ? null : Model.FirstName.Value.Trim();
					string lastName = Model.LastName.Value == null ? null : Model.LastName.Value.Trim();

					return GreatPlainsStoredProcedureManager.GetEmployeeInfo(inactive, firstName, lastName, Model.CorpDepartment.Value ).GetDataSet().Tables[0].AsEnumerable();
				},
				delegate(DataRow row)
				{
					var employeeDataModel = new EmployeeDataModel();
					employeeDataModel.GPEmployeeID.Value = row["GP ID"].ToString();
					employeeDataModel.FirstName.Value = row["FirstName"].ToString();
					employeeDataModel.MiddleInitial.Value = (string)row["MI"];
					employeeDataModel.LastName.Value = (string)row["LastName"];
					employeeDataModel.Active.Value = !Convert.ToBoolean(row["Inactive"]);
					employeeDataModel.InactiveDate.Value = (DateTime)row["InactiveDate"];
					employeeDataModel.MaritalStatus.Value = row["Marital Status"] as string;
					var ssn = (string)row["SSN"];
					employeeDataModel.SSN.Value = row["SSN"] != DBNull.Value? ssn.Substring(0, 3) + "-" + ssn.Substring(3,2) + "-" + ssn.Substring(5, 4) : (string)row["SSN"];
					employeeDataModel.Gender.Value = (string)row["Gender"];
					employeeDataModel.Department.Value = (string)row["Department"];
					employeeDataModel.BirthDate.Value = (DateTime)row["BirthDate"];
					employeeDataModel.StartDate.Value = (DateTime)row["StartDate"];
					employeeDataModel.EmploymentType.Value = (string)row["Employment Type"];
					employeeDataModel.StreetAddress.Value = (string)row["StreetAddress"];
					employeeDataModel.City.Value =(string)row["City"];
					employeeDataModel.State.Value = (string)row["State"];
					employeeDataModel.Zip.Value = (string)row["Zip"];
					var phone = (string)row["Phone"];
					employeeDataModel.Phone.Value = "(" + phone.Substring(0,3) + ")" + phone.Substring(3, 3) + "-" + phone.Substring(6,4);
					return employeeDataModel;
				},
				List,
				delegate
				{
					IsLoading.Value = false;
				}
				, delegate(Exception ex)
				{
					ErrorManager.AddCriticalMessage(ex);
					DisplayErrorMessages();
					IsLoading.Value = false;
				});
		}

		public void ClearSearchOptions()
		{
			Model.Reset();
			Model.Clean();
		}

		public void ExportToExcel()
		{
			var dlg = new System.Windows.Forms.SaveFileDialog();
			dlg.DefaultExt = ".xlsx";
// ReSharper disable once LocalizableElement
			dlg.Filter = "Excel Files (.xlsx)|*.xlsx";

			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				IsLoading.Value = true;
				string fileName = dlg.FileName;
				AsyncHelper.ExecuteAsync(
					delegate
					{
						var excelPkg = new ExcelPackage();

						ExcelWorksheet worksheet = excelPkg.Workbook.Worksheets.Add(ActionNamesSimple.EmployeeInfo);

						//Add Headers
						worksheet.Cells[1, 1].Value = "GP ID";
						worksheet.Cells[1, 2].Value = "First Name";
						worksheet.Cells[1, 3].Value = "Middle Initial";
						worksheet.Cells[1, 4].Value = "Last Name";
						worksheet.Cells[1, 5].Value = "Phone #";
						worksheet.Cells[1, 6].Value = "Department";
						worksheet.Cells[1, 7].Value = "Active";
						worksheet.Cells[1, 8].Value = "Inactive Date";
						worksheet.Cells[1, 9].Value = "Marital Status";
						worksheet.Cells[1, 10].Value = "Social Security #";
						worksheet.Cells[1, 11].Value = "Gender";
						worksheet.Cells[1, 12].Value = "Birth Date";
						worksheet.Cells[1, 13].Value = "Start Date";
						worksheet.Cells[1, 14].Value = "Employment Type";
						worksheet.Cells[1, 15].Value = "Street Address";
						worksheet.Cells[1, 16].Value = "City";
						worksheet.Cells[1, 17].Value = "State";
						worksheet.Cells[1, 18].Value = "Zip";


						//Add all other data
						int row = 2;
						foreach (EmployeeDataModel item in List)
						{
							worksheet.Cells[row, 1].Value = item.GPEmployeeID.Value;
							worksheet.Cells[row, 2].Value = item.FirstName.Value;
							worksheet.Cells[row, 3].Value = item.MiddleInitial.Value;
							worksheet.Cells[row, 4].Value = item.LastName.Value;
							worksheet.Cells[row, 5].Value = item.Phone.Value;
							worksheet.Cells[row, 6].Value = item.Department.Value;
							worksheet.Cells[row, 7].Value = item.Active.Value ? "YES" : "NO";
							worksheet.Cells[row, 8].Value = item.Active.Value ? string.Empty : item.InactiveDate.Value.ToShortDateString();
							worksheet.Cells[row, 9].Value = item.MaritalStatus.Value;
							worksheet.Cells[row, 10].Value = item.SSN.Value;
							worksheet.Cells[row, 11].Value = item.Gender.Value;
							worksheet.Cells[row, 12].Value = item.BirthDate.Value;
							worksheet.Cells[row, 13].Value = item.StartDate.Value;
							worksheet.Cells[row, 14].Value = item.EmploymentType.Value;
							worksheet.Cells[row, 15].Value = item.StreetAddress.Value;
							worksheet.Cells[row, 16].Value = item.City.Value;
							worksheet.Cells[row, 17].Value = item.State.Value;
							worksheet.Cells[row, 18].Value = item.Zip.Value;

							row = row + 1;
						}


						using (Stream writer = new FileStream(fileName, FileMode.Create))
						{
							byte[] bytes = excelPkg.GetAsByteArray();
							writer.Write(bytes, 0, bytes.Length);
						}
					}, delegate
					{
						IsLoading.Value = false;
					}, delegate(Exception ex)
					{
						ErrorManager.AddCriticalMessage(ex);
						DisplayErrorMessages();
						IsLoading.Value = false;
					});
			}
		}
		#endregion Methods
	}
}
