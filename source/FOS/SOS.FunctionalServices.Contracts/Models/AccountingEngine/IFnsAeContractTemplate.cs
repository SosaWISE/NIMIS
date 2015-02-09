using System;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeContractTemplate
	{
		int ContractTemplateID { get; set; }
		string ContractName { get; set; }
		short ContractLength { get; set; }
		decimal MonthlyFee { get; set; }
		string ShortDesc { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		DateTime ModifiedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime CreatedOn { get; set; }
		string CreatedBy { get; set; }
		DateTime DexRowTs { get; set; }
	}
}