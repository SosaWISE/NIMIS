using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsLpRequest : IFnsLpRequest
	{
		#region .ctor

		public FnsLpRequest(LP_Request request)
		{
			RequestID = request.RequestID;
			RequestNameId = request.RequestNameId;
			AccountId = request.AccountId;
			UnitID = request.UnitID;
			Sentence = request.Sentence;
			Attempts = request.Attempts;
			LastAttempDate = request.LastAttempDate;
			ProcessDate = request.ProcessDate;
			CreatedOn = request.CreatedOn;
		}

		#endregion .ctor

		#region Member Variables

		public long RequestID { get; private set; }
		public string RequestNameId { get; private set; }
		public long AccountId { get; private set; }
		public long UnitID { get; private set; }
		public string Sentence { get; private set; }
		public int? Attempts { get; private set; }
		public DateTime? LastAttempDate { get; private set; }
		public DateTime? ProcessDate { get; private set; }
		public DateTime CreatedOn { get; private set; }

		#endregion Member Variables
	}
}
