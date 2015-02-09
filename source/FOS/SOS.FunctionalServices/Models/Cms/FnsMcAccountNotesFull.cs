using System;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models.Cms
{
	public class FnsMcAccountNotesFull : IFnsMcAccountNotesFull
	{
		#region .ctor

		public FnsMcAccountNotesFull(Data.SosCrm.MC_AccountNotesAllInfoView oItem)
		{
			NoteID = oItem.NoteID;
			NoteTypeID = oItem.NoteTypeId;
			NoteType = oItem.NoteType;
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			CustomerId = oItem.CustomerId;
			LeadId = oItem.LeadId;
			NoteCategory1Id = oItem.NoteCategory1Id;
			Category1 = oItem.Category1;
			Desc1 = oItem.Desc1;
			NoteCategory2Id = oItem.NoteCategory2Id;
			Category2 = oItem.Category2;
			Desc2 = oItem.Desc2;
			Note = oItem.Note;
			CreatedBy = oItem.CreatedBy;
			CreatedOn = oItem.CreatedOn.ToUniversalTime();
		}

		#endregion .ctor

		#region Implementation of IFnsMcAccountNotesFull

		public long NoteID { get; set; }
		public string NoteTypeID { get; set; }
		public string NoteType { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? CustomerId { get; set; }
		public long? LeadId { get; set; }
		public int NoteCategory1Id { get; set; }
		public string Category1 { get; set; }
		public string Desc1 { get; set; }
		public int NoteCategory2Id { get; set; }
		public string Category2 { get; set; }
		public string Desc2 { get; set; }
		public string Note { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		#endregion Implementation of IFnsMcAccountNotesFull
	}
}