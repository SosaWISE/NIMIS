using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MainCore
{
	public class McNoteCategory1
	{
		#region Properties

		[DataMember]
		public int NoteCategory1ID { get; set; }

		[DataMember]
		public string Category { get; set; }

		[DataMember]
		public string NoteTypeId { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string CreatedBy { get; set; }

		[DataMember]
		public DateTime CreatedOn { get; set; }

		#endregion Properties
	}
}
