using System;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Models.Funding
{
	public class FnsFePacketItemView : IFnsFePacketItemView
	{
		#region .ctor

		public FnsFePacketItemView(FE_PacketItemsView viewItem)
		{
			PacketItemID = viewItem.PacketItemID;
			PacketId = viewItem.PacketId;
			CustomerNumber = viewItem.CustomerNumber;
			CustomerId = viewItem.CustomerId;
			AccountId = viewItem.AccountId;
			Csid = viewItem.Csid;
			FirstName = viewItem.FirstName;
			LastName = viewItem.LastName;
			ReturnAccountFundingStatusId = viewItem.ReturnAccountFundingStatusId;
			AccountFundingShortDesc = viewItem.AccountFundingShortDesc;
			AccountStatusNote = viewItem.AccountStatusNote;
			TransactionID = viewItem.TransactionID;
			ReportGuid = viewItem.ReportGuid;
			Bureau = viewItem.Bureau;
			Gateway = viewItem.Gateway;
			ModifiedBy = viewItem.ModifiedBy;
			ModifiedOn = viewItem.ModifiedOn;
			CreatedBy = viewItem.CreatedBy;
			CreatedOn = viewItem.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		public long PacketItemID { get; private set; }
		public int PacketId { get; private set; }
		public long CustomerNumber { get; private set; }
		public long CustomerId { get; private set; }
		public long AccountId { get; private set; }
		public string Csid { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public long? ReturnAccountFundingStatusId { get; private set; }
		public string AccountFundingShortDesc { get; private set; }
		public string AccountStatusNote { get; private set; }
		public string TransactionID { get; private set; }
		public string ReportGuid { get; private set; }
		public string Bureau { get; private set; }
		public string Gateway { get; private set; }
		public string ModifiedBy { get; private set; }
		public DateTime? ModifiedOn { get; private set; }
		public string CreatedBy { get; private set; }
		public DateTime? CreatedOn { get; private set; }
		#endregion Properties
	}
}
