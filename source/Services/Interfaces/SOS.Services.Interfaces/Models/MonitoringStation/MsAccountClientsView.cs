using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region MsAccountClientsView

	public interface IMsAccountClientsView
	{
		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		long CustomerID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string UnitID { get; set; }

		[DataMember]
		string AccountName { get; set; }

		[DataMember]
		long? EventID { get; set; }

		[DataMember]
		DateTime? EventDate { get; set; }

		[DataMember]
		string LastLatt { get; set; }

		[DataMember]
		string LastLong { get; set; }

		[DataMember]
		string UIName { get; set; }

		[DataMember]
		string Username { get; set; }

		[DataMember]
		string Password { get; set; }

		[DataMember]
		string CustomerTypeId { get; set; }

		[DataMember]
		string SystemTypeId { get; set; }

		[DataMember]
		string PanelTypeId { get; set; }

		[DataMember]
		string InvItemId { get; set; }

		[DataMember]
		long? IndustryAccountId { get; set; }

		[DataMember]
		string IndustryNumber { get; set; }

		[DataMember]
		string Designator { get; set; }

		[DataMember]
		string SubscriberNumber { get; set; }

	}

	public class MsAccountClientsView : IMsAccountClientsView
	{
		#region Properties

		public long CustomerMasterFileId { get; set; }
		public long CustomerID { get; set; }
		public long AccountId { get; set; }
		public string AccountName { get; set; }
		public long? EventID { get; set; }
		public DateTime? EventDate { get; set; }
		public string LastLatt { get; set; }
		public string LastLong { get; set; }
		public string UIName { get; set; }
		public string UnitID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string CustomerTypeId { get; set; }
		public string SystemTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public string InvItemId { get; set; }
		public long? IndustryAccountId { get; set; }
		public string IndustryNumber { get; set; }
		public string Designator { get; set; }
		public string SubscriberNumber { get; set; }

		#endregion Properties

	}

	#endregion MsAccountClientsView
}
