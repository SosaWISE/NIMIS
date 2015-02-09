using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MainCore
{
	public class McNote
	{
		[DataMember]
		public long NoteID { get; set; }

		[DataMember]
		public string NoteTypeId { get; set; }

		[DataMember]
		public string NoteType { get; set; }

		[DataMember]
		public long CustomerMasterFileId { get; set; }

		[DataMember]
		public long? CustomerId { get; set; }

		[DataMember]
		public long? LeadId { get; set; }

		[DataMember]
		public int NoteCategory1Id { get; set; }

		[DataMember]
		public string Category1 { get; set; }

		[DataMember]
		public string Desc1 { get; set; }

		[DataMember]
		public int? NoteCategory2Id { get; set; }

		[DataMember]
		public string Category2 { get; set; }

		[DataMember]
		public string Desc2 { get; set; }

		[DataMember]
		public string Note { get; set; }

		[DataMember]
		public string CreatedBy { get; set; }

		[DataMember]
		public DateTime CreatedOn { get; set; }
	}

	public class McNoteArg : McNote
	{
		[DataMember]
		public int PageSize { get; set; }

		[DataMember]
		public int PageNumber { get; set; }
	}

}
