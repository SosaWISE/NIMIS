using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.MainCore;

namespace SOS.FunctionalServices.Models.MainCore
{
	public class FnsMcAccountNoteType : IFnsMcAccountNoteType
	{

		#region .ctor

		public FnsMcAccountNoteType(MC_AccountNoteType noteType)
		{
			NoteTypeID = noteType.NoteTypeID;
			NoteType = noteType.NoteType;
		}
		#endregion .ctor

		#region Properties
		public string NoteTypeID { get; private set; }
		public string NoteType { get; private set; }
		#endregion Properties
	}
}