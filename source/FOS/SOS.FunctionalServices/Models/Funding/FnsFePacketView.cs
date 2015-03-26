using System;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Models.Funding
{
	public class FnsFePacketView : IFnsFePacketView
	{
		#region .ctor

		public FnsFePacketView(FE_PacketsView item)
		{
			PacketID = item.PacketID;
			CriteriaName = item.CriteriaName;
			CriteriaId = item.CriteriaId;
			PurchaserID = item.PurchaserID;
			PurchaserName = item.PurchaserName;
			SubmittedOn = item.SubmittedOn;
			SubmittedBy = item.SubmittedBy;
			IsDeleted = item.IsDeleted;
			CreatedOn = item.CreatedOn;
			CreatedBy = item.CreatedBy;
		}

		#endregion .ctor

		#region
		public int PacketID { get; private set; }
		public string CriteriaName { get; private set; }
		public int? CriteriaId { get; private set; }
		public string PurchaserID { get; private set; }
		public string PurchaserName { get; private set; }
		public DateTime? SubmittedOn { get; private set; }
		public string SubmittedBy { get; private set; }
		public bool IsDeleted { get; private set; }
		public DateTime? CreatedOn { get; private set; }
		public string CreatedBy { get; private set; }
		#endregion
	}
}
