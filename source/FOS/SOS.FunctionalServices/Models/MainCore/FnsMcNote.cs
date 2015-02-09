using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Models.MainCore
{
	public class FnsMcNote : IFnsMcNote
	{
		#region .ctor

		public FnsMcNote()
		{
		}

		public FnsMcNote(MC_AccountNote note)
		{
			NoteID = note.NoteID;
			NoteTypeId = note.NoteTypeId;
			CustomerMasterFileId = note.CustomerMasterFileId;
			CustomerId = note.CustomerId;
			LeadId = note.LeadId;
			NoteCategory1Id = note.NoteCategory1Id;
			NoteCategory2Id = note.NoteCategory2Id;
			Note = note.Note;
			CreatedBy = note.CreatedBy;
			CreatedOn = note.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		public long NoteID { get; set; }
		public string NoteTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? CustomerId { get; set; }
		public long? LeadId { get; set; }
		public int NoteCategory1Id { get; set; }
		public int? NoteCategory2Id { get; set; }
		public string Note { get; set; }
		public string CreatedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		#endregion Properties
	}
}
