using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MainCore
{
	public class McNoteType
	{
		#region Properties

		[DataMember]
		public string NoteTypeID { get; set; }

		[DataMember]
		public string NoteType { get; set; }

		#endregion Properties
	}
}
