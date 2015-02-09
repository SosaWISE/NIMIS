using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountSubmit
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
		 
		[DataMember]
		string Message { get; set; }
	}
}