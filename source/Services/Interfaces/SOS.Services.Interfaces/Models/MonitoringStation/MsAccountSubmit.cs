using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountSubmit : IMsAccountSubmit
	{
		#region Properties
		public long AccountSubmitID { get; set; }
		public long AccountId { get; set; }
		public string GPTechId { get; set; }
		public DateTime DateSubmitted { get; set; }
		public bool WasSuccessfull { get; set; }
		#endregion Properties
	}

	public interface IMsAccountSubmit
	{
		[DataMember]
		long AccountSubmitID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string GPTechId { get; set; }

		[DataMember]
		DateTime DateSubmitted { get; set; }

		[DataMember]
		bool WasSuccessfull { get; set; }
	}
}
