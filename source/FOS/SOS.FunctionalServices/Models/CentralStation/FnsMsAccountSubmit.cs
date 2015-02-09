using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountSubmit : IFnsMsAccountSubmit
	{
		#region .ctor

		public FnsMsAccountSubmit(MS_AccountSubmit sub)
		{
			AccountSubmitID = sub.AccountSubmitID;
			AccountId = sub.AccountId;
			GPTechId = sub.GPTechId;
			DateSubmitted = sub.DateSubmitted;
			WasSuccessfull = sub.WasSuccessfull;
			Message = sub.Message;
		}

		#endregion .ctor

		#region Properties
		public long AccountSubmitID { get; set; }
		public long AccountId { get; set; }
		public string GPTechId { get; set; }
		public DateTime DateSubmitted { get; set; }
		public bool WasSuccessfull { get; set; }
		public string Message { get; set; }
		#endregion Properties
	}
}
