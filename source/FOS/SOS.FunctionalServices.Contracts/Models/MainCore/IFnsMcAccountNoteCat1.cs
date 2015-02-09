using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.MainCore
{
	public interface IFnsMcAccountNoteCat1
	{
		[DataMember]
		int NoteCategory1ID { get; }

		[DataMember]
		string Category { get; }

		[DataMember]
		string NoteTypeId { get; }

		[DataMember]
		string Description { get; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }
	}
}