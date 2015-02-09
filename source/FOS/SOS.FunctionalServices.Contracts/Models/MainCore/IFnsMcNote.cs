using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.MainCore
{
	public interface IFnsMcNote
	{
		[DataMember]
		long NoteID { get; set; }

		[DataMember]
		string NoteTypeId { get; set; }

		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		long? CustomerId { get; set; }

		[DataMember]
		long? LeadId { get; set; }

		[DataMember]
		int NoteCategory1Id { get; set; }

		[DataMember]
		int? NoteCategory2Id { get; set; }

		[DataMember]
		string Note { get; set; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }
		 
	}
}