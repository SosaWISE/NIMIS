using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXS.Framework.Wpf.Mvvm;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;
using System.Windows.Input;
using NXS.Clients.Wpf.LicensingManager.Models;
using System.Collections.ObjectModel;
using SOS.Lib.Util;
using System.Data;
using SOS.Data.SosCrm;
using System.IO;
using OfficeOpenXml;

namespace NXS.Clients.Wpf.LicensingManager.ViewModels
{
	public class PurchasedAccountsReportViewModel : CloseableViewModel
	{
		#region Properties
		private RelayCommand<object> loadReportCommand;
		private RelayCommand<object> clearCommand;
		private RelayCommand<object> exportToExcelCommand;

		public ObservableValueContainer<bool> IsLoading { get; private set; }
		public ObservableCollection<PurchasedAccountsModel> List { get; private set; }
		public PurchasedAccountsReportModel Model { get; private set; }

		public ICommand LoadReportCommand
		{
			get { return loadReportCommand ?? (loadReportCommand = new RelayCommand<object>(param => LoadReport(), param => Model.IsValid && 
				!(Model.PurchaseEndDate.Value == null && Model.PurchaseStartDate.Value == null && string.IsNullOrEmpty(Model.City.Value) && string.IsNullOrEmpty(Model.County.Value) && Model.State.Value == null))); }
		}
		public ICommand ClearCommand
		{
			get { return clearCommand ?? (clearCommand = new RelayCommand<object>(param => ClearSearchOptions())); }
		}
		public ICommand ExportToExcelCommand
		{
			get { return exportToExcelCommand ?? (exportToExcelCommand = new RelayCommand<object>(param => ExportToExcel(), param => List.Count > 0)); }
		}
		#endregion Properties

		#region Constructors
		public PurchasedAccountsReportViewModel(ParameterDictionary args)
			: base(args)
		{
			IsLoading = new ObservableValueContainer<bool>();
			Model = new PurchasedAccountsReportModel();
			List = new ObservableCollection<PurchasedAccountsModel>();
			DisplayName = ActionNamesSimple.PurchasedAccountsReport;
		}
		#endregion Constructors

