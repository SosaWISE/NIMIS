using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking.KW621;

namespace SOS.FunctionalServices.Models.GpsTracking.KW621
{
	public class FnsKwRequest : IFnsKwRequest
	{
		#region .ctor
		public FnsKwRequest(KW_Request req)
		{
			RequestID = req.RequestID;
			AccountId = req.AccountId;
			UnitID = req.UnitID;
			Sentence = req.Sentence;
			Attempts = req.Attempts;
			LastAttempDate = req.LastAttempDate;
			ProcessDate = req.ProcessDate;
			CreatedOn = req.CreatedOn;
		}

		#endregion .ctor

		#region Memeber Variables
		public long RequestID { get; private set; }
		public long AccountId { get; private set; }
		public long UnitID { get; private set; }
		public string Sentence { get; private set; }
		public int? Attempts { get; private set; }
		public DateTime? LastAttempDate { get; private set; }
		public DateTime? ProcessDate { get; private set; }
		public DateTime CreatedOn { get; private set; }
		#endregion Memeber Variables

	}
}