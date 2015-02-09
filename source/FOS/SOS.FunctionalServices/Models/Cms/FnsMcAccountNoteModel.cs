using System;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsMcAccountNoteModel : IFnsMcAccountNoteModel
	{
		#region Implementation of IFnsMcAccountNoteModel

		public long NoteID { get; set; }
		public string NoteTypeID { get; set; }
		public string NoteType { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? CustomerId { get; set; }
		public long? LeadId { get; set; }
		public int NoteCategory1ID { get; set; }
		public string Category1 { get; set; }
		public string Desc1 { get; set; }
		public int? NoteCategory2ID { get; set; }
		public string Category2 { get; set; }
		public string Desc2 { get; set; }
		public string Note { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion Implementation of IFnsMcAccountNoteModel
	}
}