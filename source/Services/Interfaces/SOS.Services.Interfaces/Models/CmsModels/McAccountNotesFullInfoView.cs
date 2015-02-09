using System;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region McAccountNotesFullInfoView

	public interface IMcAccountNotesFullInfoView
	{
		long NoteID { get; set; }
		string NoteTypeID { get; set; }
		string NoteType { get; set; }
		long CustomerMasterFileId { get; set; }
		long? CustomerId { get; set; }
		long? LeadId { get; set; }
		int NoteCategory1ID { get; set; }
		string Category1 { get; set; }
		string Desc1 { get; set; }
		int NoteCategory2ID { get; set; }
		string Category2 { get; set; }
		string Desc2 { get; set; }
		string Note { get; set; }
		string CreatedBy { get; set; }
		DateTime CreatedOn { get; set; }

	}

	public class McAccountNotesFullInfoView : IMcAccountNotesFullInfoView
	{
		#region Implementation of IMcAccountNotesFullInfoView

		public long NoteID { get; set; }
		public string NoteTypeID { get; set; }
		public string NoteType { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? CustomerId { get; set; }
		public long? LeadId { get; set; }
		public int NoteCategory1ID { get; set; }
		public string Category1 { get; set; }
		public string Desc1 { get; set; }
		public int NoteCategory2ID { get; set; }
		public string Category2 { get; set; }
		public string Desc2 { get; set; }
		public string Note { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion Implementation of IMcAccountNotesFullInfoView
	}

	#endregion McAccountNotesFullInfoView
}
