using System;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McAccountNotes

	public interface IMcAccountNote
	{
		long NoteID { get; set; }
		string NoteTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		long? CustomerId { get; set; }
		long? LeadId { get; set; }
		int NoteCategory1Id { get; set; }
		int? NoteCategory2Id { get; set; }
		string Note { get; set; }
		string CreatedBy { get; set; }
		DateTime CreatedOn { get; set; }
	}

	public class McAccountNote : IMcAccountNote
	{
		#region Implementation of IMcAccountNote

		public long NoteID { get; set; }
		public string NoteTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? CustomerId { get; set; }
		public long? LeadId { get; set; }
		public int NoteCategory1Id { get; set; }
		public int? NoteCategory2Id { get; set; }
		public string Note { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion Implementation of IMcAccountNote
	}

	#endregion McAccountNotes
}
