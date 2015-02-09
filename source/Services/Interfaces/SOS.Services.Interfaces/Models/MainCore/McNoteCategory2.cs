using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MainCore
{
	public class McNoteCategory2
	{
		[DataMember]
		public int NoteCategory2ID { get; set; }

		[DataMember]
		public int NoteCategory1Id { get; set; }

		[DataMember]
		public string Category { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string CreatedBy { get; set; }

		[DataMember]
		public DateTime CreatedOn { get; set; }
	}
}
