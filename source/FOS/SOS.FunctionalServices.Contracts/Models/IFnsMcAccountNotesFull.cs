using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsMcAccountNotesFull
	{
		[DataMember]
		long NoteID { get; set; }
		[DataMember]
		string NoteTypeID { get; set; }
		[DataMember]
		string NoteType { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		long? CustomerId { get; set; }
		[DataMember]
		long? LeadId { get; set; }
		[DataMember]
		int NoteCategory1Id { get; set; }
		[DataMember]
		string Category1 { get; set; }
		[DataMember]
		string Desc1 { get; set; }
		[DataMember]
		int NoteCategory2Id { get; set; }
		[DataMember]
		string Category2 { get; set; }
		[DataMember]
		string Desc2 { get; set; }
		[DataMember]
		string Note { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
	}

	public interface IFnsMcAccountNoteModel
	{
		[DataMember]
		long NoteID { get; set; }
		[DataMember]
		string NoteTypeID { get; set; }
		[DataMember]
		string NoteType { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		long? CustomerId { get; set; }
		[DataMember]
		long? LeadId { get; set; }
		[DataMember]
		int NoteCategory1ID { get; set; }
		[DataMember]
		string Category1 { get; set; }
		[DataMember]
		string Desc1 { get; set; }
		[DataMember]
		int? NoteCategory2ID { get; set; }
		[DataMember]
		string Category2 { get; set; }
		[DataMember]
		string Desc2 { get; set; }
		[DataMember]
		string Note { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
	}
}