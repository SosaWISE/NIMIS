using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountClientsView : IFnsMsAccountClientsView
	{
		#region .ctor

		public FnsMsAccountClientsView (MS_AccountClientsView oItem)
		{
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			CustomerID = oItem.CustomerID;
			AccountId = oItem.AccountId;
			AccountName = oItem.AccountName;
			AccountDesc = oItem.AccountDesc;
			EventID = oItem.EventID;
			EventDate = oItem.EventDate;
			LastLatt = oItem.LastLatt;
			LastLong = oItem.LastLong;
			UIName = oItem.UIName;
			//UnitID = oItem.GpsWatchUnitID;
			Username = oItem.Username;
			Password = oItem.Password;
			CustomerTypeId = oItem.CustomerTypeId;
			SystemTypeId = oItem.SystemTypeId;
			PanelTypeId = oItem.PanelTypeId;
			//InvItemId = oItem.InvItemId;
			IndustryAccountId = oItem.IndustryAccountId;
			IndustryNumber = oItem.IndustryAccount;
			Designator = oItem.Designator;
			SubscriberNumber = oItem.SubscriberNumber;
		}


		#endregion .ctor

		#region Properties

		public long CustomerMasterFileId { get; set; }
		public long CustomerID { get; set; }
		public long AccountId { get; set; }
		public string AccountName { get; set; }
		public string AccountDesc { get; set; }
		public long? EventID { get; set; }
		public DateTime? EventDate { get; set; }
		public string LastLatt { get; set; }
		public string LastLong { get; set; }
		public string UIName { get; set; }
		//public string UnitID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string CustomerTypeId { get; set; }
		public string SystemTypeId { get; set; }
		public string PanelTypeId { get; set; }
		//public string InvItemId { get; set; }
		public long? IndustryAccountId { get; set; }
		public string IndustryNumber { get; set; }
		public string Designator { get; set; }
		public string SubscriberNumber { get; set; }

		#endregion Properties
	}
}