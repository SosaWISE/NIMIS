using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models.Receiver
{
	public class FnsIndustryAccountModel : IFnsIndustryAccountModel
	{

		#region .ctor

		public FnsIndustryAccountModel(MS_IndustryAccount oIndustryAccount)
		{
			IndustryAccountID = oIndustryAccount.IndustryAccountID;
			AccountId = oIndustryAccount.AccountId;
			ReceiverLineId = oIndustryAccount.ReceiverLineId;
			ReceiverLineBlockId = oIndustryAccount.ReceiverLineBlockId;
			SubscriberId = oIndustryAccount.SubscriberId;
			Csid = oIndustryAccount.Csid;
			IsMove = oIndustryAccount.IsMove;
			IsTakeover = oIndustryAccount.IsTakeover;
			IsActive = oIndustryAccount.IsActive;
			IsDeleted = oIndustryAccount.IsDeleted;
			ModifiedOn = oIndustryAccount.ModifiedOn;
			ModifiedBy = oIndustryAccount.ModifiedBy;
			CreatedOn = oIndustryAccount.CreatedOn;
			CreatedBy = oIndustryAccount.CreatedBy;
			DexRowTs = oIndustryAccount.DEX_ROW_TS;
		}

		#endregion .ctor

		#region Implementation of IFnsIndustryAccountModel

		public long IndustryAccountID { get; private set; }
		public long AccountId { get; private set; }
		public string ReceiverLineId { get; private set; }
		public string ReceiverLineBlockId { get; private set; }
		public string SubscriberId { get; private set; }
		public string Csid { get; private set; }
		public bool IsMove { get; private set; }
		public bool IsTakeover { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public DateTime ModifiedOn { get; private set; }
		public string ModifiedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime DexRowTs { get; private set; }

		#endregion Implementation of IFnsIndustryAccountModel

	}
}