using System;

namespace SOS.FunctionalServices.Contracts.Models.Connext
{
	public interface IFnsCxContactNote
	{
		long ContactNoteID { get; }
		long ContactId { get; }
		string Note { get; }
		DateTime CreatedOn { get; }
		string CreatedBy { get; }
	}
}