using System;

namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsIndustryAccountModel
	{
		long IndustryAccountID { get; }
		long AccountId	{ get; }
		string ReceiverLineId { get; }
		string ReceiverLineBlockId { get; }
		string SubscriberId { get; }
		string Csid { get; }
		bool IsMove { get; }
		bool IsTakeover { get; }
		bool IsActive { get; }
		bool IsDeleted { get; }
		DateTime ModifiedOn { get; }
		string ModifiedBy { get; }
		DateTime CreatedOn { get; }
		string CreatedBy { get; }
		DateTime DexRowTs { get; }
			 
	}
}