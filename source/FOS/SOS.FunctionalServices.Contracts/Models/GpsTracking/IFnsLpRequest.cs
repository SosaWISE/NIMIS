using System;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsLpRequest
	{
		long RequestID { get;  }
		string RequestNameId { get; }
		long AccountId { get; }
		long UnitID { get; }
		string Sentence { get; }
		int? Attempts { get; }
		DateTime? LastAttempDate { get; }
		DateTime? ProcessDate { get; }
		DateTime CreatedOn { get; }
	}
}
