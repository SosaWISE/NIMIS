using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountClientsView
	{
		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		long CustomerID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		//[DataMember]
		//string UnitID { get; set; }

		[DataMember]
		string AccountName { get; set; }

		[DataMember]
		string AccountDesc { get; set; }

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

		//[DataMember]
		//string InvItemId { get; set; }

		[DataMember]
		long? IndustryAccountId { get; set; }

		[DataMember]
		string IndustryNumber { get; set; }

		[DataMember]
		string Designator { get; set; }

		[DataMember]
		string SubscriberNumber { get; set; }

	}
}
