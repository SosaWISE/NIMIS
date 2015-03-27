using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using System;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountSalesInformation : IFnsMsAccountSalesInformation
	{
		#region .ctor

		public FnsMsAccountSalesInformation(MS_AccountSalesInformationsView info)
		{
			AccountID = info.AccountID;
			PaymentTypeId = info.PaymentTypeId;
			BillingDay = info.BillingDay;
			CurrentMonitoringStation = info.CurrentMonitoringStation;
			PanelTypeId = info.PanelTypeId;
			PanelItemId = info.PanelItemId;
			IsTakeOver = info.IsTakeOver;
			IsOwner = info.IsOwner;
			CellPackageItemId = info.CellPackageItemId;
			CellularTypeId = info.CellularTypeId;
			CellularTypeName = info.CellularTypeName;
			CellServicePackage = info.CellServicePackage;
			CellularVendor = info.CellularVendor;
			SetupFee = info.SetupFee;
			SetupFee1StMonth = info.SetupFee1stMonth;
			MMR = info.MMR;
			Over3Months = info.Over3Months;
			ContractLength = info.ContractLength;
			ContractId = info.ContractId;
			ContractTemplateId = info.ContractTemplateId;
			Email = info.Email;
			IsMoni = info.IsMoni;

			FriendsAndFamilyTypeId = info.FriendsAndFamilyTypeId;
			AccountSubmitId = info.AccountSubmitId;
			AccountCancelReasonId = info.AccountCancelReasonId;
			TechId = info.TechId;
			SalesRepId = info.SalesRepId;
			InstallDate = info.InstallDate;
			SubmittedToCSDate = info.SubmittedToCSDate;
			CsConfirmationNumber = info.CsConfirmationNumber;
			CsTwoWayConfNumber = info.CsTwoWayConfNumber;
			SubmittedToGPDate = info.SubmittedToGPDate;
			ContractSignedDate = info.ContractSignedDate;
			CancelDate = info.CancelDate;
			AMA = info.AMA;
			NOC = info.NOC;
			SOP = info.SOP;
		}


		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public string PaymentTypeId { get; private set; }
		public short? BillingDay { get; private set; }
		public string CurrentMonitoringStation { get; private set; }
		public string PanelTypeId { get; private set; }
		public string PanelItemId { get; private set; }
		public bool? IsTakeOver { get; private set; }
		public bool? IsOwner { get; private set; }
		public string CellPackageItemId { get; private set; }
		public string CellularTypeId { get; private set; }
		public string CellularTypeName { get; private set; }
		public string CellServicePackage { get; private set; }
		public string CellularVendor { get; set; }
		public decimal? SetupFee { get; private set; }
		public decimal? SetupFee1StMonth { get; private set; }
		public decimal? MMR { get; private set; }
		public bool? Over3Months { get; private set; }
		public short? ContractLength { get; private set; }
		public int? ContractId { get; private set; }
		public int? ContractTemplateId { get; private set; }
		public string Email { get; private set; }
		public bool? IsMoni { get; private set; }

		public string FriendsAndFamilyTypeId { get; set; }
		public long? AccountSubmitId { get; set; }
		public string AccountCancelReasonId { get; set; }
		public string TechId { get; set; }
		public string SalesRepId { get; set; }
		public DateTime? InstallDate { get; set; }
		public DateTime? SubmittedToCSDate { get; set; }
		public string CsConfirmationNumber { get; set; }
		public string CsTwoWayConfNumber { get; set; }
		public DateTime? SubmittedToGPDate { get; set; }
		public DateTime? ContractSignedDate { get; set; }
		public DateTime? CancelDate { get; set; }
		public string AMA { get; set; }
		public string NOC { get; set; }
		public string SOP { get; set; }
		#endregion Properties
	}
}
