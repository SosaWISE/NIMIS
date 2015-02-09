using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.MainCore
{
	public interface IFnsMcAccountNoteCat2
	{
		[DataMember]
		int NoteCategory2ID { get; }

		[DataMember]
		int NoteCategory1Id { get; }

		[DataMember]
		string Category { get; }

		[DataMember]
		string Description { get; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }
		 
	}
}