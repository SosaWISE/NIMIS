using System;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking.KW621
{
	public interface IFnsKwRequest
	{
		long RequestID { get; }
		long AccountId { get; }
		long UnitID { get; }
		string Sentence { get; }
		int? Attempts { get; }
		DateTime? LastAttempDate { get; }
		DateTime? ProcessDate { get; }
		DateTime CreatedOn { get; }
		 
	}
}