using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.MainCore
{
	public interface IFnsMcAccountNoteType
	{
		[DataMember]
		string NoteTypeID { get; }
		
		[DataMember]
		string NoteType { get; }
	}
}