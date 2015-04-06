using System;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Models.Funding
{
	public class FnsFeBundleItem : IFnsFeBundleItem
	{
		#region .ctor

		public FnsFeBundleItem(FE_BundleItemsView item)
		{
			BundleItemID = item.BundleItemID;
			BundleId = item.BundleId;
			PacketId = item.PacketId;
			IsDeleted = item.IsDeleted;
			CreatedOn = item.CreatedOn;
			CreatedBy = item.CreatedBy;
			PSubmittedOn = item.PSubmittedOn;
			PSubmittedBy = item.PSubmittedBy;
			PCreatedOn = item.PCreatedOn;
			PCreatedBy = item.PCreatedBy;
		}

		public int BundleItemID { get; private set; }

		public int BundleId { get; private set; }

		public int PacketId { get; private set; }

		public bool IsDeleted { get; private set; }

		public DateTime CreatedOn { get; private set; }

		public string CreatedBy { get; private set; }

		public DateTime? PSubmittedOn { get; private set; }

		public string PSubmittedBy { get; private set; }

		public DateTime PCreatedOn { get; private set; }

		public string PCreatedBy { get; private set; }

		#endregion .ctor
	}
}
