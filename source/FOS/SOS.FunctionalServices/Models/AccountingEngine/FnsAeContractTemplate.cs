using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeContractTemplate : IFnsAeContractTemplate
	{
		#region .ctor

		public FnsAeContractTemplate(AE_ContractTemplate contractTemplate)
		{
			ContractTemplateID = contractTemplate.ContractTemplateID;
			ContractName = contractTemplate.ContractName;
			ContractLength = contractTemplate.ContractLength;
			MonthlyFee = contractTemplate.MonthlyFee;
			ShortDesc = contractTemplate.ShortDesc;
			IsActive = contractTemplate.IsActive;
			IsDeleted = contractTemplate.IsDeleted;
			ModifiedBy = contractTemplate.ModifiedBy;
			ModifiedOn = contractTemplate.ModifiedOn;
			CreatedBy = contractTemplate.CreatedBy;
			CreatedOn = contractTemplate.CreatedOn;
			DexRowTs = contractTemplate.DEX_ROW_TS;
		}

		#endregion .ctor

		#region Properties
		public int ContractTemplateID { get; set; }
		public string ContractName { get; set; }
		public short ContractLength { get; set; }
		public decimal MonthlyFee { get; set; }
		public string ShortDesc { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DexRowTs { get; set; }
		#endregion Properties
	}
}