		#region Methods
		public void LoadReport()
		{
			IsLoading.Value = true;
			List.Clear();

			AsyncHelper.LoadAndTransformDataAsync<DataRow, PurchasedAccountsModel>(
				delegate()
				{
					string city = string.IsNullOrEmpty(Model.City.Value) ? null : Model.City.Value.Trim();
					string state = Model.State.Value != null ? Model.State.SelectedItem.StateAB : null;
					string county = string.IsNullOrEmpty(Model.County.Value) ? null : Model.County.Value.Trim();
					DateTime? endDate = Model.PurchaseEndDate.Value == null ? Model.PurchaseEndDate.Value : Model.PurchaseEndDate.Value.Value.AddDays(1);

					return SosCrmDataStoredProcedureManager.MS_AccountGetPurchasedAccounts(city,state, county, Model.PurchaseStartDate.Value, endDate).GetDataSet().Tables[0].AsEnumerable();
				},
				delegate(DataRow row)
				{
					PurchasedAccountsModel purchasedAccountsModel = new PurchasedAccountsModel();
					purchasedAccountsModel.AccountID.Value = (int)row["AccountID"];
					purchasedAccountsModel.CustomerName.Value = row["CustomerName"] as string;
					purchasedAccountsModel.StreetAddress.Value = string.IsNullOrEmpty(row["StreetAddress"] as string) ? row["StreetAddress"] as string : (row["StreetAddress"] as string).ToUpper();
					purchasedAccountsModel.City.Value = string.IsNullOrEmpty(row["City"] as string) ? row["City"] as string : (row["City"] as string).ToUpper();
					purchasedAccountsModel.State.Value = (row["StateAB"] as string).ToUpper();
					purchasedAccountsModel.Zip.Value = row["PostalCode"] as string;
					purchasedAccountsModel.County.Value = string.IsNullOrEmpty(row["County"] as string) ? row["County"] as string : (row["County"] as string).ToUpper();
					purchasedAccountsModel.PremisePhone.Value = StringUtility.FormatPhoneNumber(row["PremisePhone"] as string);
					purchasedAccountsModel.HomePhone.Value = StringUtility.FormatPhoneNumber(row["PhoneHome"] as string);
					purchasedAccountsModel.WorkPhone.Value = StringUtility.FormatPhoneNumber(row["PhoneWork"] as string);
					purchasedAccountsModel.WorkPhoneExt.Value = row["PhoneWorkExt"] as string;
					purchasedAccountsModel.CellPhone.Value = StringUtility.FormatPhoneNumber(row["PhoneCell"] as string);
					purchasedAccountsModel.Purchaser.Value = row["PurchasedBy"] as string;
					purchasedAccountsModel.PurchaseDate.Value = (DateTime)row["PurchasedDate"];

					return purchasedAccountsModel;
				},
				List,
				delegate()
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
			System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
			dlg.DefaultExt = ".xlsx";
			dlg.Filter = "Excel Files (.xlsx)|*.xlsx";

			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				IsLoading.Value = true;
				string fileName = dlg.FileName;
				AsyncHelper.ExecuteAsync(
					delegate()
					{
						ExcelPackage excelPkg = new ExcelPackage();

						ExcelWorksheet worksheet = excelPkg.Workbook.Worksheets.Add(ActionNamesSimple.PurchasedAccountsReport);

						//Add Headers
						worksheet.Cells[1, 1].Value = "Account #";
						worksheet.Cells[1, 2].Value = "Customer Name";
						worksheet.Cells[1, 3].Value = "Purchaser";
						worksheet.Cells[1, 4].Value = "Purchase Date";
						worksheet.Cells[1, 5].Value = "Street Address";
						worksheet.Cells[1, 6].Value = "City";
						worksheet.Cells[1, 7].Value = "County";
						worksheet.Cells[1, 8].Value = "State";
						worksheet.Cells[1, 9].Value = "Zip";
						worksheet.Cells[1, 10].Value = "Premise Phone";
						worksheet.Cells[1, 11].Value = "Home Phone";
						worksheet.Cells[1, 12].Value = "Cell Phone";
						worksheet.Cells[1, 13].Value = "Work Phone";
						worksheet.Cells[1, 14].Value = "Work Phone Ext.";


						//Add all other data
						int row = 2;
						foreach (PurchasedAccountsModel item in List)
						{
							worksheet.Cells[row, 1].Value = item.AccountID.Value;
							worksheet.Cells[row, 2].Value = item.CustomerName.Value;
							worksheet.Cells[row, 3].Value = item.Purchaser.Value;
							worksheet.Cells[row, 4].Value = item.PurchaseDate.Value;
							worksheet.Cells[row, 5].Value = item.StreetAddress.Value;
							worksheet.Cells[row, 6].Value = item.City.Value;
							worksheet.Cells[row, 7].Value = item.County.Value;
							worksheet.Cells[row, 8].Value = item.State.Value;
							worksheet.Cells[row, 9].Value = item.Zip.Value;
							worksheet.Cells[row, 10].Value = item.PremisePhone.Value;
							worksheet.Cells[row, 11].Value = item.HomePhone.Value;
							worksheet.Cells[row, 12].Value = item.CellPhone.Value;
							worksheet.Cells[row, 13].Value = item.WorkPhone.Value;
							worksheet.Cells[row, 14].Value = item.WorkPhoneExt.Value;

							row = row + 1;
						}


						using (Stream writer = new FileStream(fileName, FileMode.Create))
						{
							byte[] bytes = excelPkg.GetAsByteArray();
							writer.Write(bytes, 0, bytes.Length);
						}
					}, delegate()
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
